using System.Collections.Generic;

namespace LokerIT.Code128.Nodes
{
    public class EmptyStringNode : StartNode
    {
        public static readonly EmptyStringNode Instance = new EmptyStringNode();
        public override bool IsHigh => false;
        public override CodeSetType FinalCodeSet => CodeSetType.CodeA;
        public override int Length => 0;
        public override int EmitCount => 1;
        public override bool IsTerminal => true;

        public override void Emit(IMapping mapping, byte[] input, ICollection<Symbol> buffer)
        {
            var set = mapping.GetCodeSet(FinalCodeSet);
            buffer.Add(set.GetSymbolForCode(set.StartCode));
        }
    }
}