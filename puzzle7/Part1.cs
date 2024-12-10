namespace puzzle7
{
    internal class Part1
    {
        public static void Solve()
        {
            List<int> list = [1, 2, 3, 4];
            List<string> operators = [];
            for (int i = 0; i < list.Count - 1; i++)
            {
                operators.Add("+");
            }

            var toFlip = 0;
            var maxFlip = operators.Count - 1;
            var flipUpwards = true;
            for (int i = 0; i < Math.Pow(2, list.Count - 1); i++)
            {
                PrintTest(operators);
                Console.WriteLine();
                var (operatorsResult, nextFlipResult, maxFlipResult, flipUpwardsResult) = UpdateOperators(operators, toFlip, maxFlip, flipUpwards);
                operators = operatorsResult;
                toFlip = nextFlipResult;
                maxFlip = maxFlipResult;
                flipUpwards = flipUpwardsResult;
            }

            //var input = GetPuzzleInput();

            //var sum = 0;
            //foreach (var line in input)
            //{
            //    var validOperations = ValidSum(line);
            //    if (validOperations)
            //    {
            //        sum += line[0];
            //    }
            //}
        }

        public static void PrintTest(List<string> op)
        {
            foreach (var i in op)
                Console.Write(i + " ");
        }

        private static bool ValidSum(List<int> line)
        {
            List<string> operators = [];
            for (int i = 0; i < line.Count - 2; i++)
            {
                operators.Add("+");
            }

            bool flipUpwards = true;
            int toFlip = 0;
            int maxFlip = operators.Count - 1;
            for (int i = 0; i < Math.Pow(2, line.Count - 2); i++)
            {
                var iterationSum = 0;
                var operatorIndex = 0;
                for (var j = 2; j < line.Count; j++)
                {
                    if (operators[operatorIndex] == "+")
                    {
                        iterationSum += line[j - 1] + line[j];
                    }
                    if (operators[operatorIndex] == "*")
                    {
                        iterationSum += line[j - 1] * line[j];
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
            if (!flipUpwards && maxFlip > toFlip)
            {
                return (operators, toFlip, maxFlip, flipUpwards);
            }

            if (flipUpwards && maxFlip < toFlip)
            {
                toFlip = 0;
                maxFlip--;
            }

            if (flipUpwards)
            {
                if (toFlip != 0)
                {
                    operators[toFlip - 1] = FlipOperator(operators[toFlip - 1]);
                }

                operators[toFlip] = FlipOperator(operators[toFlip]);
                toFlip++;
                return (operators, toFlip, maxFlip, flipUpwards);
            }

            if (toFlip != operators.Count - 1)
            {
                operators[toFlip + 1] = FlipOperator(operators[toFlip + 1]);
            }

            operators[toFlip] = FlipOperator(operators[toFlip]);
            toFlip--;

            if (flipUpwards && toFlip == 0 && maxFlip == 0)
            {
                flipUpwards = false;
                toFlip = operators.Count - 1;
            }

            return (operators, toFlip, maxFlip, flipUpwards);
        }

        public static string FlipOperator(string op) => op.Equals("+") ? "*" : "+";

        private static List<List<int>> GetPuzzleInput()
        {
            string line;
            List<List<int>> list = [];
            using var reader = new StreamReader("PuzzleInput.txt");
            while ((line = reader.ReadLine()!) != null)
            {
                var split = line.Split(":");
                var sum = int.Parse(split[0]);
                var nums = split[1].Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();

                nums.Insert(0, sum);
                list.Add(nums);
            }

            return list;
        }
    }
}