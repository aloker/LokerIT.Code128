using System.Collections.Generic;

namespace LokerIT.Code128
{
    public interface IEncodedCode128
    {
        IReadOnlyList<byte> Data { get; }
        IReadOnlyList<byte> FullData { get; }
        string EncodedContent { get; }
        IReadOnlyList<Symbol> Symbols { get; }
        IEnumerable<string> ToPatternList();
    }
}