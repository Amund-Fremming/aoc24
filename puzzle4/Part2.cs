namespace puzzle4
{
    internal class Part2
    {
        public static void Solve()
        {
            char[][] input = File.ReadAllLines("PuzzleInput.txt")
                .Select(line => line.ToCharArray())
                .ToArray();

            var usedMap = new HashSet<(int, int)>();

            var sum = 0;
            for (int i = 1; i < input.Length - 1; i++)
            {
                for (int j = 1; j < input[i].Length - 1; j++)
                {
                    if (input[i][j] == 'A')
                    {
                        sum += Match(input, usedMap, (i, j));
                    }
                }
            }
            Console.WriteLine($"Sum : {sum}");
        }

        public static int Match(char[][] input, HashSet<(int x, int y)> usedMap, (int x, int y) coord)
        {
            var matches = 0;

            var x = coord.x;
            var y = coord.y;

            if (input[x - 1][y - 1] == 'M'
                && input[x + 1][y + 1] == 'S'
                && !usedMap.Contains((x - 1, y - 1))
                && !usedMap.Contains((x + 1, y + 1)))
            {
                matches++;
                usedMap.Add((x - 1, y - 1));
                usedMap.Add((x + 1, y + 1));
            }

            if (input[x + 1][y - 1] == 'M'
                && input[x - 1][y + 1] == 'S'
                && !usedMap.Contains((x + 1, y - 1))
                && !usedMap.Contains((x - 1, y + 1)))
            {
                matches++;
                usedMap.Add((x + 1, y - 1));
                usedMap.Add((x - 1, y + 1));
            }

            return matches;
        }
    }
}