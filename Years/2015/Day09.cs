namespace AdventOfCode.Years._2015
{
    public class Day09
    {
        public void Run(string inputPath)
        {
            var lines = File.ReadAllLines(inputPath);

            int distanceOfShortestRoute = DistanceOfShortestRoute(lines);
            int distanceOfLongestRoute = DistanceOfLongestRoute(lines);

            Console.WriteLine($"Part One: Distance of shortest Route {distanceOfShortestRoute}");
            Console.WriteLine($"Part Two: Distance of longest route {distanceOfLongestRoute}");
        }

        private int DistanceOfShortestRoute(string[] lines)
        {
            string[] allLocations = lines.ToArray()
                .SelectMany(line => line.Split(" = ")[0].Split(" to "))
                .Distinct()
                .ToArray();

            Dictionary<(string from, string to), int> distances = lines
                .Select(line => line.Split(" = "))
                .ToDictionary(
                    parts =>
                    {
                        var locations = parts[0].Split(" to ");
                        return (from: locations[0], to: locations[1]);
                    },
                    parts => int.Parse(parts[1])
                );

            int shortestDistance = int.MaxValue;

            foreach (var route in GetPermutations(allLocations, allLocations.Length))
            {
                int total = 0;
                for (int i = 0; i < route.Length - 1; i++)
                {
                    var from = route[i];
                    var to = route[i + 1];
                    if (distances.TryGetValue((from, to), out int dist))
                        total += dist;
                    else if (distances.TryGetValue((to, from), out dist))
                        total += dist;
                    else
                        total = int.MaxValue; // Invalid route
                }
                if (total < shortestDistance)
                    shortestDistance = total;
            }

            return shortestDistance;
        }

        private int DistanceOfLongestRoute(string[] lines)

        {
            string[] allLocations = lines.ToArray()
                .SelectMany(line => line.Split(" = ")[0].Split(" to "))
                .Distinct()
                .ToArray();
            Dictionary<(string from, string to), int> distances = lines
                .Select(line => line.Split(" = "))
                .ToDictionary(
                    parts =>
                    {
                        var locations = parts[0].Split(" to ");
                        return (from: locations[0], to: locations[1]);
                    },
                    parts => int.Parse(parts[1])
                );
            int longestDistance = int.MinValue;
            foreach (var route in GetPermutations(allLocations, allLocations.Length))
            {
                int total = 0;
                for (int i = 0; i < route.Length - 1; i++)
                {
                    var from = route[i];
                    var to = route[i + 1];
                    if (distances.TryGetValue((from, to), out int dist))
                        total += dist;
                    else if (distances.TryGetValue((to, from), out dist))
                        total += dist;
                    else
                        total = int.MinValue; // Invalid route
                }
                if (total > longestDistance)
                    longestDistance = total;
            }
            return longestDistance;
        }

        // Helper method to generate permutations
        private static IEnumerable<string[]> GetPermutations(string[] items, int count)
        {
            if (count == 1)
                return items.Select(t => new[] { t });

            return items.SelectMany((item, i) =>
                GetPermutations(items.Where((_, idx) => idx != i).ToArray(), count - 1)
                    .Select(result => (new[] { item }).Concat(result).ToArray()));
        }
    }
}