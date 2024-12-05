using System.Data;

namespace puzzle5
{
    /* SOL 1
     * - Samle alle regler i Dictinary
     * - Iterer over hver oppdatering, hvis vi møter på en regel
     * => for hver regel, se til at tallet etter er etter i oppdateringen, og at den ikke er før
     * - Hvis good legg til midterste tallet i sum
     */

    /* SOL 2
     * - Samle alle regler i Dictinary
     * - Iterer over hver oppdatering, legg til dette tallet i hashset,
     * - hvis vi møter på en regel
     * => for hver regel, hvis tall nr 2, finnes i hashset feil, iterer forover og at den ikke er før
     * - Hvis good inkrementer midten
     */

    internal class Part1
    {
        public static void Solve()
        {
            var (rules, updates) = CreateRulesAndUpdates();
            Print(rules, updates);

            // sjekk om Extract får ut verdiene fra midten av updates

            // bygg valid rules

            // iterer over
        }

        public static int ExtractMiddleValue(IList<int> update) => update[(update.Count + 1) / 2 + 1];

        public static bool ValidRules(Dictionary<int, IList<int>> rules, IList<int> update)
        {
            throw new NotImplementedException();
        }

        public static (Dictionary<int, IList<int>> rules, IList<IList<int>> updates) CreateRulesAndUpdates()
        {
            Dictionary<int, IList<int>> rules = [];
            IList<IList<int>> updates = [];
            using (var reader = new StreamReader("PuzzleInput.txt"))
            {
                var line = reader.ReadLine();
                var rulesReadingFinished = false;
                while (line != null)
                {
                    if (line == string.Empty)
                    {
                        rulesReadingFinished = true;
                        line = reader.ReadLine();
                        continue;
                    }

                    if (!rulesReadingFinished)
                    {
                        var left = int.Parse(string.Concat(line[0], line[1]));
                        var right = int.Parse(string.Concat(line[3], line[4]));

                        if (rules.TryGetValue(left, out var ruleList))
                        {
                            ruleList.Add(right);
                            rules[left] = ruleList;
                        }
                        else
                        {
                            rules.Add(left, [right]);
                        }
                    }

                    if (rulesReadingFinished)
                    {
                        var updateList = line.Split(",")
                            .Select(int.Parse)
                            .ToList();

                        updates.Add(updateList);
                    }

                    line = reader.ReadLine();
                }

                return (rules, updates);
            }
        }

        public static void Print(Dictionary<int, IList<int>> rules, IList<IList<int>> updates)
        {
            foreach (var kvp in rules)
            {
                Console.Write(kvp.Key + "| ");
                foreach (var item in kvp.Value)
                {
                    Console.Write(item + " ");
                }

                Console.WriteLine();
            }

            Console.WriteLine("------------------");

            foreach (var list in updates)
            {
                Console.Write("Update| ");
                foreach (var item in list)
                {
                    Console.Write(item + " ");
                }

                Console.WriteLine();
            }
        }
    }
}