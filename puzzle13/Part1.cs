using System.Text.RegularExpressions;

namespace puzzle13
{
    internal class Part1
    {
        public static void Solve()
        {
            ulong totalTokens = 0;
            var machines = GetPuzzleInput();
            foreach (var machine in machines)
            {
                totalTokens += GetShortestTokens(machine);
            }

            Console.WriteLine($"Sum: {totalTokens}");
        }

        public static ulong GetShortestTokens(Machine machine)
        {
            var cache = new Dictionary<(int, int), (ulong, ulong)>();
            var (a, b) = TraverseMachine(machine, (0, 0), 0, 0, cache);
            if (a == int.MaxValue && b == int.MaxValue)
                return 0;

            return (a * 3 + b * 1);
        }

        public static (ulong, ulong) TraverseMachine(Machine machine, (int, int) pos, int aPressed, int bPressed, Dictionary<(int, int), (ulong, ulong)> cache)
        {
            if (cache.ContainsKey(pos))
            {
                return cache[pos];
            }
            var (x, y) = pos;
            if (x > machine.Price.Item1 || machine.Price.Item2 < y)
            {
                return (int.MaxValue, int.MaxValue);
            }
            if (x == machine.Price.Item1 && machine.Price.Item2 == y)
            {
                return ((ulong)aPressed, (ulong)bPressed);
            }
            var aNewPos = (x + machine.ButtonA.Item1, y + machine.ButtonA.Item2);
            var bNewPos = (x + machine.ButtonB.Item1, y + machine.ButtonB.Item2);
            var aTraverse = TraverseMachine(machine, aNewPos, aPressed + 1, bPressed, cache);
            var bTraverse = TraverseMachine(machine, bNewPos, aPressed, bPressed + 1, cache);
            ulong aTokens = aTraverse.Item1 * 3 + aTraverse.Item2 * 1;
            ulong bTokens = bTraverse.Item1 * 3 + bTraverse.Item2 * 1;
            if (aTokens > bTokens)
            {
                cache[pos] = bTraverse;
                return bTraverse;
            }
            cache[pos] = aTraverse;
            return aTraverse;
        }

        public static void Print(IList<Machine> machines)
        {
            foreach (var machine in machines)
            {
                Console.WriteLine(machine.ToString());
            }
        }

        public static IList<Machine> GetPuzzleInput()
        {
            IList<Machine> machines = [];
            string line;
            using var sr = new StreamReader("PuzzleInput.txt");
            while ((line = sr.ReadLine()!) != null)
            {
                var machineInput = "";
                for (int i = 0; i < 3; i++)
                {
                    machineInput += line + "\n";
                    line = sr.ReadLine()!;
                }

                Regex buttonRegex = new(@"X\+(\d+),\s*Y\+(\d+)\n");
                var buttonMatches = buttonRegex.Matches(machineInput);
                var a = (int.Parse(buttonMatches[0].Groups[1].Value), int.Parse(buttonMatches[0].Groups[2].Value));
                var b = (int.Parse(buttonMatches[1].Groups[1].Value), int.Parse(buttonMatches[1].Groups[2].Value));

                Regex priceRegex = new(@"X=(\d+),\s*Y=(\d+)\n");
                var priceMatch = priceRegex.Matches(machineInput);
                var p = (int.Parse(priceMatch[0].Groups[1].Value), int.Parse(priceMatch[0].Groups[2].Value));

                var priceMatches = priceRegex.Matches(machineInput);

                machines.Add(new Machine(a, b, p));
            }
            return machines;
        }
    }

    public readonly struct Machine((int, int) ButtonA, (int, int) ButtonB, (int, int) Price
)
    {
        public (int, int) ButtonA { get; } = ButtonA;
        public (int, int) ButtonB { get; } = ButtonB;
        public (int, int) Price { get; } = Price;

        public override string ToString()
        {
            return $"Button A: X+{ButtonA.Item1}, Y+{ButtonA.Item2}\n" +
                $"Button B: X+{ButtonB.Item1}, Y+{ButtonB.Item2}\n" +
                $"Price: X={Price.Item1}, Y={Price.Item2}\n";
        }
    }
}