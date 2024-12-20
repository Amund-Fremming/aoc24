namespace puzzle19
{
    internal class Part1
    {
        public static void Solve()
        {
            var (patterns, designs) = GetPuzzleInput();

            int result = CountPossibleDesigns(patterns, designs);
            Console.WriteLine($"Sum : {result}");
        }

        private static int CountPossibleDesigns(List<string> patterns, List<string> designs)
        {
            int count = 0;
            foreach (var design in designs)
            {
                if (CanFormDesign(design, patterns))
                {
                    count++;
                }
            }
            return count;
        }

        private static bool CanFormDesign(string design, List<string> patterns)
        {
            int n = design.Length;
            bool[] canReachPos = new bool[n + 1];
            canReachPos[0] = true;

            for (int i = 1; i <= n; i++)
            {
                foreach (var pattern in patterns)
                {
                    var isSubstring = design.Substring(i - pattern.Length, pattern.Length) == pattern;
                    if (i >= pattern.Length && isSubstring)
                    {
                        canReachPos[i] = canReachPos[i] || canReachPos[i - pattern.Length];
                    }
                }
            }

            return canReachPos[n];
        }

        public static (List<string>, List<string>) GetPuzzleInput()
        {
            string patternsString = "";
            List<string> designs = [];
            string line;
            bool writeToDesigns = false;
            using var sr = new StreamReader("PuzzleInput.txt");
            while ((line = sr.ReadLine()!) != null)
            {
                if (line == "")
                {
                    writeToDesigns = true;
                    continue;
                }

                if (writeToDesigns)
                {
                    designs.Add(line);
                    continue;
                }

                patternsString += line;
            }

            List<string> patterns = patternsString.Split(',', StringSplitOptions.TrimEntries).ToList();

            return (patterns, designs);
        }
    }
}