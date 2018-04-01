namespace LokerIT.Code128
{
    public static class SpecialCodes
    {
        public const ushort Func1 = (char) (0xff00 + 1);
        public const ushort Func2 = (char) (0xff00 + 2);
        public const ushort Func3 = (char) (0xff00 + 3);
        public const ushort Func4 = (char) (0xff00 + 4);
        public const ushort SwitchToCodeA = (char) (0xff00 + 10);
        public const ushort SwitchToCodeB = (char) (0xff00 + 11);
        public const ushort SwitchToCodeC = (char) (0xff00 + 12);
        public const ushort ShiftToA = (char) (0xff00 + 20);
        public const ushort ShiftToB = (char) (0xff00 + 21);
        public const ushort StartA = (char) (0xff00 + 30);
        public const ushort StartB = (char) (0xff00 + 31);
        public const ushort StartC = (char) (0xff00 + 32);
        public const ushort Stop = (char) (0xff00 + 40);
        public const ushort CheckSum = (char) 0xffff;

        public static bool IsControlCode(this ushort c)
        {
            switch (c)
            {
                case Func1:
                case Func2:
                case Func3:
                case Func4:
                case SwitchToCodeA:
                case SwitchToCodeB:
                case SwitchToCodeC:
                case ShiftToA:
                case ShiftToB:
                case StartA:
                case StartB:
                case StartC:
                case Stop:
                    return true;
                default:
                    return false;
            }
        }
    }
}