namespace puzzle7
{
    /// <summary>
    /// Denne bruker binærtelling for å teste ut alle mulige kombinasjoner som er mulige. + er 0, * er 1
    /// </summary>
    internal class Part1
    {
        public static void Solve()
        {
            var input = GetPuzzleInput();
            long sum = 0;
            foreach (var line in input)
            {
                var validOperations = ValidSum(line);
                if (validOperations)
                {
                    sum += line[0];
                }
            }
            Console.WriteLine($"Sum: {sum}");
        }

        public static void PrintTest(string[] op)
        {
            foreach (var i in op)
                Console.Write(i + " ");
        }

        private static bool ValidSum(List<long> line)
        {
            string[] operators = Enumerable.Range(0, line.Count - 2)
                 .Select(_ => "+")
                 .ToArray();

            for (int i = 0; i < Math.Pow(2, line.Count - 2); i++)
            {
                var iterationSum = line.ElementAt(1);
                var operatorIndex = 0;
                for (var j = 2; j < line.Count; j++)
                {
                    if (operators[operatorIndex] == "+")
                    {
                        iterationSum += line[j];
                    }
                    if (operators[operatorIndex] == "*")
                    {
                        iterationSum *= line[j];
                    }
                    operatorIndex++;
                }

                if (iterationSum == line.ElementAt(0))
                {
                    return true;
                }

                operators = InkrementOperators(operators);
            }
            return false;
        }

        /// <summary>
        /// Denne teller oppover som binærtelling, + er 0, * er 1
        /// </summary>
        /// <param name="operators"></param>
        /// <returns></returns>
        private static string[] InkrementOperators(string[] operators)
        {
            var n = operators.Length - 1;
            if (operators[n] == "+")
            {
                operators[n] = "*";
                return operators;
            }

            var j = n - 1;
            while (j >= 0)
            {
                if (operators[j] == "+")
                {
                    operators[j] = "*";
                    break;
                }

                j--;
            }

            for (int i = j + 1; i < operators.Length; i++)
            {
                operators[i] = "+";
            }

            return operators;
        }

        private static List<List<long>> GetPuzzleInput()
        {
            string line;
            List<List<long>> list = [];
            using var reader = new StreamReader("PuzzleInput.txt");
            while ((line = reader.ReadLine()!) != null)
            {
                var split = line.Split(":");
                var sum = long.Parse(split[0]);
                var nums = split[1].Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(long.Parse)
                    .ToList();

                nums.Insert(0, sum);
                list.Add(nums);
            }

            return list;
        }
    }
}