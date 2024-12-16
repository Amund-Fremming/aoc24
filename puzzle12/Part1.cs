namespace puzzle12
{
    internal class Part1
    {
        /* Antagelser (Kan være feil)
         * - Planter kan oppstå over alt
         * - Er bare valid samling om den finnes med en eller fler naboer
         */

        public static void Solve()
        {
            var garden = GetPuzzleInput();
            Dictionary<char, (int, int)> map = [];

            (int, int)[] directions = [(-1, 0), (1, 0), (0, 1), (0, -1)];
            for (int i = 0; i < garden.Length; i++)
            {
                for (int j = 0; j < garden[i].Length; j++)
                {
                    var isRegion = false;
                    var perimeter = 0;
                    var typePlant = garden[i][j];
                    for (int k = 0; k < 4; k++)
                    {
                        var (dy, dx) = directions[k];
                        var isOutOfBounds = i + dy < 0 || i + dy >= garden.Length || j + dx < 0 || j + dx >= garden[i].Length;
                        var isNeighbour = isOutOfBounds ? false : garden[i + dy][j + dx] == typePlant;
                        if (isNeighbour && !isOutOfBounds)
                        {
                            isRegion = true;
                        }
                        if (isOutOfBounds || !isNeighbour)
                        {
                            perimeter++;
                        }
                    }
                    if (!isRegion)
                    {
                        continue;
                    }

                    var exists = map.TryGetValue(typePlant, out var vals);
                    if (exists)
                    {
                        var (prevArea, prevPerimeter) = vals;
                        var newArea = prevArea + 1;
                        map[typePlant] = (newArea, prevPerimeter + perimeter);
                        continue;
                    }
                    map.Add(typePlant, (1, perimeter));
                }
            }

            int sum = 0;
            foreach (var kvp in map)
            {
                var (area, perimeter) = kvp.Value;
                sum += area * perimeter;
            }

            Console.WriteLine($"Sum: {sum}");
        }

        public static char[][] GetPuzzleInput()
        {
            return File.ReadAllLines("PuzzleInput.txt")
                .Select(_ => _.ToCharArray())
                .Select(_ => _.ToArray())
                .ToArray();
        }
    }
}