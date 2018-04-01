using System;
using System.Collections.Generic;

namespace LokerIT.Code128.Nodes
{
    public static class FlushHelper
    {
        public static int FlushCodeSetC(IMapping mapping, byte[] input, ICollection<Symbol> buffer, int startIndex,
            int count)
        {
            var set = mapping.GetCodeSet(CodeSetType.CodeC);
            while (count >= 2)
            {
                var n = 10 * (input[startIndex] - '0') +
                        (input[startIndex + 1] - '0');
                var code = set.GetSymbolForCode((ushort) n);
                buffer.Add(code);
                startIndex += 2;
                count -= 2;
            }

            return startIndex;
        }


        public static IEnumerable<Action> CreateFlushEmits(ICodeSet set, byte[] input, ICollection<Symbol> buffer,
            int startIndex, int count)
        {
            while (count > 0)
            {
                var readIndex = startIndex;
                yield return () => buffer.Add(set.GetSymbolForCode(input[readIndex]));
                ++startIndex;
                --count;
            }
        }

        public static int FlushCodeSet(ICodeSet set, byte[] input, ICollection<Symbol> buffer, int startIndex,
            int count)
        {
            while (count > 0)
            {
                buffer.Add(set.GetSymbolForCode(input[startIndex]));
                ++startIndex;
                --count;
            }

            return startIndex;
        }
    }
}