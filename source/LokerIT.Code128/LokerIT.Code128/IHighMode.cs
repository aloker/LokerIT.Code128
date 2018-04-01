using System;
using System.Collections.Generic;

namespace LokerIT.Code128
{
    public interface IHighMode
    {
        void Wrap(ICodeSet codeSet, ICollection<Symbol> buffer, IEnumerable<Action> emits);
        int GetOverhead(int length);
    }
}