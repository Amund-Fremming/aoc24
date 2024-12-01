namespace puzzle1
{
    internal class Part1
    {
        public static void Solve()
        {
            List<int> left = [];
            List<int> right = [];

            using (StreamReader sr = new("PuzzleInput.txt"))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    var arr = line.Split(" ");
                    left.Add(int.Parse(arr[0]));
                    right.Add(int.Parse(arr[^1]));
                    line = sr.ReadLine();
                }

                sr.Close();
            }

            left.Sort();
            right.Sort();

            var sum = 0;
            for (int i = 0; i < left.Count; i++)
            {
                sum += Math.Abs(left[i] - right[i]);
            }

            Console.WriteLine($"Sum: {sum}");
            Console.ReadLine();
        }
    }
}
