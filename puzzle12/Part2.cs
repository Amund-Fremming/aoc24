namespace puzzle12
{
    internal class Part2
    {
        /* Antagelser (Kan være feil)
         * - Planter kan oppstå over alt
         * - Er bare valid samling om den finnes med en eller fler naboer
         */

        public static void Solve()
        {
            var garden = GetPuzzleInput();
            HashSet<(int, int)> usedPositions = [];
            List<int> regionCosts = [];
            for (int i = 0; i < garden.Length; i++)
            {
                for (int j = 0; j < garden[i].Length; j++)
                {
                    if (!usedPositions.Contains((i, j)))
                    {
                        var costs = TraverseRegion(garden, usedPositions);
                        regionCosts.Add(costs);
                    }
                }
            }
        }

        public static int TraverseRegion(char[][] garden, HashSet<(int, int)> usedPositions)
        {
        }

        public static char[][] GetPuzzleInput()
        {
            return File.ReadAllLines("PuzzleInput.txt")
                .Select(_ => _.ToCharArray())
                .ToArray();
        }
    }
}