using System.Text.RegularExpressions;

namespace puzzle3
{
    internal class Part1half
    {
        public static void Solve()
        {
            // Replace this with your actual input
            string input = File.ReadAllText("PuzzleInput.txt");

            // Regular expression to match valid mul(X,Y) instructions
            // This allows optional spaces and handles possible malformed values
            string pattern = @"mul\s*\(\s*(-?\d{1,3})\s*,\s*(-?\d{1,3})\s*\)";

            Regex regex = new Regex(pattern);

            int totalSum = 0;

            // Find all matches
            foreach (Match match in regex.Matches(input))
            {
                if (match.Success)
                {
                    // Extract the two numbers, handling malformed input
                    int x = int.TryParse(match.Groups[1].Value, out int numX) ? numX : 0;
                    int y = int.TryParse(match.Groups[2].Value, out int numY) ? numY : 0;

                    // Multiply and add to the total sum
                    totalSum += x * y;
                }
            }

            // Output the result
            Console.WriteLine($"Total sum of valid multiplications: {totalSum}");
        }
    }
}