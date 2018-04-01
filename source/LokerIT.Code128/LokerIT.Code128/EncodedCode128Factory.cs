using System.Collections.Generic;

namespace LokerIT.Code128
{
    public class EncodedCode128Factory : IEncodedCode128Factory
    {
        public static readonly EncodedCode128Factory Default =
            new EncodedCode128Factory(checksumCalculator: new Mod103ChecksumCalculator());

        public static readonly EncodedCode128Factory EscPos =
            new EncodedCode128Factory(false);

        public EncodedCode128Factory(bool appendStopCode = true, IChecksumCalculator checksumCalculator = null)
        {
            AppendStopCode = appendStopCode;
            ChecksumCalculator = checksumCalculator;
        }

        public bool AppendStopCode { get; }

        public IChecksumCalculator ChecksumCalculator { get; }

        public IEncodedCode128 Create(IMapping mapping, IEnumerable<Symbol> symbols, string content)
        {
            return new EncodedCode128(mapping, symbols, content, ChecksumCalculator);
        }
    }
}