using System;
using System.Collections.Generic;

namespace LokerIT.Code128.HighModes
{
    public class Keep : IHighMode
    {
        public static readonly Keep Instance = new Keep();

        public void Wrap(ICodeSet codeSet, ICollection<Symbol> buffer, IEnumerable<Action> emits)
        {
            foreach (var emit in emits)
            {
                emit();
            }
        }

        public int GetOverhead(int length)
        {
            return 0;
        }
    }
}