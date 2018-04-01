using System;
using System.Collections.Generic;

namespace LokerIT.Code128
{
    /// <summary>
    ///     Encoding as used by ESC/P
    /// </summary>
    public class EscPosMapping : IMapping
    {
        public ICodeSet CodeA { get; } = new CodeSet(SpecialCodes.StartA,

            #region Symbols

            new Symbol((char) 0, 0), // NUL
            new Symbol((char) 1, 1), // SOH
            new Symbol((char) 2, 2), // STX
            new Symbol((char) 3, 3), // ETX
            new Symbol((char) 4, 4), // EOT
            new Symbol((char) 5, 5), // ENQ
            new Symbol((char) 6, 6), // ACK
            new Symbol((char) 7, 7), // BEL
            new Symbol((char) 8, 8), // BS
            new Symbol((char) 9, 9), // HT
            new Symbol((char) 10, 10), // LF
            new Symbol((char) 11, 11), // VT
            new Symbol((char) 12, 12), // FF
            new Symbol((char) 13, 13), // CR
            new Symbol((char) 14, 14), // SO
            new Symbol((char) 15, 15), // SI
            new Symbol((char) 16, 16), // DLE
            new Symbol((char) 17, 17), // DC1
            new Symbol((char) 18, 18), // DC2
            new Symbol((char) 19, 19), // DC3
            new Symbol((char) 20, 20), // DC4
            new Symbol((char) 21, 21), // NAK
            new Symbol((char) 22, 22), // SYN
            new Symbol((char) 23, 23), // ETB
            new Symbol((char) 24, 24), // CAN
            new Symbol((char) 25, 25), // EM
            new Symbol((char) 26, 26), // SUB
            new Symbol((char) 27, 27), // ESC
            new Symbol((char) 28, 28), // FS
            new Symbol((char) 29, 29), // GS
            new Symbol((char) 30, 30), // RS
            new Symbol((char) 31, 31), // US
            new Symbol(' ', 32),
            new Symbol('!', 33),
            new Symbol('"', 34),
            new Symbol('#', 35),
            new Symbol('$', 36),
            new Symbol('%', 37),
            new Symbol('&', 38),
            new Symbol('\'', 39),
            new Symbol('(', 40),
            new Symbol(')', 41),
            new Symbol('*', 42),
            new Symbol('+', 43),
            new Symbol(',', 44),
            new Symbol('-', 45),
            new Symbol('.', 46),
            new Symbol('/', 47),
            new Symbol('0', 48),
            new Symbol('1', 49),
            new Symbol('2', 50),
            new Symbol('3', 51),
            new Symbol('4', 52),
            new Symbol('5', 53),
            new Symbol('6', 54),
            new Symbol('7', 55),
            new Symbol('8', 56),
            new Symbol('9', 57),
            new Symbol(':', 58),
            new Symbol(';', 59),
            new Symbol('<', 60),
            new Symbol('=', 61),
            new Symbol('>', 62),
            new Symbol('?', 63),
            new Symbol('@', 64),
            new Symbol('A', 65),
            new Symbol('B', 66),
            new Symbol('C', 67),
            new Symbol('D', 68),
            new Symbol('E', 69),
            new Symbol('F', 70),
            new Symbol('G', 71),
            new Symbol('H', 72),
            new Symbol('I', 73),
            new Symbol('J', 74),
            new Symbol('K', 75),
            new Symbol('L', 76),
            new Symbol('M', 77),
            new Symbol('N', 78),
            new Symbol('O', 79),
            new Symbol('P', 80),
            new Symbol('Q', 81),
            new Symbol('R', 82),
            new Symbol('S', 83),
            new Symbol('T', 84),
            new Symbol('U', 85),
            new Symbol('V', 86),
            new Symbol('W', 87),
            new Symbol('X', 88),
            new Symbol('Y', 89),
            new Symbol('Z', 90),
            new Symbol('[', 91),
            new Symbol('\\', 92),
            new Symbol(']', 93),
            new Symbol('^', 94),
            new Symbol('_', 95),
            new Symbol(SpecialCodes.Func1, 123, 49),
            new Symbol(SpecialCodes.Func2, 123, 50),
            new Symbol(SpecialCodes.Func3, 123, 51),
            new Symbol(SpecialCodes.Func4, 123, 52),
            new Symbol(SpecialCodes.ShiftToB, 123, 83),
            new Symbol(SpecialCodes.SwitchToCodeB, 123, 66),
            new Symbol(SpecialCodes.SwitchToCodeC, 123, 67),
            new Symbol(SpecialCodes.StartA, 123, 65),
            new Symbol(SpecialCodes.StartB, 123, 66),
            new Symbol(SpecialCodes.StartC, 123, 67),
            new Symbol(SpecialCodes.Stop, 123, 106)

            #endregion

        )
        {
            ShiftOtherCode = SpecialCodes.ShiftToB,
            SwitchToCode = SpecialCodes.SwitchToCodeA
        };

