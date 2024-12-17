using System.Text;

namespace puzzle17
{
    internal class Part1
    {
        public static int _A = 0;
        public static int _B = 0;
        public static int _C = 0;
        public static int[] _program = [];

        public static int _pointer = 0;
        public static bool _pointerJumpOne = false;

        public static void RunProgram()
        {
            SetProgram();

            while (_pointer + 1 < _program.Length)
            {
                var opcode = _program[_pointer];
                OpcodeResolver(opcode);
                MovePointer();
            }
        }

        public static void SetProgram()
        {
            _A = 729;
            _B = 0;
            _C = 0;
            _program = [0, 1, 5, 4, 3, 0];
        }

        public static void MovePointer()
        {
            if (_pointerJumpOne)
            {
                _pointerJumpOne = false;
                _pointer++;
                return;
            }
            _pointer += 2;
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
            string registerBitString = Convert.ToString(_B, 2);

            var value = _program[_pointer + 1];
            string bitString = Convert.ToString(value, 2);

            int maxBits = Math.Max(bitString.Length, registerBitString.Length);
            bitString = bitString.PadLeft(maxBits, '0');
            registerBitString = registerBitString.PadLeft(maxBits, '0');

            StringBuilder sb = new();
            for (int i = 0; i < bitString.Length; i++)
            {
                var bit = bitString[i] == registerBitString[i] ? "0" : "1";
                sb.Append(bit);
            }
            int result = Convert.ToInt32(sb.ToString(), 2);
            _B = result;
        }

        private static void Opcode5()
        {
            var value = _program[_pointer + 1];
            var comboOperand = ComboOperandResolver(value);
            Console.Write((comboOperand % 8) + ", ");
        }

        private static void Opcode4()
        {
            // Possible bug here: Register B for now cannot be larger than a 3 bit number
            string bitString = Convert.ToString(_B, 2);

            string registerBitString = Convert.ToString(_C, 2);

            int maxBits = Math.Max(bitString.Length, registerBitString.Length);
            bitString = bitString.PadLeft(maxBits, '0');
            registerBitString = registerBitString.PadLeft(maxBits, '0');

            StringBuilder sb = new();
            for (int i = 0; i < bitString.Length; i++)
            {
                var bit = bitString[i] == registerBitString[i] ? "0" : "1";
                sb.Append(bit);
            }
            int result = Convert.ToInt32(sb.ToString(), 2);
            _B = result;
        }

        private static void Opcode3()
        {
            if (_A == 0)
                return;

            _pointer = _program[_pointer + 1];
            _pointerJumpOne = true;
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
    }
}