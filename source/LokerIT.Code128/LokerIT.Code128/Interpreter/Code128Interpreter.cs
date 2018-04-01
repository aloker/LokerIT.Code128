using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LokerIT.Code128.Interpreter
{
    public class Code128Interpreter
    {
        private readonly IChecksumCalculator _checksumCalculator;
        private readonly Encoding _encoding;
        private readonly IMapping _mapping;

        public Code128Interpreter(IMapping mapping = null, Encoding encoding = null,
            IChecksumCalculator checksumCalculator = null)
        {
            if (encoding != null && !encoding.IsSingleByte)
            {
                throw new ArgumentException("Encoding must be single byte");
            }

            _mapping = mapping ?? new DefaultMapping();
            _encoding = encoding ?? Encoding.Default;
            _checksumCalculator = checksumCalculator ?? new Mod103ChecksumCalculator();
        }

        public string Interpret(IEnumerable<string> patternList)
        {
            var chars = patternList.SelectMany(c => c).Select(c => (byte) (c - '0'));
            return Interpret(chars);
        }

        public string Interpret(IEnumerable<byte> bars)
        {
            return new Code128Interpretation(_mapping, _encoding, _checksumCalculator).Interpret(bars);
        }
    }
}