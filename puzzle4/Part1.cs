namespace puzzle4
{
    internal class Part1
    {
        public static void Solve()
        {
            List<List<char>> input = File.ReadAllLines("PuzzleInput.txt")
                .Select(line =>
                    line.ToCharArray()
                        .ToList())
                .ToList();

            var s = input[0];
            List<char> xmas = ['X', 'M', 'A', 'S'];
            var sum = 0;
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Count; j++)
                {
                    bool backwards = j - 3 >= 0;
                    bool upwards = i - 3 >= 0;
                    bool forwards = j + 3 < input[i].Count;
                    bool downwards = i + 3 < input.Count;

                    bool upLeft = backwards && upwards;
                    bool upRight = forwards && upwards;
                    bool downRight = forwards && downwards;
                    bool downLeft = backwards && downwards;

                    if (forwards && TraverseForwards(input, xmas, i, j))
                        sum++;

                    if (backwards && TraverseBackwards(input, xmas, i, j))
                        sum++;

                    if (upwards && TraverseUpwards(input, xmas, i, j))
                        sum++;

                    if (downwards && TraverseDownwards(input, xmas, i, j))
                        sum++;

                    if (upLeft && TraverseUpLeft(input, xmas, i, j))
                        sum++;

                    if (upRight && TraverseUpRight(input, xmas, i, j))
                        sum++;

                    if (downRight && TraverseDownRight(input, xmas, i, j))
                        sum++;

                    if (downLeft && TraverseDownLeft(input, xmas, i, j))
                        sum++;
                }
            }

            Console.WriteLine($"Sum : {sum}");
        }

        public static bool TraverseForwards(List<List<char>> input, List<char> xmas, int i, int j)
        {
            for (int x = 0; x < xmas.Count; x++)
            {
                if (input[i][j + x] != xmas[x])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool TraverseBackwards(List<List<char>> input, List<char> xmas, int i, int j)
        {
            for (int x = 0; x < xmas.Count; x++)
            {
                if (input[i][j - x] != xmas[x])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool TraverseUpwards(List<List<char>> input, List<char> xmas, int i, int j)
        {
            for (int x = 0; x < xmas.Count; x++)
            {
                if (input[i - x][j] != xmas[x])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool TraverseDownwards(List<List<char>> input, List<char> xmas, int i, int j)
        {
            for (int x = 0; x < xmas.Count; x++)
            {
                if (input[i + x][j] != xmas[x])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool TraverseUpLeft(List<List<char>> input, List<char> xmas, int i, int j)
        {
            for (int x = 0; x < xmas.Count; x++)
            {
                if (input[i - x][j - x] != xmas[x])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool TraverseUpRight(List<List<char>> input, List<char> xmas, int i, int j)
        {
            for (int x = 0; x < xmas.Count; x++)
            {
                if (input[i - x][j + x] != xmas[x])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool TraverseDownRight(List<List<char>> input, List<char> xmas, int i, int j)
        {
            for (int x = 0; x < xmas.Count; x++)
            {
                if (input[i + x][j + x] != xmas[x])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool TraverseDownLeft(List<List<char>> input, List<char> xmas, int i, int j)
        {
            for (int x = 0; x < xmas.Count; x++)
            {
                if (input[i + x][j - x] != xmas[x])
                {
                    return false;
                }
            }
            return true;
        }
    }
}