namespace puzzle4
{
    internal class Part1
    {
        public static void Solve()
        {
            char[][] input = File.ReadAllLines("PuzzleInput.txt")
                .Select(line => line.ToCharArray())
                .ToArray();

            List<char> xmas = ['X', 'M', 'A', 'S'];
            var sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    bool backwards = j - 3 >= 0;
                    bool upwards = i - 3 >= 0;
                    bool forwards = j + 3 < input[i].Length;
                    bool downwards = i + 3 < input.Length;
                    bool upLeft = backwards && upwards;
                    bool upRight = forwards && upwards;
                    bool downRight = forwards && downwards;
                    bool downLeft = backwards && downwards;

                    if (forwards && Traverse(input, xmas, i, j, (0, 1))) sum++;
                    if (backwards && Traverse(input, xmas, i, j, (0, -1))) sum++;
                    if (upwards && Traverse(input, xmas, i, j, (-1, 0))) sum++;
                    if (downwards && Traverse(input, xmas, i, j, (1, 0))) sum++;
                    if (upLeft && Traverse(input, xmas, i, j, (-1, -1))) sum++;
                    if (upRight && Traverse(input, xmas, i, j, (-1, 1))) sum++;
                    if (downRight && Traverse(input, xmas, i, j, (1, 1))) sum++;
                    if (downLeft && Traverse(input, xmas, i, j, (1, -1))) sum++;
                }
            }

            Console.WriteLine($"Sum : {sum}");
        }

        public static bool Traverse(char[][] input, List<char> xmas, int i, int j, (int left, int right) operators)
        {
            for (int n = 0; n < xmas.Count; n++)
            {
                int GetVal(int val, int opr) => opr != 0
                    ? val + (opr * n)
                    : val;

                var x = GetVal(i, operators.left);
                var y = GetVal(j, operators.right);

                if (input[x][y] != xmas[n])
                    return false;
            }
            return true;
        }
    }
}