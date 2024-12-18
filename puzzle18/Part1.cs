namespace puzzle18
{
    internal class Part1
    {
        public static void Solve()
        {
            var set = GetPuzzleInput(1024);
            // Print(map);

            var size = 71;
            int sum = BFS(set, 70, 70, size);
            Console.WriteLine($"Sum : {sum}");
        }

        private static void Print(char[][] map)
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    Console.Write(map[i][j]);
                }
                Console.WriteLine();
            }
        }

        private static HashSet<(int, int)> GetPuzzleInput(int bytes)
        {
            HashSet<(int, int)> set = [];
            string line;
            using var sr = new StreamReader("PuzzleInput.txt");
            var count = 0;
            while ((line = sr.ReadLine()!) != null && count < bytes)
            {
                var ints = line.Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToArray();

                set.Add((ints[0], ints[1]));
                count++;
            }

            return set;
        }

        private static int BFS(HashSet<(int, int)> set, int targetX, int targetY, int size)
        {
            int rows = size, cols = size;
            bool[][] visited = new bool[rows][];
            for (int i = 0; i < rows; i++)
            {
                visited[i] = new bool[cols];
            }

            Queue<(int x, int y, int distance)> queue = new();
            queue.Enqueue((0, 0, 0));
            visited[0][0] = true;
            (int, int)[] directions = [(0, 1), (0, -1), (1, 0), (-1, 0)];

            while (queue.Count > 0)
            {
                var (x, y, distance) = queue.Dequeue();

                if (x == targetX && y == targetY)
                {
                    return distance;
                }

                foreach (var dir in directions)
                {
                    int newX = x + dir.Item1;
                    int newY = y + dir.Item2;

                    if (newX >= 0 && newX < rows && newY >= 0 && newY < cols && !visited[newX][newY] && !set.Contains((newX, newY)))
                    {
                        queue.Enqueue((newX, newY, distance + 1));
                        visited[newX][newY] = true;
                    }
                }
            }

            return -1;
        }
    }
}