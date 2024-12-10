namespace puzzle7
{
    internal class Part1
    {
        public static void Solve()
        {
            List<int> list = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
            List<string> operators = [];
            for (int i = 0; i < list.Count - 1; i++)
            {
                operators.Add("+");
            }

            var toFlip = 0;
            var maxFlip = operators.Count;
            var flipUpwards = true;
            var combos = 0;
            for (int i = 0; i < Math.Pow(2, list.Count - 1); i++)
            {
                combos++;
                if (toFlip == -1)
                    Console.WriteLine("Combos " + combos);

                PrintTest(operators);
                Console.WriteLine();
                var (operatorsResult, nextFlipResult, maxFlipResult, flipUpwardsResult) = UpdateOperators(operators, toFlip, maxFlip, flipUpwards);
                operators = operatorsResult;
                toFlip = nextFlipResult;
                maxFlip = maxFlipResult;
                flipUpwards = flipUpwardsResult;
            }

            //var input = GetPuzzleInput();

            //long sum = 0;
            //foreach (var line in input)
            //{
            //    var validOperations = ValidSum(line);
            //    if (validOperations)
            //    {
            //        sum += line[0];
            //    }
            //}
            //Console.WriteLine($"Sum: {sum}");
        }

        public static void PrintTest(List<string> op)
        {
            foreach (var i in op)
                Console.Write(i + " ");
        }

        private static bool ValidSum(List<long> line)
        {
            List<string> operators = [];
            for (int i = 0; i < line.Count - 2; i++)
            {
                operators.Add("+");
            }

            int toFlip = 0;
            bool flipUpwards = true;
            int maxFlip = operators.Count;
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

                var (operatorsResult, nextFlipResult, maxFlipResult, flipUpwardsResult) = UpdateOperators(operators, toFlip, maxFlip, flipUpwards);
                operators = operatorsResult;
                toFlip = nextFlipResult;
                maxFlip = maxFlipResult;
                flipUpwards = flipUpwardsResult;
            }
            return false;
        }

        private static (List<string> operatorsResult, int nextFlipResult, int maxFlipResult, bool flipUpwardsResult) UpdateOperators(List<string> operators, int toFlip, int maxFlip, bool flipUpwards)
        {
            if (flipUpwards && maxFlip == 1 && toFlip == 1)
            {
                flipUpwards = false;
                maxFlip = -1;
                toFlip = operators.Count - 1;
            }

            if (flipUpwards)
            {
                if (maxFlip == toFlip)
                {
                    toFlip = 0;
                    maxFlip--;
                }

                if (toFlip != 0)
                {
                    operators[toFlip - 1] = FlipOperator(operators[toFlip - 1]);
                }

                operators[toFlip] = FlipOperator(operators[toFlip]);
                toFlip++;
            }

            if (!flipUpwards)
            {
                if (maxFlip == toFlip)
                {
                    toFlip = operators.Count - 1;
                    maxFlip++;
                }

                if (toFlip != operators.Count - 1)
                {
                    operators[toFlip + 1] = FlipOperator(operators[toFlip + 1]);
                }

                operators[toFlip] = FlipOperator(operators[toFlip]);
                toFlip--;
            }

            return (operators, toFlip, maxFlip, flipUpwards);
        }

        public static string FlipOperator(string op) => op.Equals("+") ? "*" : "+";

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