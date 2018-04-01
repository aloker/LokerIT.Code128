using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LokerIT.Code128
{
    public class CodeSet : ICodeSet
    {
        private readonly List<Symbol> _symbols;
        private readonly Dictionary<ushort, Symbol> _symbolsByCode = new Dictionary<ushort, Symbol>();
        private readonly Dictionary<string, Symbol> _symbolsByPattern = new Dictionary<string, Symbol>();
        private readonly HashSet<byte> _textCodes;

        public CodeSet(ushort startCode, params Symbol[] symbols)
        {
            StartCode = startCode;
            _symbols = (symbols ?? throw new ArgumentNullException(nameof(symbols))).ToList();
            _textCodes = new HashSet<byte>(symbols.Where(c => c.Code <= byte.MaxValue).Select(c => (byte) c.Code));

            for (var i = 0; i < symbols.Length; i++)
            {
                var symbol = symbols[i];
                _symbolsByCode[symbol.Code] = symbol;
                if (!string.IsNullOrWhiteSpace(symbol.ModulePattern))
                {
                    _symbolsByPattern[symbol.ModulePattern] = symbol;
                }
            }
        }

        public ushort StartCode { get; }
        public ushort? SwitchToCode { get; set; }
        public ushort? ShiftOtherCode { get; set; }

        public IReadOnlyList<byte> GetValuesForCode(ushort code)
        {
            if (!TryGetValuesForCode(code, out var value))
            {
                throw new ArgumentOutOfRangeException(nameof(code));
            }

            return value;
        }

        public Symbol GetSymbolForPattern(string pattern)
        {
            if (!TryGetSymbolForPattern(pattern, out var symbol))
            {
                throw new ArgumentOutOfRangeException(nameof(pattern));
            }

            return symbol;
        }

        public bool TryGetSymbolForPattern(string pattern, out Symbol value)
        {
            return _symbolsByPattern.TryGetValue(pattern, out value);
        }

        public Symbol GetSymbolForCode(ushort code)
        {
            if (!TryGetSymbolForCode(code, out var symbol))
            {
                throw new ArgumentOutOfRangeException(nameof(code));
            }

            return symbol;
        }

        public bool TryGetValuesForCode(ushort code, out IReadOnlyList<byte> value)
        {
            value = null;
            if (_symbolsByCode.TryGetValue(code, out var symbol))
            {
                value = symbol.Values;
                return true;
            }

            return false;
        }

        public bool TryGetSymbolForCode(ushort code, out Symbol symbol)
        {
            return _symbolsByCode.TryGetValue(code, out symbol);
        }

        public bool Supports(byte c)
        {
            return _textCodes.Contains(c);
        }

        public bool Supports(string entry)
        {
            return Encoding.Default.GetBytes(entry).All(c => _textCodes.Contains(c));
        }

        public ICodeSet AddHigh()
        {
            foreach (var symbol in _symbols.ToArray().Where(c => c.Code <= 127)
                .Select(c => new Symbol((ushort) (c.Code + 128), c.ModulePattern, c.Values)))
            {
                _symbols.Add(symbol);
                _textCodes.Add((byte) symbol.Code);
                _symbolsByCode[symbol.Code] = symbol;
            }

            return this;
        }
    }
}