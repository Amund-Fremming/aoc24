namespace puzzle2
{
    internal class Part1
    {
        public static void Solve()
        {
            // Burde være mulig å unngå O(n^2) her
            List<List<int>> puzzleInput = [];
            using (var sr = new StreamReader("PuzzleInput.txt"))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    var list = line.Split(' ')
                        .Select(int.Parse)
                        .ToList();

                    puzzleInput.Add(list);
                    line = sr.ReadLine();
                }
            }

            // Burde være mulig å få bedre enn O(n^2) her også
            int sum = 0;
            foreach (var list in puzzleInput)
            {
                if (IntervalIsSafe(list, 0, null))
                    sum++;
            }

            Console.WriteLine($"Sum : {sum}");
            Console.ReadKey();
        }

        public static bool IntervalIsSafe(List<int> list, int index, string status)
        {
            if (index == list.Count - 1)
                return true;

            var interval = list.ElementAt(index) - list.ElementAt(index + 1);
            var newStatus = interval switch
            {
                < 0 => "INC",
                > 0 => "DEC",
                _ => "NONE"
            };

            status ??= newStatus;
            var incDecSafe = status == newStatus;

            interval = Math.Abs(interval);
            var intervalSafe = interval <= 3;
            if (intervalSafe && incDecSafe)
                return IntervalIsSafe(list, index + 1, newStatus);

            return false;
        }
    }
}