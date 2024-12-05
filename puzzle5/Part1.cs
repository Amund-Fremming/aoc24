using System.Data;

namespace puzzle5
{
    internal class Part1
    {
        public static void Solve()
        {
            var (rules, updates) = CreateRulesAndUpdates();
            // Print(rules, updates);

            var sum = 0;
            foreach (var update in updates)
            {
                if (ValidRules(rules, update))
                {
                    sum += ExtractMiddleValue(update);
                }
            }
            Console.WriteLine($"Sum: {sum}");
        }

        public static int ExtractMiddleValue(IList<int> update) => update[(update.Count - 1) / 2];

        public static bool ValidRules(Dictionary<int, HashSet<int>> rules, IList<int> update)
        {
            HashSet<int> occurances = [];
            foreach (var item in update)
            {
                if (rules.TryGetValue(item, out var ruleSet) && occurances.Overlaps(ruleSet))
                {
                    return false;
                }

                occurances.Add(item);
            }

            return true;
        }

        public static (Dictionary<int, HashSet<int>> rules, IList<IList<int>> updates) CreateRulesAndUpdates()
        {
            Dictionary<int, HashSet<int>> rules = [];
            IList<IList<int>> updates = [];
            using var reader = new StreamReader("PuzzleInput.txt");
            string line;
            var rulesReadingFinished = false;
            while ((line = reader.ReadLine()) != null)
            {
                if (line == string.Empty)
                {
                    rulesReadingFinished = true;
                    continue;
                }

                if (!rulesReadingFinished)
                {
                    var split = line.Split('|');
                    var left = int.Parse(split[0]);
                    var right = int.Parse(split[1]);

                    if (!rules.ContainsKey(left))
                    {
                        rules[left] = [];
                    }

                    rules[left].Add(right);
                }

                if (rulesReadingFinished)
                {
                    var updateList = line.Split(",")
                        .Select(int.Parse)
                        .ToList();

                    updates.Add(updateList);
                }
            }

            return (rules, updates);
        }

        public static void Print(Dictionary<int, HashSet<int>> rules, IList<IList<int>> updates)
        {
            foreach (var kvp in rules)
            {
                Console.Write(kvp.Key + "|");
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