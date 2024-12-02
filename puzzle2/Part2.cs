namespace puzzle2
{
    internal class Part2
    {
        public static void Solve()
        {
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

            int sum = 0;
            foreach (var list in puzzleInput)
            {
                // Er hele listen rett
                var isAsc = list[0] < list[1];
                if (IsValidAscending(list) || IsValidDescending(list))
                {
                    sum++;
                    continue;
                }

                // Listen var ikke rett, kan en fjernes?
                // Brute force pga. mange edge caser
                bool foundValid = false;
                for (int i = 0; i < list.Count; i++)
                {
                    var tempList = new List<int>(list);
                    tempList.RemoveAt(i);

                    if (IsValidAscending(tempList) || IsValidDescending(tempList))
                    {
                        sum++;
                        foundValid = true;
                        break;
                    }
                }

                if (foundValid) continue;
            }

            Console.WriteLine($"Sum : {sum}");
            Console.ReadKey();
        }

        public static bool IsValidAscending(List<int> list)
        {
            for (int i = 1; i < list.Count; i++)
            {
                var diff = list[i - 1] - list[i];
                var isValid = diff < 0 && Math.Abs(diff) <= 3;
                if (!isValid)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsValidDescending(List<int> list)
        {
            for (int i = 1; i < list.Count; i++)
            {
                var diff = list[i - 1] - list[i];
                var isValid = diff > 0 && Math.Abs(diff) <= 3;
                if (!isValid)
                {
                    return false;
                }
            }

            return true;
        }
    }
}