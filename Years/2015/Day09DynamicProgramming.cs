namespace AdventOfCode.Years._2015
{
    public class Day09DynamicProgramming
    {
        public void Run(string inputPath)
        {
            var lines = File.ReadAllLines(inputPath);

            int shortest = DistanceOfShortestRouteDP(lines);
            int longest = DistanceOfLongestRouteDP(lines);

            Console.WriteLine($"Part One: Distance of shortest Route {shortest}");
            Console.WriteLine($"Part Two: Distance of longest route {longest}");
        }

        private int DistanceOfShortestRouteDP(string[] lines)
        {
            var locations = lines
                .SelectMany(line => line.Split(" = ")[0].Split(" to "))
                .Distinct()
                .ToArray();

            var index = locations
                .Select((loc, i) => new { loc, i })
                .ToDictionary(x => x.loc, x => x.i);

            int n = locations.Length;
            int[,] dist = new int[n, n];

            foreach (var line in lines)
            {
                var parts = line.Split(" = ");
                var locs = parts[0].Split(" to ");
                int a = index[locs[0]];
                int b = index[locs[1]];
                int d = int.Parse(parts[1]);
                dist[a, b] = d;
                dist[b, a] = d;
            }

            var memo = new Dictionary<(int, int), int>();

            int TSP(int mask, int pos)
            {
                if (mask == (1 << n) - 1)
                    return 0;

                var key = (mask, pos);
                if (memo.TryGetValue(key, out int cached))
                    return cached;

                int min = int.MaxValue;
                for (int next = 0; next < n; next++)
                {
                    if ((mask & (1 << next)) == 0)
                    {
                        int cost = dist[pos, next];
                        if (cost > 0)
                        {
                            int total = cost + TSP(mask | (1 << next), next);
                            if (total < min)
                                min = total;
                        }
                    }
                }
                memo[key] = min;
                return min;
            }

            int shortest = int.MaxValue;
            for (int start = 0; start < n; start++)
            {
                int result = TSP(1 << start, start);
                if (result < shortest)
                    shortest = result;
            }
            return shortest;
        }

        private int DistanceOfLongestRouteDP(string[] lines)
        {
            var locations = lines
                .SelectMany(line => line.Split(" = ")[0].Split(" to "))
                .Distinct()
                .ToArray();

            var index = locations
                .Select((loc, i) => new { loc, i })
                .ToDictionary(x => x.loc, x => x.i);

            int n = locations.Length;
            int[,] dist = new int[n, n];

            foreach (var line in lines)
            {
                var parts = line.Split(" = ");
                var locs = parts[0].Split(" to ");
                int a = index[locs[0]];
                int b = index[locs[1]];
                int d = int.Parse(parts[1]);
                dist[a, b] = d;
                dist[b, a] = d;
            }

            var memo = new Dictionary<(int, int), int>();

            int TSP(int mask, int pos)
            {
                if (mask == (1 << n) - 1)
                    return 0;

                var key = (mask, pos);
                if (memo.TryGetValue(key, out int cached))
                    return cached;

                int max = int.MinValue;
                for (int next = 0; next < n; next++)
                {
                    if ((mask & (1 << next)) == 0)
                    {
                        int cost = dist[pos, next];
                        if (cost > 0)
                        {
                            int total = cost + TSP(mask | (1 << next), next);
                            if (total > max)
                                max = total;
                        }
                    }
                }
                memo[key] = max;
                return max;
            }

            int longest = int.MinValue;
            for (int start = 0; start < n; start++)
            {
                int result = TSP(1 << start, start);
                if (result > longest)
                    longest = result;
            }
            return longest;
        }
    }
}