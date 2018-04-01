using System;
using System.Collections.Generic;

namespace LokerIT.Code128.HighModes
{
    public class Toggle : IHighMode
    {
        public static readonly Toggle Instance = new Toggle();

        public int Overhead => 2;

        public void Wrap(ICodeSet codeSet, ICollection<Symbol> buffer, IEnumerable<Action> emits)
        {
            var symbol = codeSet.GetSymbolForCode(SpecialCodes.Func4);
            buffer.Add(symbol);
            buffer.Add(symbol);
            foreach (var action in emits)
            {
                action();
            }
        }

        public int GetOverhead(int length)
        {
            return 2;
        }
    }
}