using System.Collections.Generic;

namespace LokerIT.Code128
{
    public interface IMapping
    {
        Symbol StopSymbol { get; }
        IReadOnlyList<Symbol> StartSymbols { get; }
        ICodeSet GetCodeSet(CodeSetType type);
        string GetPattern(byte value);
    }
}