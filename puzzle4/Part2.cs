namespace puzzle4
{
    internal class Part2
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("PuzzleInput.txt")
                .Select(line => line.ToCharArray())
                .ToArray();

            var sum = 0;
            for (int row = 1; row < input.Length - 1; row++)
            {
                for (int col = 1; col < input[row].Length - 1; col++)
                {
                    if (input[row][col] == 'A')
                    {
                        sum += CountXMASInShape(input, (row, col));
                    }
                }
            }
            Console.WriteLine($"Total patterns : {sum}");
        }

        public static int CountXMASInShape(char[][] input, (int row, int col) coord)
        {
            var matches = 0;

            var row = coord.row;
            var col = coord.col;

            if (input[row - 1][col - 1] == 'M' && input[row + 1][col + 1] == 'S')
            {
                matches++;
            }

            if (input[row + 1][col - 1] == 'M' && input[row - 1][col + 1] == 'S')
            {
                matches++;
            }

            return matches;
        }
    }
}