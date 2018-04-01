using System.Collections.Generic;
using System.Linq;

namespace LokerIT.Code128
{
    public class EncodedCode128 : IEncodedCode128
    {
        public EncodedCode128(IMapping mapping,
            IEnumerable<Symbol> symbols,
            string encodedContent,
            IChecksumCalculator calculator)
        {
            var symbolList = symbols.ToList();
            Symbols = symbolList;
            Data = Symbols.SelectMany(c => c.Values).ToList();
            EncodedContent = encodedContent;
            if (calculator != null)
            {
                var checkSum = calculator.CalculateChecksum(Data);
                var checkSumSymbol = new Symbol(SpecialCodes.CheckSum, mapping.GetPattern(checkSum), checkSum);
                symbolList.Add(checkSumSymbol);
                symbolList.Add(mapping.StopSymbol);
            }

            FullData = Symbols.SelectMany(c => c.Values).ToList();
        }


        public IReadOnlyList<Symbol> Symbols { get; }

        public IEnumerable<string> ToPatternList()
        {
            return Symbols.Select(c => c.ModulePattern);
        }

        public IReadOnlyList<byte> Data { get; }

        public IReadOnlyList<byte> FullData { get; }

        public string EncodedContent { get; }
    }
}