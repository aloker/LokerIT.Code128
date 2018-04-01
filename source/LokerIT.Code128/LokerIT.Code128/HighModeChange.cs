using System;
using LokerIT.Code128.HighModes;

namespace LokerIT.Code128
{
    public enum HighModeChange
    {
        Keep,
        Shift,
        Toggle
    }

    public static class HighModeExtensions
    {
        public static IHighMode Instantiate(this HighModeChange mode)
        {
            switch (mode)
            {
                case HighModeChange.Keep:
                    return Keep.Instance;
                case HighModeChange.Shift:
                    return Shift.Instance;
                case HighModeChange.Toggle:
                    return Toggle.Instance;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
    }
}