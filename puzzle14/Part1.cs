using System.Text.RegularExpressions;

namespace puzzle14
{
    internal class Part1
    {
        public static void Solve()
        {
            var tall = 103;
            var wide = 101;
            var robots = GetPuzzleInput();
            var seconds = 100;
            for (int i = 0; i < seconds; i++)
            {
                foreach (var robot in robots)
                {
                    robot.Position = CalculateNextPosition(robot, tall, wide);
                }
            }
            var sum = CalculateQuadrantSum(robots, wide, tall);
            Console.WriteLine($"Sum : {sum}");
        }

        public static int CalculateQuadrantSum(IList<Robot> robots, int wide, int tall)
        {
            var xNotQuad = (wide - 1) / 2;
            var yNotQuad = (tall - 1) / 2;

            var q1 = 0;
            var q2 = 0;
            var q3 = 0;
            var q4 = 0;

            foreach (var robot in robots)
            {
                var (p_x, p_y) = robot.Position;
                if (p_x == xNotQuad || p_y == yNotQuad)
                {
                    continue;
                }

                if (p_x < xNotQuad && p_y < yNotQuad)
                {
                    q1++;
                }
                if (p_x > xNotQuad && p_y < yNotQuad)
                {
                    q2++;
                }
                if (p_x < xNotQuad && p_y > yNotQuad)
                {
                    q3++;
                }
                if (p_x > xNotQuad && p_y > yNotQuad)
                {
                    q4++;
                }
            }
            var sum = q1 * q2 * q3 * q4;
            return sum;
        }

        public static (int, int) CalculateNextPosition(Robot robot, int tall, int wide)
        {
            var (x, y) = robot.Position;
            var (dx, dy) = robot.Velocity;
            static int Wrap(int value, int max) => (value + max) % max;

            var next_x = Wrap(x + dx, wide);
            var next_y = Wrap(y + dy, tall);

            return (next_x, next_y);
        }

        public static IList<Robot> GetPuzzleInput()
        {
            static (int, int) ExtractVal(MatchCollection matches, int i, int j) => (int.Parse(matches[0].Groups[i].Value), int.Parse(matches[0].Groups[j].Value));

            List<Robot> robots = [];
            using var sr = new StreamReader("PuzzleInput.txt");
            string line;
            while ((line = sr.ReadLine()!) != null)
            {
                var matches = new Regex(@"p\=(-?\d+),(-?\d+)\sv\=(-?\d+),(-?\d+)").Matches(line);
                var position = ExtractVal(matches, 1, 2);
                var velocity = ExtractVal(matches, 3, 4);

                robots.Add(new Robot(position, velocity));
            }
            return robots;
        }

        public record Robot((int, int) Position, (int, int) Velocity)
        {
            public (int, int) Position { get; set; } = Position;
        }
    }
}