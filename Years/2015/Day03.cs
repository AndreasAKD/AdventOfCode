namespace AdventOfCode.Years._2015
{
    public class Day03
    {
        public void Run(string inputPath)
        {
            var instructions = File.ReadAllText(inputPath);

            int howManyHouses = PartOneHowManyHousesGetsOnePresent(instructions);
            int howManyHousePartTwo = PartTwoHowManyHousesGetsOnePresent(instructions);

            Console.WriteLine($"Part One: This many houses get's presents: {howManyHouses}");
            Console.WriteLine($"Part Two: Position of first basement entry: {howManyHousePartTwo}");
        }

        private int PartOneHowManyHousesGetsOnePresent(string instructions)
        {
            int x = 0, y = 0;
            var visits = new HashSet<(int, int)>();
            visits.Add((x, y));

            foreach (char c in instructions)
            {
                switch (c)
                {
                    case '^': y++; break;
                    case 'v': y--; break;
                    case '>': x++; break;
                    case '<': x--; break;
                }
                visits.Add((x, y));
            }

            return visits.Count;
        }

        private int PartTwoHowManyHousesGetsOnePresent(string instructions)
        {
            // Santa's position
            int santaX = 0, santaY = 0;
            // Robo-Santa's position
            int roboX = 0, roboY = 0;

            // HashSet to track all unique visited houses
            var visited = new HashSet<(int, int)>();
            visited.Add((0, 0)); // Both start at the same house

            for (int i = 0; i < instructions.Length; i++)
            {
                // Alternate turns: even index = Santa, odd index = Robo-Santa
                if (i % 2 == 0)
                {
                    switch (instructions[i])
                    {
                        case '^': santaY++; break;
                        case 'v': santaY--; break;
                        case '>': santaX++; break;
                        case '<': santaX--; break;
                    }
                    visited.Add((santaX, santaY));
                }
                else
                {
                    switch (instructions[i])
                    {
                        case '^': roboY++; break;
                        case 'v': roboY--; break;
                        case '>': roboX++; break;
                        case '<': roboX--; break;
                    }
                    visited.Add((roboX, roboY));
                }
            }

            return visited.Count;
        }
    }
}