using System;
using System.Collections.Generic;

namespace LokerIT.Code128
{
    public class DefaultMapping : IMapping
    {
        private static readonly string[] Patterns =
        {
            "212222",
            "222122",
            "222221",
            "121223",
            "121322",
            "131222",
            "122213",
            "122312",
            "132212",
            "221213",
            "221312",
            "231212",
            "112232",
            "122132",
            "122231",
            "113222",
            "123122",
            "123221",
            "223211",
            "221132",
            "221231",
            "213212",
            "223112",
            "312131",
            "311222",
            "321122",
            "321221",
            "312212",
            "322112",
            "322211",
            "212123",
            "212321",
            "232121",
            "111323",
            "131123",
            "131321",
            "112313",
            "132113",
            "132311",
            "211313",
            "231113",
            "231311",
            "112133",
            "112331",
            "132131",
            "113123",
            "113321",
            "133121",
            "313121",
            "211331",
            "231131",
            "213113",
            "213311",
            "213131",
            "311123",
            "311321",
            "331121",
            "312113",
            "312311",
            "332111",
            "314111",
            "221411",
            "431111",
            "111224",
            "111422",
            "121124",
            "121421",
            "141122",
            "141221",
            "112214",
            "112412",
            "122114",
            "122411",
            "142112",
            "142211",
            "241211",
            "221114",
            "413111",
            "241112",
            "134111",
            "111242",
            "121142",
            "121241",
            "114212",
            "124112",
            "124211",
            "411212",
            "421112",
            "421211",
            "212141",
            "214121",
            "412121",
            "111143",
            "111341",
            "131141",
            "114113",
            "114311",
            "411113",
            "411311",
            "113141",
            "114131",
            "311141",
            "411131",
            "211412",
            "211214",
            "211232",
            "233111",
            "2331112"
        };

