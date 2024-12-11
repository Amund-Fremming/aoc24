namespace puzzle10
{
    internal class Part1
    {
        public static void Solve()
        {
            var map = GetPuzzleInput();
            var startPositions = GetStartPointPositions(map);

            var sum = 0;
            foreach (var startPos in startPositions)
            {
                sum += TraverseRoute(map, [], startPos);
            }

            Console.WriteLine($"Sum : {sum}");
        }

        private static int[][] GetPuzzleInput()
        {
            var text = File.ReadAllLines("PuzzleInput.txt");
            int s = int.Parse(text.ElementAt(0).ToString());
            int[][] map = new int[text.Length][];
            var i = 0;
            foreach (var line in text)
            {
                map[i] = line.ToCharArray()
                    .Select(c => int.Parse(c.ToString()))
                    .ToArray();

                i++;
            }
            return map;
        }

        private static IList<(int, int)> GetStartPointPositions(int[][] map)
        {
            IList<(int, int)> startPositions = [];
            for (var i = 0; i < map.Length; i++)
            {
                for (var j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == 0)
                    {
                        startPositions.Add((i, j));
                    }
                }
            }
            return startPositions;
        }

        private static int TraverseRoute(int[][] map, HashSet<(int, int)> usedPositions, (int, int) startPos)
        {
            var (y, x) = startPos;
            if (map[y][x] == 9)
            {
                return 1;
            }

            IEnumerable<(int, int)> directions = [(1, 0), (-1, 0), (0, 1), (0, -1)];
            IList<(int, int)> nextSteps = [];
            for (int i = 0; i < 4; i++)
            {
                var (dy, dx) = directions.ElementAt(i);
                var (nextY, nextX) = (y + dy, x + dx);
                if (nextY >= map.Length || nextY < 0 || nextX >= map[y].Length || nextX < 0 || map[nextY][nextX] - 1 != map[y][x] || usedPositions.Contains((nextY, nextX)))
                {
                    continue;
                }
                nextSteps.Add((nextY, nextX));
            }

            if (nextSteps.Count == 0)
            {
                return 0;
            }

            usedPositions.Add(startPos);
            return nextSteps.Select(s => TraverseRoute(map, usedPositions, s)).Sum();
        }
    }
}