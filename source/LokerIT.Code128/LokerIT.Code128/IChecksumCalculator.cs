using System.Collections.Generic;

namespace LokerIT.Code128
{
    public interface IChecksumCalculator
    {
        byte CalculateChecksum(IReadOnlyList<byte> buffer);
    }
}