        public ICodeSet CodeB { get; } = new CodeSet(SpecialCodes.StartB,

            #region Symbols

            new Symbol(' ', 32),
            new Symbol('!', 33),
            new Symbol('"', 34),
            new Symbol('#', 35),
            new Symbol('$', 36),
            new Symbol('%', 37),
            new Symbol('&', 38),
            new Symbol('\'', 39),
            new Symbol('(', 40),
            new Symbol(')', 41),
            new Symbol('*', 42),
            new Symbol('+', 43),
            new Symbol(',', 44),
            new Symbol('-', 45),
            new Symbol('.', 46),
            new Symbol('/', 47),
            new Symbol('0', 48),
            new Symbol('1', 49),
            new Symbol('2', 50),
            new Symbol('3', 51),
            new Symbol('4', 52),
            new Symbol('5', 53),
            new Symbol('6', 54),
            new Symbol('7', 55),
            new Symbol('8', 56),
            new Symbol('9', 57),
            new Symbol(':', 58),
            new Symbol(';', 59),
            new Symbol('<', 60),
            new Symbol('=', 61),
            new Symbol('>', 62),
            new Symbol('?', 63),
            new Symbol('@', 64),
            new Symbol('A', 65),
            new Symbol('B', 66),
            new Symbol('C', 67),
            new Symbol('D', 68),
            new Symbol('E', 69),
            new Symbol('F', 70),
            new Symbol('G', 71),
            new Symbol('H', 72),
            new Symbol('I', 73),
            new Symbol('J', 74),
            new Symbol('K', 75),
            new Symbol('L', 76),
            new Symbol('M', 77),
            new Symbol('N', 78),
            new Symbol('O', 79),
            new Symbol('P', 80),
            new Symbol('Q', 81),
            new Symbol('R', 82),
            new Symbol('S', 83),
            new Symbol('T', 84),
            new Symbol('U', 85),
            new Symbol('V', 86),
            new Symbol('W', 87),
            new Symbol('X', 88),
            new Symbol('Y', 89),
            new Symbol('Z', 90),
            new Symbol('[', 91),
            new Symbol('\\', 92),
            new Symbol(']', 93),
            new Symbol('^', 94),
            new Symbol('_', 95),
            new Symbol('`', 96),
            new Symbol('a', 97),
            new Symbol('b', 98),
            new Symbol('c', 99),
            new Symbol('d', 100),
            new Symbol('e', 101),
            new Symbol('f', 102),
            new Symbol('g', 103),
            new Symbol('h', 104),
            new Symbol('i', 105),
            new Symbol('j', 106),
            new Symbol('k', 107),
            new Symbol('l', 108),
            new Symbol('m', 109),
            new Symbol('n', 110),
            new Symbol('o', 111),
            new Symbol('p', 112),
            new Symbol('q', 113),
            new Symbol('r', 114),
            new Symbol('s', 115),
            new Symbol('t', 116),
            new Symbol('u', 117),
            new Symbol('v', 118),
            new Symbol('w', 119),
            new Symbol('x', 120),
            new Symbol('y', 121),
            new Symbol('z', 122),
            new Symbol('{', 123, 123),
            new Symbol('|', 124),
            new Symbol('}', 125),
            new Symbol('~', 126),
            new Symbol((char) 127, 127),
            new Symbol(SpecialCodes.Func1, 123, 49),
            new Symbol(SpecialCodes.Func2, 123, 50),
            new Symbol(SpecialCodes.Func3, 123, 51),
            new Symbol(SpecialCodes.Func4, 123, 52),
            new Symbol(SpecialCodes.ShiftToA, 123, 83),
            new Symbol(SpecialCodes.SwitchToCodeA, 123, 65),
            new Symbol(SpecialCodes.SwitchToCodeC, 123, 67),
            new Symbol(SpecialCodes.StartA, 123, 65),
            new Symbol(SpecialCodes.StartB, 123, 66),
            new Symbol(SpecialCodes.StartC, 123, 67),
            new Symbol(SpecialCodes.Stop, 123, 106)

            #endregion

        )
        {
            ShiftOtherCode = SpecialCodes.ShiftToA,
            SwitchToCode = SpecialCodes.SwitchToCodeB
        };

