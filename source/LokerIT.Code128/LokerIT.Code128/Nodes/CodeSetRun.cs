using System;
using System.Collections.Generic;

namespace LokerIT.Code128.Nodes
{
    public class CodeSetRun : SuccessorNode
    {
        private readonly IHighMode _highMode;
        private readonly HighModeChange _highModeChange;
        private readonly bool _needsChangeOfSet;

        public CodeSetRun(INode predecessor, CodeSetType codeSet, int length, bool isTerminal,
            HighModeChange highModeChange) : base(predecessor)
        {
            _highModeChange = highModeChange;
            _needsChangeOfSet = predecessor.FinalCodeSet != codeSet;
            FinalCodeSet = codeSet;
            Length = length;
            IsTerminal = isTerminal;
            _highMode = highModeChange.Instantiate();
        }

        public override int Length { get; }
        public override int EmitCount => (_needsChangeOfSet ? 1 : 0) + Length + _highMode.GetOverhead(Length);
        public override bool IsTerminal { get; }
        public override bool IsHigh => Predecessor.IsHigh != (_highModeChange == HighModeChange.Toggle);
        public override CodeSetType FinalCodeSet { get; }

        public override void Emit(IMapping mapping, byte[] input, ICollection<Symbol> buffer)
        {
            if (_needsChangeOfSet)
            {
                var previousSet = mapping.GetCodeSet(Predecessor.FinalCodeSet);
                var mySet = mapping.GetCodeSet(FinalCodeSet);
                if (!mySet.SwitchToCode.HasValue)
                {
                    throw new InvalidOperationException($"Cannot switch to {FinalCodeSet} from ");
                }

                buffer.Add(previousSet.GetSymbolForCode(mySet.SwitchToCode.Value));
            }

            var set = mapping.GetCodeSet(FinalCodeSet);
            _highMode.Wrap(set, buffer, FlushHelper.CreateFlushEmits(set, input, buffer, Start, Length));
        }
    }
}