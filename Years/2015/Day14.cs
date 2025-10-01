using System.Text.RegularExpressions;

namespace AdventOfCode.Years._2015
{
    public class Day14
    {
        public void Run(string inputPath)
        {
            var lines = File.ReadAllLines(inputPath);

            int totalDistanceAfter2503Seconds = TotalDistanceAfter2503Seconds(lines);
            int newScoringSystemWinner = NewScoringSystemWinner(lines);


            Console.WriteLine($"Part One: Fastest Reindeer: {totalDistanceAfter2503Seconds}");
            Console.WriteLine($"Part One: Winner with new scoring system: {newScoringSystemWinner}");


        }
        public int TotalDistanceAfter2503Seconds(string[] lines)
        {

            int totalTime = 2503;

            var regex = new Regex(@"(\w+) can fly (\d+) km/s for (\d+) seconds, but then must rest for (\d+) seconds\.");
            var reindeers = lines
                .Select(line =>
                {
                    var match = regex.Match(line);
                    return new Reindeer
                    {
                        Name = match.Groups[1].Value,
                        Speed = int.Parse(match.Groups[2].Value),
                        FlyTime = int.Parse(match.Groups[3].Value),
                        RestTime = int.Parse(match.Groups[4].Value)
                    };
                })
                .ToList();


            foreach (var reindeer in reindeers)
            {
                int cycleTime = reindeer.FlyTime + reindeer.RestTime;
                int fullCycles = totalTime / cycleTime;
                int remainingTime = totalTime % cycleTime;

                int flyingTime = (fullCycles * reindeer.FlyTime) + Math.Min(remainingTime, reindeer.FlyTime);
                int totalDistance = flyingTime * reindeer.Speed;

                reindeer.TotalDistance = totalDistance;
            }

            return reindeers.Max(r => r.TotalDistance ?? 0);
        }

        public int NewScoringSystemWinner(string[] lines)
        {
            int totalTime = 2503;
            var regex = new Regex(@"(\w+) can fly (\d+) km/s for (\d+) seconds, but then must rest for (\d+) seconds\.");
            var reindeers = lines
                .Select(line =>
                {
                    var match = regex.Match(line);
                    return new Reindeer
                    {
                        Name = match.Groups[1].Value,
                        Speed = int.Parse(match.Groups[2].Value),
                        FlyTime = int.Parse(match.Groups[3].Value),
                        RestTime = int.Parse(match.Groups[4].Value),
                        Points = 0
                    };
                })
                .ToList();

            // State: (distance, timeInCycle, isFlying)
            var state = reindeers
                .Select(r => (distance: 0, timeInCycle: 0, isFlying: true))
                .ToArray();

            for (int second = 1; second <= totalTime; second++)
            {
                for (int i = 0; i < reindeers.Count; i++)
                {
                    state[i] = UpdateReindeerState(state[i], reindeers[i]);
                }

                int maxDistance = state.Max(s => s.distance);
                for (int i = 0; i < reindeers.Count; i++)
                {
                    if (state[i].distance == maxDistance)
                        reindeers[i].Points++;
                }
            }

            return reindeers.Max(r => r.Points ?? 0);
        }

        // Helper method for updating state
        private (int distance, int timeInCycle, bool isFlying) UpdateReindeerState(
            (int distance, int timeInCycle, bool isFlying) s, Reindeer r)
        {
            if (s.isFlying)
            {
                s.distance += r.Speed;
                s.timeInCycle++;
                if (s.timeInCycle == r.FlyTime)
                {
                    s.isFlying = false;
                    s.timeInCycle = 0;
                }
            }
            else
            {
                s.timeInCycle++;
                if (s.timeInCycle == r.RestTime)
                {
                    s.isFlying = true;
                    s.timeInCycle = 0;
                }
            }
            return s;
        }


        public class Reindeer
        {
            public string Name { get; set; }
            public int Speed { get; set; }
            public int FlyTime { get; set; }
            public int RestTime { get; set; }
            public int? TotalDistance { get; set; }

            public int? Points { get; set; }
        }
    }
}