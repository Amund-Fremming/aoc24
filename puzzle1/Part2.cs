namespace puzzle1
{
    internal class Part2
    {
        public static void Solve()
        {
            List<int> left = [];
            Dictionary<int, int> rightCountTable = [];

            using (StreamReader sr = new("PuzzleInput.txt"))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    var arr = line.Split(" ");
                    left.Add(int.Parse(arr[0]));

                    var rightValue = int.Parse(arr[^1]);
                    var numberExists = rightCountTable.TryGetValue(rightValue, out var count);
                    if (!numberExists)
                    {
                        rightCountTable[rightValue] = 1;
                        line = sr.ReadLine();
                        continue;
                    }

                    count++;
                    rightCountTable[rightValue] = count;
                    line = sr.ReadLine();
                }

                sr.Close();
            }

            var sum = 0;
            foreach (var num in left)
            {
                var numberExist = rightCountTable.TryGetValue(num, out var count);
                if (numberExist)
                    sum += num * count;
            }

            Console.WriteLine($"Sum: {sum}");
            Console.ReadKey();
        }
    }
}