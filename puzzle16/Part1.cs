namespace puzzle16
{
    public enum Direction
    {
        Up, Down, Left, Right,
    }

    internal class Part1
    {
        public static void Solve()
        {
            var map = GetPuzzleInput();
            var sum = GetShortestPathCost(map);
            Console.WriteLine($"Sum: {sum}");
        }

        public static long GetShortestPathCost(char[][] map)
        {
            var startPos = (Row: map.Length - 2, Col: 1);
            var endPos = FindEndPosition(map);

            var queue = new PriorityQueue<(int Row, int Col, Direction Dir, long Cost)>();
            var visited = new HashSet<(int Row, int Col, Direction Dir)>();

            queue.Enqueue((startPos.Row, startPos.Col, Direction.Right, 0), 0);

            var deltas = new Dictionary<Direction, (int RowDelta, int ColDelta)>
            {
                { Direction.Up, (-1, 0) },
                { Direction.Down, (1, 0) },
                { Direction.Left, (0, -1) },
                { Direction.Right, (0, 1) },
            };

            while (queue.Count > 0)
            {
                var (row, col, currentDir, cost) = queue.Dequeue();

                if (visited.Contains((row, col, currentDir))) continue;
                visited.Add((row, col, currentDir));

                if ((row, col) == endPos) return cost;

                var (dr, dc) = deltas[currentDir];
                var newRow = row + dr;
                var newCol = col + dc;
                if (map[newRow][newCol] != '#')
                {
                    queue.Enqueue((newRow, newCol, currentDir, cost + 1), cost + 1);
                }

                foreach (var rotation in GetRotations(currentDir))
                {
                    if (!visited.Contains((row, col, rotation)))
                    {
                        queue.Enqueue((row, col, rotation, cost + 1000), cost + 1000);
                    }
                }
            }

            throw new Exception("Path not found.");
        }

        public static (int Row, int Col) FindEndPosition(char[][] map)
        {
            for (int r = 0; r < map.Length; r++)
            {
                for (int c = 0; c < map[r].Length; c++)
                {
                    if (map[r][c] == 'E') return (r, c);
                }
            }

            throw new Exception("End position not found.");
        }

        public static IEnumerable<Direction> GetRotations(Direction current)
        {
            return new List<Direction>
            {
                current switch
                {
                    Direction.Up => Direction.Right,
                    Direction.Right => Direction.Down,
                    Direction.Down => Direction.Left,
                    Direction.Left => Direction.Up,
                    _ => throw new ArgumentOutOfRangeException()
                },
                current switch
                {
                    Direction.Up => Direction.Left,
                    Direction.Right => Direction.Up,
                    Direction.Down => Direction.Right,
                    Direction.Left => Direction.Down,
                    _ => throw new ArgumentOutOfRangeException()
                },
            };
        }

        public static char[][] GetPuzzleInput()
        {
            return File.ReadAllLines("PuzzleInput.txt")
                .Select(line => line.ToCharArray())
                .ToArray();
        }
    }

    public class PriorityQueue<T>
    {
        private List<(T Item, long Priority)> _elements = new List<(T, long)>();

        public int Count => _elements.Count;

        public void Enqueue(T item, long priority)
        {
            _elements.Add((item, priority));
            _elements.Sort((x, y) => x.Priority.CompareTo(y.Priority));
        }

        public T Dequeue()
        {
            var item = _elements[0];
            _elements.RemoveAt(0);
            return item.Item;
        }
    }
}