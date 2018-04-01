using System.Collections.Generic;

namespace LokerIT.Code128.Nodes
{
    public class CodeCRun : SuccessorNode
    {
        private readonly bool _switchToSet;

        public CodeCRun(INode predecessor, int length, bool isTerminal) : base(predecessor)
        {
            _switchToSet = predecessor.FinalCodeSet != CodeSetType.CodeC;
            Length = length;
            IsTerminal = isTerminal;
        }

        public override bool IsHigh => Predecessor.IsHigh;
        public override CodeSetType FinalCodeSet => CodeSetType.CodeC;
        public override int Length { get; }
        public override int EmitCount => (_switchToSet ? 1 : 0) + Length / 2;
        public override bool IsTerminal { get; }

        public override void Emit(IMapping mapping, byte[] input, ICollection<Symbol> buffer)
        {
            if (_switchToSet)
            {
                var previousSet = mapping.GetCodeSet(Predecessor.FinalCodeSet);
                buffer.Add(previousSet.GetSymbolForCode(SpecialCodes.SwitchToCodeC));
            }

            FlushHelper.FlushCodeSetC(mapping, input, buffer, Start, Length);
        }
    }
}