        public ICodeSet CodeC { get; } = new CodeSet(SpecialCodes.StartC,

            #region Symbols

            new Symbol((char) 0, 0),
            new Symbol((char) 1, 1),
            new Symbol((char) 2, 2),
            new Symbol((char) 3, 3),
            new Symbol((char) 4, 4),
            new Symbol((char) 5, 5),
            new Symbol((char) 6, 6),
            new Symbol((char) 7, 7),
            new Symbol((char) 8, 8),
            new Symbol((char) 9, 9),
            new Symbol((char) 10, 10),
            new Symbol((char) 11, 11),
            new Symbol((char) 12, 12),
            new Symbol((char) 13, 13),
            new Symbol((char) 14, 14),
            new Symbol((char) 15, 15),
            new Symbol((char) 16, 16),
            new Symbol((char) 17, 17),
            new Symbol((char) 18, 18),
            new Symbol((char) 19, 19),
            new Symbol((char) 20, 20),
            new Symbol((char) 21, 21),
            new Symbol((char) 22, 22),
            new Symbol((char) 23, 23),
            new Symbol((char) 24, 24),
            new Symbol((char) 25, 25),
            new Symbol((char) 26, 26),
            new Symbol((char) 27, 27),
            new Symbol((char) 28, 28),
            new Symbol((char) 29, 29),
            new Symbol((char) 30, 30),
            new Symbol((char) 31, 31),
            new Symbol((char) 32, 32),
            new Symbol((char) 33, 33),
            new Symbol((char) 34, 34),
            new Symbol((char) 35, 35),
            new Symbol((char) 36, 36),
            new Symbol((char) 37, 37),
            new Symbol((char) 38, 38),
            new Symbol((char) 39, 39),
            new Symbol((char) 40, 40),
            new Symbol((char) 41, 41),
            new Symbol((char) 42, 42),
            new Symbol((char) 43, 43),
            new Symbol((char) 44, 44),
            new Symbol((char) 45, 45),
            new Symbol((char) 46, 46),
            new Symbol((char) 47, 47),
            new Symbol((char) 48, 48),
            new Symbol((char) 49, 49),
            new Symbol((char) 50, 50),
            new Symbol((char) 51, 51),
            new Symbol((char) 52, 52),
            new Symbol((char) 53, 53),
            new Symbol((char) 54, 54),
            new Symbol((char) 55, 55),
            new Symbol((char) 56, 56),
            new Symbol((char) 57, 57),
            new Symbol((char) 58, 58),
            new Symbol((char) 59, 59),
            new Symbol((char) 60, 60),
            new Symbol((char) 61, 61),
            new Symbol((char) 62, 62),
            new Symbol((char) 63, 63),
            new Symbol((char) 64, 64),
            new Symbol((char) 65, 65),
            new Symbol((char) 66, 66),
            new Symbol((char) 67, 67),
            new Symbol((char) 68, 68),
            new Symbol((char) 69, 69),
            new Symbol((char) 70, 70),
            new Symbol((char) 71, 71),
            new Symbol((char) 72, 72),
            new Symbol((char) 73, 73),
            new Symbol((char) 74, 74),
            new Symbol((char) 75, 75),
            new Symbol((char) 76, 76),
            new Symbol((char) 77, 77),
            new Symbol((char) 78, 78),
            new Symbol((char) 79, 79),
            new Symbol((char) 80, 80),
            new Symbol((char) 81, 81),
            new Symbol((char) 82, 82),
            new Symbol((char) 83, 83),
            new Symbol((char) 84, 84),
            new Symbol((char) 85, 85),
            new Symbol((char) 86, 86),
            new Symbol((char) 87, 87),
            new Symbol((char) 88, 88),
            new Symbol((char) 89, 89),
            new Symbol((char) 90, 90),
            new Symbol((char) 91, 91),
            new Symbol((char) 92, 92),
            new Symbol((char) 93, 93),
            new Symbol((char) 94, 94),
            new Symbol((char) 95, 95),
            new Symbol((char) 96, 96),
            new Symbol((char) 97, 97),
            new Symbol((char) 98, 98),
            new Symbol((char) 99, 99),
            new Symbol(SpecialCodes.Func1, 123, 49),
            new Symbol(SpecialCodes.SwitchToCodeA, 123, 65),
            new Symbol(SpecialCodes.SwitchToCodeB, 123, 66),
            new Symbol(SpecialCodes.StartA, 123, 65),
            new Symbol(SpecialCodes.StartB, 123, 66),
            new Symbol(SpecialCodes.StartC, 123, 67),
            new Symbol(SpecialCodes.Stop, 123, 106)

            #endregion

        );

        public ICodeSet GetCodeSet(CodeSetType type)
        {
            switch (type)
            {
                case CodeSetType.CodeA:
                    return CodeA;
                case CodeSetType.CodeB:
                    return CodeB;
                case CodeSetType.CodeC:
                    return CodeC;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public Symbol StopSymbol { get; } = null;
        public IReadOnlyList<Symbol> StartSymbols { get; } = new List<Symbol>();

        public string GetPattern(byte value)
        {
            throw new NotSupportedException();
        }
    }
}