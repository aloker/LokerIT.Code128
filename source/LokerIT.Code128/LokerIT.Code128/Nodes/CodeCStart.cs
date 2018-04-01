using System.Collections.Generic;

namespace LokerIT.Code128.Nodes
{
    public class CodeCStart : StartNode
    {
        public CodeCStart(int length, bool isTerminal)
        {
            Length = length;
            IsTerminal = isTerminal;
        }

        public override bool IsHigh => false;
        public override CodeSetType FinalCodeSet => CodeSetType.CodeC;
        public override int Length { get; }
        public override int EmitCount => 1 + Length / 2;
        public override bool IsTerminal { get; }

        public override void Emit(IMapping mapping, byte[] input, ICollection<Symbol> buffer)
        {
            var set = mapping.GetCodeSet(CodeSetType.CodeC);
            buffer.Add(set.GetSymbolForCode(set.StartCode));
            FlushHelper.FlushCodeSetC(mapping, input, buffer, Start, Length);
        }
    }
}