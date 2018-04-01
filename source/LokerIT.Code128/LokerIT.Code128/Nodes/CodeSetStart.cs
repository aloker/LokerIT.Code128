using System.Collections.Generic;

namespace LokerIT.Code128.Nodes
{
    public class CodeSetStart : StartNode
    {
        private readonly CodeSetType _codeSet;
        private readonly IHighMode _highMode;
        private readonly HighModeChange _highModeChange;

        public CodeSetStart(CodeSetType codeSet, int length, bool isTerminal, HighModeChange highModeChange)
        {
            _codeSet = codeSet;
            _highModeChange = highModeChange;
            _highMode = _highModeChange.Instantiate();
            Length = length;
            IsTerminal = isTerminal;
        }

        public override int Length { get; }
        public override int EmitCount => 1 + Length + _highMode.GetOverhead(Length);
        public override bool IsTerminal { get; }

        public override bool IsHigh => _highModeChange == HighModeChange.Toggle;

        public override CodeSetType FinalCodeSet => _codeSet;

        public override void Emit(IMapping mapping, byte[] input, ICollection<Symbol> buffer)
        {
            var set = mapping.GetCodeSet(_codeSet);
            buffer.Add(set.GetSymbolForCode(set.StartCode));
            _highMode.Wrap(set, buffer, FlushHelper.CreateFlushEmits(set, input, buffer, Start, Length));
        }
    }
}