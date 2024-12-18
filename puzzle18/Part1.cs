namespace puzzle18
{
    internal class Part1
    {
        public static void Solve()
        {
            var map = GetPuzzleInput(1024, 71);
            // Print(map);

            int sum = BFS(map, 0, 0, 70, 70);
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

        private static char[][] GetPuzzleInput(int bytes, int size)
        {
            var map = new char[size][];
            for (int i = 0; i < size; i++)
            {
                map[i] = Enumerable.Range(0, size)
                    .Select(e => '.')
                    .ToArray();
            }

            string line;
            using var sr = new StreamReader("PuzzleInput.txt");
            var count = 0;
            while ((line = sr.ReadLine()!) != null && count < bytes)
            {
                var ints = line.Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToArray();

                map[ints[1]][ints[0]] = '#';
                count++;
            }

            return map;
        }

        private static int BFS(char[][] map, int startX, int startY, int targetX, int targetY)
        {
            int rows = map.Length;
            int cols = map[0].Length;
            bool[][] visited = new bool[rows][];
            for (int i = 0; i < rows; i++)
            {
                visited[i] = new bool[cols];
            }

            Queue<(int x, int y, int distance)> queue = new();
            queue.Enqueue((startX, startY, 0));
            visited[startX][startY] = true;
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

                    if (newX >= 0 && newX < rows && newY >= 0 && newY < cols && !visited[newX][newY] && map[newX][newY] != '#')
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