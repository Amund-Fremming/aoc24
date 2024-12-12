namespace puzzle11
{
    internal class Part1
    {
        public static void Solve()
        {
            int blinks = 25;
            var stones = GetPuzzleInput();
            for (var i = 0; i < blinks; i++)
            {
                stones = Blink(stones);
            }

            Console.WriteLine($"Sum : {stones.Count}");
        }

        private static List<string> Blink(List<string> stones)
        {
            List<string> newStones = [];
            foreach (var stone in stones)
            {
                if (stone.Length % 2 == 0)
                {
                    int mid = stone.Length / 2;
                    var left = stone[0..mid];
                    var right = stone[mid..stone.Length];
                    newStones.Add(left);
                    newStones.Add(int.Parse(right).ToString());
                    continue;
                }

                var stoneToAdd = stone == "0" ? "1" : (long.Parse(stone) * 2024).ToString();
                newStones.Add(stoneToAdd);
            }
            return newStones;
        }

        private static List<string> GetPuzzleInput()
        {
            return File.ReadAllText("PuzzleInput.txt")
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .ToList();
        }
    }
}