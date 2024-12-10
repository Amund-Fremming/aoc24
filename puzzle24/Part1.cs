namespace puzzle7
{
    internal class Part1
    {
        public static void Solve()
        {
            var input = GetPuzzleInput();
            foreach (var part in input)
            {
                foreach (var part2 in part)
                {
                    Console.Write(part2 + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Solved");
        }

        private static char[][] GetPuzzleInput()
        {
            // Ikke char array
            return File.ReadAllLines("PuzzleInput.txt")
                .Select(line => line.ToCharArray())
                .ToArray();
        }
    }
}