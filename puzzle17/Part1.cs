namespace puzzle17
{
    internal class Part1
    {
        public static int _A = 0;
        public static int _B = 0;
        public static int _C = 0;
        public static int[] _program = [];

        public static int _pointer = 0;

        public static void RunProgram()
        {
            SetProgram();

            while (_pointer + 1 < _program.Length)
            {
                var opcode = _program[_pointer];
                OpcodeResolver(opcode);
                if (opcode == 3 && _A != 0)
                {
                    continue;
                }
                _pointer += 2;
            }
        }

        public static void SetProgram()
        {
            _A = 27334280;
            _B = 0;
            _C = 0;
            _program = [2, 4, 1, 2, 7, 5, 0, 3, 1, 7, 4, 1, 5, 5, 3, 0];
        }

        public static int ComboOperandResolver(int value)
        {
            return value switch
            {
                0 or 1 or 2 or 3 => value,
                4 => _A,
                5 => _B,
                6 => _C,
                _ => throw new Exception("Value out bounds")
            };
        }

        public static void OpcodeResolver(int opcode)
        {
            if (opcode == 1) Opcode1();
            if (opcode == 2) Opcode2();
            if (opcode == 3) Opcode3();
            if (opcode == 4) Opcode4();
            if (opcode == 5) Opcode5();

            if (opcode == 0) _A = OpcodeADV();
            if (opcode == 6) _B = OpcodeADV();
            if (opcode == 7) _C = OpcodeADV();
        }

        private static void Opcode1()
        {
            var literalOpreand = _program[_pointer + 1];
            _B = BitwiseXOR(literalOpreand, _B);
        }

        private static void Opcode5()
        {
            var value = _program[_pointer + 1];
            var comboOperand = ComboOperandResolver(value);
            var result = comboOperand % 8;
            Console.Write(result + ",");
        }

        private static void Opcode4() => _B = BitwiseXOR(_B, _C);

        private static void Opcode3()
        {
            if (_A == 0)
                return;

            _pointer = _program[_pointer + 1];
        }

        private static void Opcode2()
        {
            var value = _program[_pointer + 1];
            var comboOperand = ComboOperandResolver(value);
            _B = comboOperand % 8;
        }

        private static int OpcodeADV()
        {
            var value = _program[_pointer + 1];
            var comboOperand = ComboOperandResolver(value);
            var result = (int)(_A / Math.Pow(2, comboOperand));
            return result;
        }

        private static int BitwiseXOR(int x1, int x2)
        {
            string bitString = Convert.ToString(x1, 2);
            string registerBitString = Convert.ToString(x2, 2);

            int maxBits = Math.Max(bitString.Length, registerBitString.Length);

            bitString = bitString.PadLeft(maxBits, '0');
            registerBitString = registerBitString.PadLeft(maxBits, '0');

            var str = bitString.Select((value, index) => registerBitString[index] == value ? "0" : "1")
                .Aggregate("", (sum, next) => sum += next);

            int result = Convert.ToInt32(str, 2);
            return result;
        }
    }
}