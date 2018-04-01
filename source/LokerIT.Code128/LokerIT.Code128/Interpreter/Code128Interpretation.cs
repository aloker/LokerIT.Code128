using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LokerIT.Code128.Interpreter
{
    public class Code128Interpretation
    {
        private readonly IChecksumCalculator _checksumCalculator;
        private readonly Encoding _encoding;
        private readonly IMapping _mapping;
        private ICodeSet _activeCodeSet;
        private CodeSetType _activeType;
        private List<Symbol> _consumed;
        private Symbol _current;
        private List<byte> _data;
        private byte _high;
        private bool _highShift;
        private int _index;
        private List<byte> _output;
        private CodeSetType? _tempSet;

        public Code128Interpretation(IMapping mapping = null, Encoding encoding = null,
            IChecksumCalculator checksumCalculator = null)
        {
            if (encoding != null && !encoding.IsSingleByte)
            {
                throw new ArgumentException("Encoding must be single byte");
            }

            _mapping = mapping ?? new DefaultMapping();
            _encoding = encoding ?? Encoding.Default;
            _checksumCalculator = checksumCalculator ?? new Mod103ChecksumCalculator();
        }

        public string Interpret(IEnumerable<string> patternList)
        {
            var chars = patternList.SelectMany(c => c).Select(c => (byte) (c - '0'));
            return Interpret(chars);
        }

        public string Interpret(IEnumerable<byte> bars)
        {
            _high = 0;
            _highShift = false;
            _tempSet = null;
            _output = new List<byte>();
            _data = bars.ToList();
            _index = 0;
            _consumed = new List<Symbol>();
            _current = null;

            MatchStart();

            while (_current != null)
            {
                Handle();
            }

            CheckEnd();

            VerifyCheckSum();

            return _encoding.GetString(_output.ToArray());
        }

        private void CheckEnd()
        {
            // make sure we have the end bar
            var final = Next(0, 1);
            if (final == null || final != "2")
            {
                throw new CodeError("Missing final black 2-module bar.");
            }
        }

        private void VerifyCheckSum()
        {
            if (_consumed.Count < 3)
            {
                throw new CodeError("Expected at least 3 codes in the token.");
            }

            var allBytes = _consumed.SelectMany(c => c.Values).ToList();
            var bytes = new List<byte>(allBytes.Take(allBytes.Count - 2));

            var expectedCheckSum = _checksumCalculator.CalculateChecksum(bytes);
            var providedCheckSum = allBytes[allBytes.Count - 2];
            if (expectedCheckSum != providedCheckSum)
            {
                throw new CodeError($"Check sum mismatch. Expected {expectedCheckSum}, found {providedCheckSum}");
            }
        }

        private void Handle()
        {
            var next = Peek(1);
            if (next != null && next.Code == SpecialCodes.Stop)
            {
                // this is the checksum, consume without interpreting
                Consume();
                return;
            }

            switch (_current.Code)
            {
                case SpecialCodes.Func1:
                case SpecialCodes.Func2:
                case SpecialCodes.Func3:
                    // TODO determine what to do
                    Consume();
                    break;
                case SpecialCodes.Func4:
                    if (_tempSet.HasValue)
                    {
                        throw new CodeError("Unexpected Func4 after code set shift.");
                    }

                    Consume();
                    _high += 128;
                    var peek = Peek(0);
                    if (peek == null || peek.Code != SpecialCodes.Func4)
                    {
                        _highShift = true;
                    }
                    else
                    {
                        Consume();
                    }

                    break;
                case SpecialCodes.SwitchToCodeA:
                    HandleCodeSetSwitch(CodeSetType.CodeA);
                    break;
                case SpecialCodes.SwitchToCodeB:
                    HandleCodeSetSwitch(CodeSetType.CodeB);
                    break;
                case SpecialCodes.SwitchToCodeC:
                    HandleCodeSetSwitch(CodeSetType.CodeC);
                    break;
                case SpecialCodes.ShiftToA:
                    HandleShift(CodeSetType.CodeB, CodeSetType.CodeA);
                    break;
                case SpecialCodes.ShiftToB:
                    HandleShift(CodeSetType.CodeA, CodeSetType.CodeB);
                    break;
                case SpecialCodes.StartA:
                case SpecialCodes.StartB:
                case SpecialCodes.StartC:
                    throw new CodeError("Unexpected start token");
                case SpecialCodes.Stop:
                    Consume();
                    return;
                default:

                    switch (_tempSet ?? _activeType)
                    {
                        case CodeSetType.CodeC:
                            var value = _current.Code;
                            _output.AddRange(_encoding.GetBytes(value.ToString("00")));
                            break;
                        default:
                            _output.Add((byte) (_current.Code + _high));
                            break;
                    }


                    if (_highShift)
                    {
                        _high += 128;
                        _highShift = false;
                    }

                    if (_tempSet.HasValue)
                    {
                        _tempSet = null;
                        _activeCodeSet = _mapping.GetCodeSet(_activeType);
                    }

                    Consume();

                    break;
            }
        }

        private void HandleCodeSetSwitch(CodeSetType target)
        {
            if (_tempSet.HasValue)
            {
                throw new CodeError("Cannot switch code set after shift.");
            }

            if (_activeType == target)
            {
                throw new CodeError("Redundant code set switch.");
            }

            _activeType = target;
            _activeCodeSet = _mapping.GetCodeSet(_activeType);
            Consume();
        }

        private void HandleShift(CodeSetType expectedCurrentSet, CodeSetType targetSet)
        {
            if (_tempSet.HasValue)
            {
                throw new CodeError("Already in a shifting state.");
            }

            if (_activeType != expectedCurrentSet)
            {
                throw new CodeError($"Cannot shift to {targetSet} from current code set {_activeType}");
            }

            _tempSet = targetSet;
            _activeCodeSet = _mapping.GetCodeSet(_tempSet.Value);
            Consume();
        }


        private void MatchStart()
        {
            var (type, symbol) = DetermineType();
            if (symbol != null && type.HasValue)
            {
                Apply(symbol, type.Value);
                return;
            }

            // no match yet, maybe reversing helps
            _data.Reverse();
            (type, symbol) = DetermineType();
            if (symbol != null && type.HasValue)
            {
                Apply(symbol, type.Value);
                return;
            }

            throw new InvalidOperationException("Could not determine code set.");
        }

        private (CodeSetType?, Symbol) DetermineType()
        {
            var choices = new[]
            {
                Tuple.Create(SpecialCodes.StartA, CodeSetType.CodeA),
                Tuple.Create(SpecialCodes.StartB, CodeSetType.CodeB),
                Tuple.Create(SpecialCodes.StartC, CodeSetType.CodeC)
            };

            foreach (var choice in choices)
            {
                var symbol = _mapping.GetCodeSet(choice.Item2).GetSymbolForCode(choice.Item1);
                if (Match(symbol))
                {
                    return (choice.Item2, symbol);
                }
            }

            return (null, null);
        }

        private void Apply(Symbol symbol, CodeSetType type)
        {
            _current = symbol;
            _activeType = type;
            _tempSet = null;
            _activeCodeSet = _mapping.GetCodeSet(_activeType);
            Consume();
        }

        private bool Consume()
        {
            _consumed.Add(_current);
            _index += _current.ModuleData.Count;
            _current = Peek(0);

            if (_current != null)
            {
                return true;
            }

            return false;
        }


        private Symbol Peek(int offset)
        {
            var next = Next(offset);
            if (next == null)
            {
                // not enough data
                return null;
            }

            if (_activeCodeSet.TryGetSymbolForPattern(next, out var symbol))
            {
                return symbol;
            }

            return null;
        }

        private string Next(int offset, int count = 6)
        {
            if (!Has((offset + 1) * count))
            {
                return null;
            }

            return new string(Enumerable.Range(_index + offset * count, count).Select(c => (char) (_data[c] + '0'))
                .ToArray());
        }

        private bool Has(int count)
        {
            return _data.Count - _index >= count;
        }

        private bool Match(Symbol s)
        {
            if (!Has(s.ModuleData.Count))
            {
                return false;
            }

            for (var i = 0; i < s.ModuleData.Count; ++i)
            {
                if (_data[i + _index] != s.ModuleData[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}