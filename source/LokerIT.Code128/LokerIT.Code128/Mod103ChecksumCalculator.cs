using System;
using System.Collections.Generic;

namespace LokerIT.Code128
{
    public class Mod103ChecksumCalculator : IChecksumCalculator
    {
        public byte CalculateChecksum(IReadOnlyList<byte> rawData)
        {
            if (rawData == null)
            {
                throw new ArgumentNullException(nameof(rawData));
            }

            if (rawData.Count == 0)
            {
                return 0;
            }

            long checkSumBuilder = rawData[0];
            for (var i = 1; i < rawData.Count; ++i)
            {
                checkSumBuilder += rawData[i] * i;
            }

            var checkSum = (byte) (checkSumBuilder % 103);
            return checkSum;
        }
    }
}