using System.Text.RegularExpressions;

namespace AdventOfCode.Years._2015
{
    public class Day13
    {
        public void Run(string inputPath)
        {
            var lines = File.ReadAllLines(inputPath);

            int totalChangeInHappines = TotalChangeInHappines(lines);
            int totalChangeInHappinesIncludingSelf = TotalChangeInHappinesIncludingSelf(lines);


            Console.WriteLine($"Part One: Total Change in Happiness: {totalChangeInHappines}");
            Console.WriteLine($"Part One: Total Change in Happiness with self: {totalChangeInHappinesIncludingSelf}");
        }

        public int TotalChangeInHappines(string[] lines)
        {
            var happiness = new Dictionary<(string, string), int>();
            var guests = new HashSet<string>();
            var regex = new Regex(@"^(\w+) would (gain|lose) (\d+) happiness units by sitting next to (\w+)\.");

            foreach (var line in lines)
            {
                var match = regex.Match(line);
                if (match.Success)
                {
                    string from = match.Groups[1].Value;
                    string to = match.Groups[4].Value;
                    int value = int.Parse(match.Groups[3].Value);
                    if (match.Groups[2].Value == "lose")
                        value = -value;

                    happiness[(from, to)] = value;
                    guests.Add(from);
                }
            }

            var guestList = guests.ToList();
            string fixedGuest = guestList[0];
            var others = guestList.Skip(1).ToList();

            int maxHappiness = int.MinValue;
            foreach (var arrangement in GetPermutations(others))
            {
                var seating = new List<string> { fixedGuest };
                seating.AddRange(arrangement);

                int total = 0;
                for (int i = 0; i < seating.Count; i++)
                {
                    string a = seating[i];
                    string b = seating[(i + 1) % seating.Count]; // circular

                    total += happiness.GetValueOrDefault((a, b), 0);
                    total += happiness.GetValueOrDefault((b, a), 0);
                }
                if (total > maxHappiness)
                    maxHappiness = total;
            }

            return maxHappiness;
        }

        public int TotalChangeInHappinesIncludingSelf(string[] lines)
        {
            var happiness = new Dictionary<(string, string), int>();
            var guests = new HashSet<string>();
            var regex = new Regex(@"^(\w+) would (gain|lose) (\d+) happiness units by sitting next to (\w+)\.");

            foreach (var line in lines)
            {
                var match = regex.Match(line);
                if (match.Success)
                {
                    string from = match.Groups[1].Value;
                    string to = match.Groups[4].Value;
                    int value = int.Parse(match.Groups[3].Value);
                    if (match.Groups[2].Value == "lose")
                        value = -value;

                    happiness[(from, to)] = value;
                    guests.Add(from);
                }
            }

            // Add yourself
            const string self = "You";
            foreach (var guest in guests)
            {
                happiness[(self, guest)] = 0;
                happiness[(guest, self)] = 0;
            }
            guests.Add(self);

            var guestList = guests.ToList();
            string fixedGuest = guestList[0];
            var others = guestList.Skip(1).ToList();

            int maxHappiness = int.MinValue;
            foreach (var arrangement in GetPermutations(others))
            {
                var seating = new List<string> { fixedGuest };
                seating.AddRange(arrangement);

                int total = 0;
                for (int i = 0; i < seating.Count; i++)
                {
                    string a = seating[i];
                    string b = seating[(i + 1) % seating.Count]; // circular

                    total += happiness.GetValueOrDefault((a, b), 0);
                    total += happiness.GetValueOrDefault((b, a), 0);
                }
                if (total > maxHappiness)
                    maxHappiness = total;
            }

            return maxHappiness;
        }


        private IEnumerable<List<T>> GetPermutations<T>(List<T> list)
        {
            if (list.Count == 1) yield return new List<T>(list);
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    var first = list[i];
                    var rest = list.Where((_, idx) => idx != i).ToList();
                    foreach (var perm in GetPermutations(rest))
                    {
                        perm.Insert(0, first);
                        yield return perm;
                    }
                }
            }
        }
    }
}