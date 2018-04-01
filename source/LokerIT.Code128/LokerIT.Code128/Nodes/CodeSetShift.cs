using System;
using System.Collections.Generic;
using System.Linq;

namespace LokerIT.Code128.Nodes
{
    public class CodeSetShift : SuccessorNode
    {
        private readonly CodeSetType _codeSet;
        private readonly IHighMode _highMode;
        private readonly HighModeChange _highModeChange;

        public CodeSetShift(INode predecessor, CodeSetType codeSet, HighModeChange highModeChange) : base(predecessor)
        {
            _codeSet = codeSet;
            _highModeChange = highModeChange;
            FinalCodeSet = predecessor.FinalCodeSet;
            Length = 1;
            _highMode = highModeChange.Instantiate();
        }

        public override int Length { get; }
        public override int EmitCount => 2 + _highMode.GetOverhead(Length);
        public override bool IsTerminal => false;
        public override bool IsHigh => Predecessor.IsHigh;
        public override CodeSetType FinalCodeSet { get; }

        public override void Emit(IMapping mapping, byte[] input, ICollection<Symbol> buffer)
        {
            var previousSet = mapping.GetCodeSet(Predecessor.FinalCodeSet);
            var mySet = mapping.GetCodeSet(_codeSet);
            if (!previousSet.ShiftOtherCode.HasValue)
            {
                throw new InvalidOperationException($"Cannot switch to {_codeSet} from ");
            }

            var actions = Enumerable.Range(Start, Length).Select(index => new Action(() =>
            {
                buffer.Add(previousSet.GetSymbolForCode(previousSet.ShiftOtherCode.Value));
                buffer.Add(mySet.GetSymbolForCode(input[Start]));
            }));
            _highMode.Wrap(previousSet, buffer, actions);
        }
    }
}