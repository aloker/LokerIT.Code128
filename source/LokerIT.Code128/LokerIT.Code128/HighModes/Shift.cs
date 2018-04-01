using System;
using System.Collections.Generic;

namespace LokerIT.Code128.HighModes
{
    public class Shift : IHighMode
    {
        public static readonly Shift Instance = new Shift();
        public int Overhead => 1;

        public void Wrap(ICodeSet codeSet, ICollection<Symbol> buffer, IEnumerable<Action> emits)
        {
            var symbol = codeSet.GetSymbolForCode(SpecialCodes.Func4);

            foreach (var item in emits)
            {
                buffer.Add(symbol);
                item();
            }
        }

        public int GetOverhead(int length)
        {
            return length;
        }
    }
}