using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using LokerIT.Code128.Nodes;
using LokerIT.Code128.Optimization;

namespace LokerIT.Code128
{
    public class Code128Encoder : ICode128Encoder
    {
        private static readonly CodeSetType[] CodeSetAB = {CodeSetType.CodeA, CodeSetType.CodeB};
        private readonly Encoding _encoding;
        private readonly IEncodedCode128Factory _factory;
        private readonly IMapping _mapping;

        public Code128Encoder(IMapping mapping, IEncodedCode128Factory factory, Encoding encoding = null)
        {
            if (encoding != null && !encoding.IsSingleByte)
            {
                throw new ArgumentException("Encoding must be single byte.", nameof(encoding));
            }

            _mapping = mapping;
            _factory = factory;
            _encoding = encoding ?? Encoding.Default;
        }

        public IEncodedCode128 Encode(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var inputChars = _encoding.GetBytes(input);

            var startNodes = GetStartNodes(inputChars);

            foreach (var node in startNodes.Where(c => !c.IsTerminal))
            {
                FollowAndRecurse(inputChars, node);
            }

            var shortestPath = DetermineShortestPath(startNodes);
            var data = new List<Symbol>();
            foreach (var item in shortestPath)
            {
                item.Emit(_mapping, inputChars, data);
            }

            return _factory.Create(_mapping, data.ToArray(), input);
        }

        private void FollowAndRecurse(byte[] input, INode node)
        {
            FollowNode(input, node);
            foreach (var item in node.Successors.Where(c => !c.IsTerminal))
            {
                FollowAndRecurse(input, item);
            }
        }

        private IList<INode> DetermineShortestPath(IReadOnlyList<INode> nodes)
        {
            var terminal = GetCheapestTerminal(nodes);

            var nodeList = new List<INode>();
            while (terminal != null)
            {
                nodeList.Add(terminal);
                terminal = terminal.Predecessor;
            }

            nodeList.Reverse();
            return nodeList;
        }

        private INode GetCheapestTerminal(IReadOnlyList<INode> nodes)
        {
            var tracker = new Tracker();
            foreach (var node in nodes)
            {
                GetCheapestTerminal(tracker, node);
            }

            return tracker.CheapestNode;
        }

        private void GetCheapestTerminal(Tracker tracker, INode node)
        {
            if (tracker.Push(node))
            {
                CheckForPendingTerminal(node);
                foreach (var successor in node.Successors)
                {
                    GetCheapestTerminal(tracker, successor);
                }

                tracker.Pop();
            }
        }

        private List<INode> GetStartNodes(byte[] input)
        {
            if (input.Length == 0)
            {
                return new List<INode> {EmptyStringNode.Instance};
            }

            var result = new List<INode>();

            var count = DigitRun(input, 0);
            var isTerminal = count == input.Length;

            // Use CodeC only if the total input is two digits
            // Or if it starts with at least four digits.
            if (isTerminal && count == 2 || count >= 4)
            {
                var actualCount = count - count % 2;
                result.Add(new CodeCStart(actualCount, actualCount == input.Length));
                return result;
            }

            var types = CodeSetAB;
            foreach (var type in types)
            {
                count = CodeSetRun(input, _mapping.GetCodeSet(type), 0, out var isHigh);
                isTerminal = count == input.Length;

                if (count > 0)
                {
                    if (isHigh)
                    {
                        if (count > 4 || isTerminal && count >= 2)
                        {
                            // always switch to high mode, no need to try anything else
                            result.Add(new CodeSetStart(type, count, isTerminal, HighModeChange.Toggle));
                        }
                        else
                        {
                            // try switching and shifting
                            result.Add(new CodeSetStart(type, count, isTerminal, HighModeChange.Toggle));
                            result.Add(new CodeSetStart(type, count, isTerminal, HighModeChange.Shift));
                        }
                    }
                    else
                    {
                        result.Add(new CodeSetStart(type, count, count == input.Length, HighModeChange.Keep));
                    }
                }
            }

            return result;
        }


