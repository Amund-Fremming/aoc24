namespace puzzle6
{
    internal class P2
    {
        private static void Main()
        {
            string[] grid = {
            "....#.....",
            ".........#",
            "..........",
            "..#.......",
            ".......#..",
            "..........",
            ".#..^.....",
            "........#.",
            "#.........",
            "......#..."
        };

            Console.WriteLine($"Distinct positions visited: {PredictGuardPath(grid)}");
        }

        private static int PredictGuardPath(string[] grid)
        {
            var directions = new (int dx, int dy)[]
            {
            (0, -1),  // Up
            (1, 0),   // Right
            (0, 1),   // Down
            (-1, 0)   // Left
            };

            Dictionary<char, int> turnRight = new()
        {
            { '^', 1 },  // Up -> Right
            { '>', 2 },  // Right -> Down
            { 'v', 3 },  // Down -> Left
            { '<', 0 }   // Left -> Up
        };

            int rows = grid.Length;
            int cols = grid[0].Length;

            int guardX = 0, guardY = 0, dirIndex = 0;
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    char cell = grid[y][x];
                    if ("^>v<".Contains(cell))
                    {
                        guardX = x;
                        guardY = y;
                        dirIndex = Array.IndexOf(new char[] { '^', '>', 'v', '<' }, cell);
                        break;
                    }
                }
            }

            HashSet<(int, int)> visited = new() { (guardX, guardY) };

            while (true)
            {
                int nextX = guardX + directions[dirIndex].dx;
                int nextY = guardY + directions[dirIndex].dy;

                if (nextX < 0 || nextX >= cols || nextY < 0 || nextY >= rows)
                {
                    break;
                }

                if (grid[nextY][nextX] == '#')
                {
                    dirIndex = turnRight[new char[] { '^', '>', 'v', '<' }[dirIndex]];
                }
                else
                {
                    guardX = nextX;
                    guardY = nextY;
                    visited.Add((guardX, guardY));
                }
            }

            return visited.Count;
        }
    }
}