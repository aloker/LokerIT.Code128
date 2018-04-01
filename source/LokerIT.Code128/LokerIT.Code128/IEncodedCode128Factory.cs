using System.Collections.Generic;

namespace LokerIT.Code128
{
    public interface IEncodedCode128Factory
    {
        IEncodedCode128 Create(IMapping mapping, IEnumerable<Symbol> symbols, string content);
    }
}