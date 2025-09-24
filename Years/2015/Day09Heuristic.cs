namespace AdventOfCode.Years._2015
{
    public class Day09Heuristic
    {
        public void Run(string inputPath)
        {
            var lines = File.ReadAllLines(inputPath);

            int shortest = DistanceOfShortestRouteNearestNeighbor(lines);
            int longest = DistanceOfLongestRouteNearestNeighbor(lines);

            Console.WriteLine($"Part One (Heuristic): Distance of shortest Route {shortest}");
            Console.WriteLine($"Part Two (Heuristic): Distance of longest route {longest}");
        }

        private int DistanceOfShortestRouteNearestNeighbor(string[] lines)
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

            int shortest = int.MaxValue;
            for (int start = 0; start < n; start++)
            {
                var visited = new bool[n];
                int total = 0;
                int current = start;
                visited[current] = true;

                for (int step = 1; step < n; step++)
                {
                    int next = -1;
                    int minDist = int.MaxValue;
                    for (int i = 0; i < n; i++)
                    {
                        if (!visited[i] && dist[current, i] > 0 && dist[current, i] < minDist)
                        {
                            minDist = dist[current, i];
                            next = i;
                        }
                    }
                    if (next == -1) break; // No valid next step
                    total += minDist;
                    visited[next] = true;
                    current = next;
                }
                if (visited.All(v => v))
                    shortest = Math.Min(shortest, total);
            }
            return shortest;
        }

        private int DistanceOfLongestRouteNearestNeighbor(string[] lines)
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

            int longest = int.MinValue;
            for (int start = 0; start < n; start++)
            {
                var visited = new bool[n];
                int total = 0;
                int current = start;
                visited[current] = true;

                for (int step = 1; step < n; step++)
                {
                    int next = -1;
                    int maxDist = int.MinValue;
                    for (int i = 0; i < n; i++)
                    {
                        if (!visited[i] && dist[current, i] > 0 && dist[current, i] > maxDist)
                        {
                            maxDist = dist[current, i];
                            next = i;
                        }
                    }
                    if (next == -1) break; // No valid next step
                    total += maxDist;
                    visited[next] = true;
                    current = next;
                }
                if (visited.All(v => v))
                    longest = Math.Max(longest, total);
            }
            return longest;
        }
    }
}