        public DefaultMapping()
        {
            StopSymbol = new Symbol(SpecialCodes.Stop, Patterns[107], 106);
            StartSymbols = new List<Symbol>
            {
                new Symbol(SpecialCodes.StartA, Patterns[103], 103),
                new Symbol(SpecialCodes.StartB, Patterns[104], 104),
                new Symbol(SpecialCodes.StartC, Patterns[105], 105)
            };
            ;
            CodeA = new CodeSet(SpecialCodes.StartA,

                #region Symbols

                new Symbol(' ', Patterns[0], 0),
                new Symbol('!', Patterns[1], 1),
                new Symbol('"', Patterns[2], 2),
                new Symbol('#', Patterns[3], 3),
                new Symbol('$', Patterns[4], 4),
                new Symbol('%', Patterns[5], 5),
                new Symbol('&', Patterns[6], 6),
                new Symbol('\'', Patterns[7], 7),
                new Symbol('(', Patterns[8], 8),
                new Symbol(')', Patterns[9], 9),
                new Symbol('*', Patterns[10], 10),
                new Symbol('+', Patterns[11], 11),
                new Symbol(',', Patterns[12], 12),
                new Symbol('-', Patterns[13], 13),
                new Symbol('.', Patterns[14], 14),
                new Symbol('/', Patterns[15], 15),
                new Symbol('0', Patterns[16], 16),
                new Symbol('1', Patterns[17], 17),
                new Symbol('2', Patterns[18], 18),
                new Symbol('3', Patterns[19], 19),
                new Symbol('4', Patterns[20], 20),
                new Symbol('5', Patterns[21], 21),
                new Symbol('6', Patterns[22], 22),
                new Symbol('7', Patterns[23], 23),
                new Symbol('8', Patterns[24], 24),
                new Symbol('9', Patterns[25], 25),
                new Symbol(':', Patterns[26], 26),
                new Symbol(';', Patterns[27], 27),
                new Symbol('<', Patterns[28], 28),
                new Symbol('=', Patterns[29], 29),
                new Symbol('>', Patterns[30], 30),
                new Symbol('?', Patterns[31], 31),
                new Symbol('@', Patterns[32], 32),
                new Symbol('A', Patterns[33], 33),
                new Symbol('B', Patterns[34], 34),
                new Symbol('C', Patterns[35], 35),
                new Symbol('D', Patterns[36], 36),
                new Symbol('E', Patterns[37], 37),
                new Symbol('F', Patterns[38], 38),
                new Symbol('G', Patterns[39], 39),
                new Symbol('H', Patterns[40], 40),
                new Symbol('I', Patterns[41], 41),
                new Symbol('J', Patterns[42], 42),
                new Symbol('K', Patterns[43], 43),
                new Symbol('L', Patterns[44], 44),
                new Symbol('M', Patterns[45], 45),
                new Symbol('N', Patterns[46], 46),
                new Symbol('O', Patterns[47], 47),
                new Symbol('P', Patterns[48], 48),
                new Symbol('Q', Patterns[49], 49),
                new Symbol('R', Patterns[50], 50),
                new Symbol('S', Patterns[51], 51),
                new Symbol('T', Patterns[52], 52),
                new Symbol('U', Patterns[53], 53),
                new Symbol('V', Patterns[54], 54),
                new Symbol('W', Patterns[55], 55),
                new Symbol('X', Patterns[56], 56),
                new Symbol('Y', Patterns[57], 57),
                new Symbol('Z', Patterns[58], 58),
                new Symbol('[', Patterns[59], 59),
                new Symbol('\\', Patterns[60], 60),
                new Symbol(']', Patterns[61], 61),
                new Symbol('^', Patterns[62], 62),
                new Symbol('_', Patterns[63], 63),
                new Symbol(0, Patterns[64], 64), // NUL
                new Symbol(1, Patterns[65], 65), // SOH
                new Symbol(2, Patterns[66], 66), // STX
                new Symbol(3, Patterns[67], 67), // ETX
                new Symbol(4, Patterns[68], 68), // EOT
                new Symbol(5, Patterns[69], 69), // ENQ
                new Symbol(6, Patterns[70], 70), // ACK
                new Symbol(7, Patterns[71], 71), // BEL
                new Symbol(8, Patterns[72], 72), // BS
                new Symbol(9, Patterns[73], 73), // HT
                new Symbol(10, Patterns[74], 74), // LF
                new Symbol(11, Patterns[75], 75), // VT
                new Symbol(12, Patterns[76], 76), // FF
                new Symbol(13, Patterns[77], 77), // CR
                new Symbol(14, Patterns[78], 78), // SO
                new Symbol(15, Patterns[79], 79), // SI
                new Symbol(16, Patterns[80], 80), // DLE
                new Symbol(17, Patterns[81], 81), // DC1
                new Symbol(18, Patterns[82], 82), // DC2
                new Symbol(19, Patterns[83], 83), // DC3
                new Symbol(20, Patterns[84], 84), // DC4
                new Symbol(21, Patterns[85], 85), // NAK
                new Symbol(22, Patterns[86], 86), // SYN
                new Symbol(23, Patterns[87], 87), // ETB
                new Symbol(24, Patterns[88], 88), // CAN
                new Symbol(25, Patterns[89], 89), // EM
                new Symbol(26, Patterns[90], 90), // SUB
                new Symbol(27, Patterns[91], 91), // ESC
                new Symbol(28, Patterns[92], 92), // FS
                new Symbol(29, Patterns[93], 93), // GS
                new Symbol(30, Patterns[94], 94), // RS
                new Symbol(31, Patterns[95], 95), // US
                new Symbol(SpecialCodes.Func3, Patterns[96], 96),
                new Symbol(SpecialCodes.Func2, Patterns[97], 97),
                new Symbol(SpecialCodes.ShiftToB, Patterns[98], 98),
                new Symbol(SpecialCodes.SwitchToCodeC, Patterns[99], 99),
                new Symbol(SpecialCodes.SwitchToCodeB, Patterns[100], 100),
                new Symbol(SpecialCodes.Func4, Patterns[101], 101),
                new Symbol(SpecialCodes.Func1, Patterns[102], 102),
                new Symbol(SpecialCodes.StartA, Patterns[103], 103),
                new Symbol(SpecialCodes.StartB, Patterns[104], 104),
                new Symbol(SpecialCodes.StartC, Patterns[105], 105),
                new Symbol(SpecialCodes.Stop, Patterns[106], 106)

                #endregion

            )
            {
                ShiftOtherCode = SpecialCodes.ShiftToB,
                SwitchToCode = SpecialCodes.SwitchToCodeA
            }.AddHigh();
            CodeB = new CodeSet(SpecialCodes.StartB,

                #region Symbols

                new Symbol(' ', Patterns[0], 0),
                new Symbol('!', Patterns[1], 1),
                new Symbol('"', Patterns[2], 2),
                new Symbol('#', Patterns[3], 3),
                new Symbol('$', Patterns[4], 4),
                new Symbol('%', Patterns[5], 5),
                new Symbol('&', Patterns[6], 6),
                new Symbol('\'', Patterns[7], 7),
                new Symbol('(', Patterns[8], 8),
                new Symbol(')', Patterns[9], 9),
                new Symbol('*', Patterns[10], 10),
                new Symbol('+', Patterns[11], 11),
                new Symbol(',', Patterns[12], 12),
                new Symbol('-', Patterns[13], 13),
                new Symbol('.', Patterns[14], 14),
                new Symbol('/', Patterns[15], 15),
                new Symbol('0', Patterns[16], 16),
                new Symbol('1', Patterns[17], 17),
                new Symbol('2', Patterns[18], 18),
                new Symbol('3', Patterns[19], 19),
                new Symbol('4', Patterns[20], 20),
                new Symbol('5', Patterns[21], 21),
                new Symbol('6', Patterns[22], 22),
                new Symbol('7', Patterns[23], 23),
                new Symbol('8', Patterns[24], 24),
                new Symbol('9', Patterns[25], 25),
                new Symbol(':', Patterns[26], 26),
                new Symbol(';', Patterns[27], 27),
                new Symbol('<', Patterns[28], 28),
                new Symbol('=', Patterns[29], 29),
                new Symbol('>', Patterns[30], 30),
                new Symbol('?', Patterns[31], 31),
                new Symbol('@', Patterns[32], 32),
                new Symbol('A', Patterns[33], 33),
                new Symbol('B', Patterns[34], 34),
                new Symbol('C', Patterns[35], 35),
                new Symbol('D', Patterns[36], 36),
                new Symbol('E', Patterns[37], 37),
                new Symbol('F', Patterns[38], 38),
                new Symbol('G', Patterns[39], 39),
                new Symbol('H', Patterns[40], 40),
                new Symbol('I', Patterns[41], 41),
                new Symbol('J', Patterns[42], 42),
                new Symbol('K', Patterns[43], 43),
                new Symbol('L', Patterns[44], 44),
                new Symbol('M', Patterns[45], 45),
                new Symbol('N', Patterns[46], 46),
                new Symbol('O', Patterns[47], 47),
                new Symbol('P', Patterns[48], 48),
                new Symbol('Q', Patterns[49], 49),
                new Symbol('R', Patterns[50], 50),
                new Symbol('S', Patterns[51], 51),
                new Symbol('T', Patterns[52], 52),
                new Symbol('U', Patterns[53], 53),
                new Symbol('V', Patterns[54], 54),
                new Symbol('W', Patterns[55], 55),
                new Symbol('X', Patterns[56], 56),
                new Symbol('Y', Patterns[57], 57),
                new Symbol('Z', Patterns[58], 58),
                new Symbol('[', Patterns[59], 59),
                new Symbol('\\', Patterns[60], 60),
                new Symbol(']', Patterns[61], 61),
                new Symbol('^', Patterns[62], 62),
                new Symbol('_', Patterns[63], 63),
                new Symbol('`', Patterns[64], 64),
                new Symbol('a', Patterns[65], 65),
                new Symbol('b', Patterns[66], 66),
                new Symbol('c', Patterns[67], 67),
                new Symbol('d', Patterns[68], 68),
                new Symbol('e', Patterns[69], 69),
                new Symbol('f', Patterns[70], 70),
                new Symbol('g', Patterns[71], 71),
                new Symbol('h', Patterns[72], 72),
                new Symbol('i', Patterns[73], 73),
                new Symbol('j', Patterns[74], 74),
                new Symbol('k', Patterns[75], 75),
                new Symbol('l', Patterns[76], 76),
                new Symbol('m', Patterns[77], 77),
                new Symbol('n', Patterns[78], 78),
                new Symbol('o', Patterns[79], 79),
                new Symbol('p', Patterns[80], 80),
                new Symbol('q', Patterns[81], 81),
                new Symbol('r', Patterns[82], 82),
                new Symbol('s', Patterns[83], 83),
                new Symbol('t', Patterns[84], 84),
                new Symbol('u', Patterns[85], 85),
                new Symbol('v', Patterns[86], 86),
                new Symbol('w', Patterns[87], 87),
                new Symbol('x', Patterns[88], 88),
                new Symbol('y', Patterns[89], 89),
                new Symbol('z', Patterns[90], 90),
                new Symbol('{', Patterns[91], 91),
                new Symbol('|', Patterns[92], 92),
                new Symbol('}', Patterns[93], 93),
                new Symbol('~', Patterns[94], 94),
                new Symbol((char) 127, Patterns[95], 95), // DEL
                new Symbol(SpecialCodes.Func3, Patterns[96], 96),
                new Symbol(SpecialCodes.Func2, Patterns[97], 97),
                new Symbol(SpecialCodes.ShiftToA, Patterns[98], 98),
                new Symbol(SpecialCodes.SwitchToCodeC, Patterns[99], 99),
                new Symbol(SpecialCodes.Func4, Patterns[100], 100),
                new Symbol(SpecialCodes.SwitchToCodeA, Patterns[101], 101),
                new Symbol(SpecialCodes.Func1, Patterns[102], 102),
                new Symbol(SpecialCodes.StartA, Patterns[103], 103),
                new Symbol(SpecialCodes.StartB, Patterns[104], 104),
                new Symbol(SpecialCodes.StartC, Patterns[105], 105),
                new Symbol(SpecialCodes.Stop, Patterns[106], 106)

                #endregion

            )
            {
                ShiftOtherCode = SpecialCodes.ShiftToA,
                SwitchToCode = SpecialCodes.SwitchToCodeB
            }.AddHigh();

            CodeC = new CodeSet(SpecialCodes.StartC,

                #region Symbols

                new Symbol((char) 0, Patterns[0], 0),
                new Symbol((char) 1, Patterns[1], 1),
                new Symbol((char) 2, Patterns[2], 2),
                new Symbol((char) 3, Patterns[3], 3),
                new Symbol((char) 4, Patterns[4], 4),
                new Symbol((char) 5, Patterns[5], 5),
                new Symbol((char) 6, Patterns[6], 6),
                new Symbol((char) 7, Patterns[7], 7),
                new Symbol((char) 8, Patterns[8], 8),
                new Symbol((char) 9, Patterns[9], 9),
                new Symbol((char) 10, Patterns[10], 10),
                new Symbol((char) 11, Patterns[11], 11),
                new Symbol((char) 12, Patterns[12], 12),
                new Symbol((char) 13, Patterns[13], 13),
                new Symbol((char) 14, Patterns[14], 14),
                new Symbol((char) 15, Patterns[15], 15),
                new Symbol((char) 16, Patterns[16], 16),
                new Symbol((char) 17, Patterns[17], 17),
                new Symbol((char) 18, Patterns[18], 18),
                new Symbol((char) 19, Patterns[19], 19),
                new Symbol((char) 20, Patterns[20], 20),
                new Symbol((char) 21, Patterns[21], 21),
                new Symbol((char) 22, Patterns[22], 22),
                new Symbol((char) 23, Patterns[23], 23),
                new Symbol((char) 24, Patterns[24], 24),
                new Symbol((char) 25, Patterns[25], 25),
                new Symbol((char) 26, Patterns[26], 26),
                new Symbol((char) 27, Patterns[27], 27),
                new Symbol((char) 28, Patterns[28], 28),
                new Symbol((char) 29, Patterns[29], 29),
                new Symbol((char) 30, Patterns[30], 30),
                new Symbol((char) 31, Patterns[31], 31),
                new Symbol((char) 32, Patterns[32], 32),
                new Symbol((char) 33, Patterns[33], 33),
                new Symbol((char) 34, Patterns[34], 34),
                new Symbol((char) 35, Patterns[35], 35),
                new Symbol((char) 36, Patterns[36], 36),
                new Symbol((char) 37, Patterns[37], 37),
                new Symbol((char) 38, Patterns[38], 38),
                new Symbol((char) 39, Patterns[39], 39),
                new Symbol((char) 40, Patterns[40], 40),
                new Symbol((char) 41, Patterns[41], 41),
                new Symbol((char) 42, Patterns[42], 42),
                new Symbol((char) 43, Patterns[43], 43),
                new Symbol((char) 44, Patterns[44], 44),
                new Symbol((char) 45, Patterns[45], 45),
                new Symbol((char) 46, Patterns[46], 46),
                new Symbol((char) 47, Patterns[47], 47),
                new Symbol((char) 48, Patterns[48], 48),
                new Symbol((char) 49, Patterns[49], 49),
                new Symbol((char) 50, Patterns[50], 50),
                new Symbol((char) 51, Patterns[51], 51),
                new Symbol((char) 52, Patterns[52], 52),
                new Symbol((char) 53, Patterns[53], 53),
                new Symbol((char) 54, Patterns[54], 54),
                new Symbol((char) 55, Patterns[55], 55),
                new Symbol((char) 56, Patterns[56], 56),
                new Symbol((char) 57, Patterns[57], 57),
                new Symbol((char) 58, Patterns[58], 58),
                new Symbol((char) 59, Patterns[59], 59),
                new Symbol((char) 60, Patterns[60], 60),
                new Symbol((char) 61, Patterns[61], 61),
                new Symbol((char) 62, Patterns[62], 62),
                new Symbol((char) 63, Patterns[63], 63),
                new Symbol((char) 64, Patterns[64], 64),
                new Symbol((char) 65, Patterns[65], 65),
                new Symbol((char) 66, Patterns[66], 66),
                new Symbol((char) 67, Patterns[67], 67),
                new Symbol((char) 68, Patterns[68], 68),
                new Symbol((char) 69, Patterns[69], 69),
                new Symbol((char) 70, Patterns[70], 70),
                new Symbol((char) 71, Patterns[71], 71),
                new Symbol((char) 72, Patterns[72], 72),
                new Symbol((char) 73, Patterns[73], 73),
                new Symbol((char) 74, Patterns[74], 74),
                new Symbol((char) 75, Patterns[75], 75),
                new Symbol((char) 76, Patterns[76], 76),
                new Symbol((char) 77, Patterns[77], 77),
                new Symbol((char) 78, Patterns[78], 78),
                new Symbol((char) 79, Patterns[79], 79),
                new Symbol((char) 80, Patterns[80], 80),
                new Symbol((char) 81, Patterns[81], 81),
                new Symbol((char) 82, Patterns[82], 82),
                new Symbol((char) 83, Patterns[83], 83),
                new Symbol((char) 84, Patterns[84], 84),
                new Symbol((char) 85, Patterns[85], 85),
                new Symbol((char) 86, Patterns[86], 86),
                new Symbol((char) 87, Patterns[87], 87),
                new Symbol((char) 88, Patterns[88], 88),
                new Symbol((char) 89, Patterns[89], 89),
                new Symbol((char) 90, Patterns[90], 90),
                new Symbol((char) 91, Patterns[91], 91),
                new Symbol((char) 92, Patterns[92], 92),
                new Symbol((char) 93, Patterns[93], 93),
                new Symbol((char) 94, Patterns[94], 94),
                new Symbol((char) 95, Patterns[95], 95),
                new Symbol((char) 96, Patterns[96], 96),
                new Symbol((char) 97, Patterns[97], 97),
                new Symbol((char) 98, Patterns[98], 98),
                new Symbol((char) 99, Patterns[99], 99),
                new Symbol(SpecialCodes.SwitchToCodeB, Patterns[100], 100),
                new Symbol(SpecialCodes.SwitchToCodeA, Patterns[101], 101),
                new Symbol(SpecialCodes.Func1, Patterns[102], 102),
                new Symbol(SpecialCodes.StartA, Patterns[103], 103),
                new Symbol(SpecialCodes.StartB, Patterns[104], 104),
                new Symbol(SpecialCodes.StartC, Patterns[105], 105),
                new Symbol(SpecialCodes.Stop, Patterns[106], 106)

                #endregion

            );
        }

        public ICodeSet CodeA { get; }
        public ICodeSet CodeB { get; }
        public ICodeSet CodeC { get; }

        public Symbol StopSymbol { get; }

        public IReadOnlyList<Symbol> StartSymbols { get; }

        public string GetPattern(byte value)
        {
            return Patterns[value];
        }

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
    }
}