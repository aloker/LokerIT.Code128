using System;
using System.Collections.Generic;
using System.Linq;

namespace LokerIT.Code128
{
    public class Symbol
    {
        private readonly byte[] _values;

        public Symbol(ushort code, IEnumerable<byte> values) : this(code, null, values)
        {
        }

        public Symbol(ushort code, string moduleString, IEnumerable<byte> values)
        {
            ModulePattern = moduleString;
            Code = code;
            _values = (values ?? throw new ArgumentNullException(nameof(values))).ToArray();
            ParseModuleString();
        }

        public Symbol(ushort code, params byte[] values) : this(code, null, values)
        {
        }

        public Symbol(ushort code, string moduleString, params byte[] values)
        {
            Code = code;
            ModulePattern = moduleString;
            _values = values ?? throw new ArgumentNullException(nameof(values));
            ParseModuleString();
        }

        public ushort Code { get; }

        public IReadOnlyList<byte> Values => _values;

        public IReadOnlyList<byte> ModuleData { get; private set; }

        public string ModulePattern { get; }

        private void ParseModuleString()
        {
            if (string.IsNullOrWhiteSpace(ModulePattern))
            {
                return;
            }

            var data = new List<byte>();
            foreach (var c in ModulePattern)
            {
                switch (c)
                {
                    case '1':
                        data.Add(1);
                        break;
                    case '2':
                        data.Add(2);
                        break;
                    case '3':
                        data.Add(3);
                        break;
                    case '4':
                        data.Add(4);
                        break;
                    default:
                        throw new ArgumentException($"Character '{c}' is not allowed in the module string");
                }
            }

            ModuleData = data;
        }

        public override string ToString()
        {
            return $"{Code:X4}: {string.Join(", ", Values)} ({ModulePattern})";
        }
    }
}