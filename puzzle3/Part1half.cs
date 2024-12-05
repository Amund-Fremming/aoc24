using System.Text.RegularExpressions;

namespace puzzle3
{
    internal class Part1half
    {
        public static void Solve()
        {
            string input = File.ReadAllText("PuzzleInput.txt");
            string pattern = @"mul\s*\(\s*(-?\d{1,3})\s*,\s*(-?\d{1,3})\s*\)";

            Regex regex = new Regex(pattern);

            int totalSum = 0;

            foreach (Match match in regex.Matches(input))
            {
                if (match.Success)
                {
                    int x = int.TryParse(match.Groups[1].Value, out int numX) ? numX : 0;
                    int y = int.TryParse(match.Groups[2].Value, out int numY) ? numY : 0;

                    totalSum += x * y;
                }
            }

            Console.WriteLine($"Total sum of valid multiplications: {totalSum}");
        }
    }
}