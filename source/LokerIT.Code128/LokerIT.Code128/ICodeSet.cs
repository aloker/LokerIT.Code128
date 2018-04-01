using System.Collections.Generic;

namespace LokerIT.Code128
{
    public interface ICodeSet
    {
        ushort StartCode { get; }
        ushort? SwitchToCode { get; set; }
        ushort? ShiftOtherCode { get; set; }
        IReadOnlyList<byte> GetValuesForCode(ushort code);
        Symbol GetSymbolForCode(ushort code);
        bool TryGetValuesForCode(ushort code, out IReadOnlyList<byte> value);
        bool TryGetSymbolForCode(ushort code, out Symbol symbol);
        bool Supports(byte c);
        bool Supports(string entry);
        Symbol GetSymbolForPattern(string pattern);
        bool TryGetSymbolForPattern(string pattern, out Symbol value);
    }
}