        private void FollowNode(byte[] input, INode predecessor)
        {
            var start = predecessor.Start + predecessor.Length;
            var count = DigitRun(input, start);
            if (count > 0)
            {
                var isTerminal = count + start == input.Length;
                // Below four consecutive digits, CodeC uses more space
                if (count >= 4)
                {
                    if (count % 2 == 0)
                    {
                        // easy, just use a Code C run
                        predecessor.AddSuccessor(new CodeCRun(predecessor, count, isTerminal));
                        return;
                    }


                    var codeSetType = predecessor.FinalCodeSet == CodeSetType.CodeC
                        ? CodeSetType.CodeA
                        : predecessor.FinalCodeSet;
                    if (predecessor.IsHigh)
                    {
                        predecessor.AddSuccessor(new CodeSetRun(predecessor,
                            codeSetType, 1,
                            false,
                            HighModeChange.Toggle));
                        predecessor.AddSuccessor(new CodeSetRun(predecessor,
                            codeSetType, 1,
                            false,
                            HighModeChange.Shift));
                    }
                    else
                    {
                        predecessor.AddSuccessor(new CodeSetRun(predecessor,
                            codeSetType, 1,
                            false,
                            HighModeChange.Keep));
                    }

                    predecessor.AddSuccessor(new CodeCRun(predecessor, count - 1, false));

                    return;
                }
            }

            var types = CodeSetAB;
            foreach (var type in types)
            {
                count = CodeSetRun(input, _mapping.GetCodeSet(type), start, out var high);
                var isHighSet = high;
                var isTerminal = count + start == input.Length;
                var wasHighSet = predecessor.IsHigh;
                var highChange = wasHighSet != high;

                if (count == 0)
                {
                    continue;
                }

                if (count == 1)
                {
                    // Only one character matches this set.
                    // See if we can shift to the target set.
                    // Don't shift if this is the terminal
                    // character or if there's no actual change in code set.
                    // Also, we can't shift from CodeC.
                    if (!isTerminal &&
                        type != predecessor.FinalCodeSet &&
                        predecessor.FinalCodeSet != CodeSetType.CodeC &&
                        CodeSetRun(input, _mapping.GetCodeSet(predecessor.FinalCodeSet), start + count,
                            out high) > 0)
                    {
                        if (highChange)
                        {
                            predecessor.AddSuccessor(new CodeSetShift(predecessor, type, HighModeChange.Shift));
                            predecessor.AddSuccessor(new CodeSetShift(predecessor, type, HighModeChange.Toggle));
                        }
                        else
                        {
                            predecessor.AddSuccessor(new CodeSetShift(predecessor, type, HighModeChange.Keep));
                        }

                        continue;
                    }
                }

                if (highChange)
                {
                    predecessor.AddSuccessor(new CodeSetRun(predecessor, type, count, isTerminal,
                        HighModeChange.Shift));

                    predecessor.AddSuccessor(
                        new CodeSetRun(predecessor, type, count, isTerminal, HighModeChange.Toggle));
                }
                else
                {
                    predecessor.AddSuccessor(new CodeSetRun(predecessor, type, count, isTerminal, HighModeChange.Keep));
                }
            }
        }


        private int DigitRun(byte[] input, int startIndex)
        {
            var index = startIndex;
            while (index < input.Length && char.IsDigit((char) input[index]))
            {
                ++index;
            }

            return index - startIndex;
        }

        private int CodeSetRun(byte[] input, ICodeSet set, int startIndex, out bool highSet)
        {
            bool? high = null;
            var index = startIndex;
            while (index < input.Length)
            {
                var current = input[index];
                var currentIsHigh = current >= 128;
                if (currentIsHigh)
                {
                    current -= 128;
                }

                if (high.HasValue)
                {
                    if (currentIsHigh != high.Value)
                    {
                        // break on first change
                        break;
                    }
                }
                else
                {
                    high = currentIsHigh;
                }

                if (!set.Supports(current))
                {
                    break;
                }

                var digitRun = DigitRun(input, index);

                if (digitRun > 0)
                {
                    var terminalDigitRun = digitRun + index == input.Length;
                    if (digitRun >= (terminalDigitRun ? 4 : 6))
                    {
                        break;
                    }
                }

                ++index;
            }

            highSet = high.GetValueOrDefault();
            return index - startIndex;
        }

        [Conditional("DEBUG")]
        private static void CheckForPendingTerminal(INode node)
        {
            if (!node.Successors.Any() && !node.IsTerminal)
            {
                throw new InvalidOperationException($"Node {node} is not terminal but has no successors");
            }
        }
    }
}