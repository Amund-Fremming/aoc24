using System.Text;

namespace puzzle15
{
    internal class Part1
    {
        public static void Solve()
        {
            var (map, moves) = GetPuzzleInput();
            var robotPosition = GetRobotPosition(map);
            for (int i = 0; i < moves.Length; i++)
            {
                var move = moves[i];
                var (y, x) = robotPosition;
                var (dy, dx) = GetVector(move);
                var next_y = y + dy;
                var next_x = x + dx;
                var nextPos = map[next_y][next_x];

                if (nextPos == '.')
                {
                    map[y][x] = '.';
                    map[next_y][next_x] = '@';
                    robotPosition = (next_y, next_x);
                }

                if (nextPos == 'O')
                {
                    var ver = next_y;
                    var hor = next_x;
                    while (map[ver][hor] != '.' && map[ver][hor] != '#')
                    {
                        ver = ver + dy;
                        hor = hor + dx;
                    }

                    if (map[ver][hor] == '.')
                    {
                        map[y][x] = '.';
                        map[next_y][next_x] = '@';
                        map[ver][hor] = 'O';
                        robotPosition = (next_y, next_x);
                    }
                }
            }

            Print(map);
            var boxCoordinates = GetBoxCoordinates(map);
            var sum = CalculateBoxesSum(boxCoordinates);
            Console.WriteLine($"Sum : {sum}");
        }

        public static void Print(char[][] map)
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

        public static int CalculateBoxesSum(List<(int, int)> boxCoordinates) => boxCoordinates.Aggregate(0, (total, crd) => total += crd.Item1 * 100 + crd.Item2);

        public static List<(int, int)> GetBoxCoordinates(char[][] map)
        {
            var coords = new List<(int, int)>();
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == 'O')
                    {
                        coords.Add((i, j));
                    }
                }
            }
            return coords;
        }

        public static (int, int) GetRobotPosition(char[][] map)
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == '@')
                    {
                        return (i, j);
                    }
                }
            }

            throw new ArgumentException("Robot does not exist!");
        }

        public static (int, int) GetVector(char move)
        {
            return move switch
            {
                '<' => (0, -1),
                '>' => (0, +1),
                '^' => (-1, 0),
                'v' => (+1, 0),
                _ => throw new ArgumentException("Invalid move!")
            };
        }

        public static (char[][], string) GetPuzzleInput()
        {
            List<char[]> map = [];
            string line;
            using var sr = new StreamReader("PuzzleInput.txt");
            var sb = new StringBuilder();
            var mapFinished = false;
            while ((line = sr.ReadLine()!) != null)
            {
                if (line == "")
                {
                    mapFinished = true;
                    continue;
                }
                if (!mapFinished)
                {
                    map.Add(line.ToCharArray());
                }
                if (mapFinished)
                {
                    sb.Append(line);
                }
            }
            return (map.ToArray(), sb.ToString());
        }
    }
}