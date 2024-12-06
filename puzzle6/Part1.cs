namespace puzzle6
{
    internal enum Direction
    {
        Up, Right, Down, Left
    }

    internal class Part1
    {
        public static void Solve()
        {
            var distinctPositions = new HashSet<(int, int)>();
            var directionMap = CreateDirections();
            var map = CreateMap();
            var guardPos = GetGuardCoord(map, out var direction);

            bool guardInSight = true;
            while (guardInSight)
            {
                var nextMove = directionMap[direction];
                distinctPositions.Add(guardPos);

                if (MovesOut(map, guardPos, nextMove))
                {
                    guardInSight = false;
                    continue;
                }

                if (NextIsObstruction(map, guardPos, nextMove))
                {
                    direction = Rotate(direction);
                    continue;
                }

                guardPos = Move(guardPos, nextMove);
            }

            Console.WriteLine($"Sum : {distinctPositions.Count}");
        }

        public static bool MovesOut(char[][] map, (int x, int y) pos, (int x, int y) move)
            => (pos.x + move.x > map.Length - 1) || (pos.x + move.x < 0) || (pos.y + move.y > map[0].Length - 1) || (pos.y + move.y < 0);

        public static Direction Rotate(Direction direction) => (Direction)((int)(direction + 1) % 4);

        private static (int, int) Move((int x, int y) pos, (int x, int y) move)
            => (pos.x + move.x, pos.y + move.y);

        private static bool NextIsObstruction(char[][] map, (int x, int y) pos, (int x, int y) move)
            => map[pos.x + move.x][pos.y + move.y] == '#';

        private static (int, int) GetGuardCoord(char[][] map, out Direction direction)
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if ("^>v<".Contains(map[i][j]))
                    {
                        direction = map[i][j] switch
                        {
                            '^' => Direction.Up,
                            '>' => Direction.Right,
                            'v' => Direction.Down,
                            '<' => Direction.Left,
                            _ => throw new NotImplementedException(),
                        };

                        return (i, j);
                    }
                }
            }

            throw new KeyNotFoundException("Guard does not exist in this map.");
        }

        private static char[][] CreateMap()
        {
            return File.ReadAllLines("PuzzleInput.txt")
                .Select(_ => _.ToCharArray())
                .ToArray();
        }

        private static Dictionary<Direction, (int, int)> CreateDirections()
        {
            return new Dictionary<Direction, (int, int)>(4)
            {
                { Direction.Up, (-1, 0) },
                { Direction.Right, (0, 1) },
                { Direction.Down, (1, 0) },
                { Direction.Left, (0, -1) },
            };
        }
    }
}