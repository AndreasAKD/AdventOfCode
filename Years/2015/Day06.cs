using System.Drawing;

namespace AdventOfCode.Years._2015
{
    public class Day06
    {
        public void Run(string inputPath)
        {
            var lines = File.ReadAllLines(inputPath);

            int howManyLights = HowManyLightsPartOneNoImage(lines);

            int howManyLightsPartOne;
            var grid = HowManyLightsPartOne(lines, out howManyLightsPartOne);
            var dir = @"C:\Users\AndreasDahlgren\source\repos\AdventOfCode\Puzzles\2015\";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            SaveGridAsBitmap(grid, Path.Combine(dir, "lights_part_one.png"));
            int totalBrightness = Totalbrightness(lines);

            Console.WriteLine($"Part One: Number of lights {HowManyLightsPartOneNoImage}");
            Console.WriteLine($"Part Two: totalBrightness {totalBrightness}");
        }



        private int HowManyLightsPartOneNoImage(string[] lines)
        {
            bool[,] grid = new bool[1000, 1000];

            foreach (var line in lines)
            {
                // Step 3: Parse the instruction
                string action;
                int x1, y1, x2, y2;

                // Example: "turn on 0,0 through 999,999"
                var parts = line.Split(' ');
                if (line.StartsWith("turn on"))
                {
                    action = "on";
                    var start = parts[2].Split(',');
                    var end = parts[4].Split(',');
                    x1 = int.Parse(start[0]);
                    y1 = int.Parse(start[1]);
                    x2 = int.Parse(end[0]);
                    y2 = int.Parse(end[1]);
                }
                else if (line.StartsWith("turn off"))
                {
                    action = "off";
                    var start = parts[2].Split(',');
                    var end = parts[4].Split(',');
                    x1 = int.Parse(start[0]);
                    y1 = int.Parse(start[1]);
                    x2 = int.Parse(end[0]);
                    y2 = int.Parse(end[1]);
                }
                else // toggle
                {
                    action = "toggle";
                    var start = parts[1].Split(',');
                    var end = parts[3].Split(',');
                    x1 = int.Parse(start[0]);
                    y1 = int.Parse(start[1]);
                    x2 = int.Parse(end[0]);
                    y2 = int.Parse(end[1]);
                }

                // Step 5: Apply the action to the grid
                for (int x = x1; x <= x2; x++)
                {
                    for (int y = y1; y <= y2; y++)
                    {
                        if (action == "on")
                            grid[x, y] = true;
                        else if (action == "off")
                            grid[x, y] = false;
                        else // toggle
                            grid[x, y] = !grid[x, y];
                    }
                }
            }

            // Step 6: Count the number of lights that are on
            int lightsLit = 0;
            for (int x = 0; x < 1000; x++)
            {
                for (int y = 0; y < 1000; y++)
                {
                    if (grid[x, y]) lightsLit++;
                }
            }

            return lightsLit;
        }

        private bool[,] HowManyLightsPartOne(string[] lines, out int lightsLit)
        {
            bool[,] grid = new bool[1000, 1000];

            foreach (var line in lines)
            {
                // Step 3: Parse the instruction
                string action;
                int x1, y1, x2, y2;

                // Example: "turn on 0,0 through 999,999"
                var parts = line.Split(' ');
                if (line.StartsWith("turn on"))
                {
                    action = "on";
                    var start = parts[2].Split(',');
                    var end = parts[4].Split(',');
                    x1 = int.Parse(start[0]);
                    y1 = int.Parse(start[1]);
                    x2 = int.Parse(end[0]);
                    y2 = int.Parse(end[1]);
                }
                else if (line.StartsWith("turn off"))
                {
                    action = "off";
                    var start = parts[2].Split(',');
                    var end = parts[4].Split(',');
                    x1 = int.Parse(start[0]);
                    y1 = int.Parse(start[1]);
                    x2 = int.Parse(end[0]);
                    y2 = int.Parse(end[1]);
                }
                else // toggle
                {
                    action = "toggle";
                    var start = parts[1].Split(',');
                    var end = parts[3].Split(',');
                    x1 = int.Parse(start[0]);
                    y1 = int.Parse(start[1]);
                    x2 = int.Parse(end[0]);
                    y2 = int.Parse(end[1]);
                }

                // Step 5: Apply the action to the grid
                for (int x = x1; x <= x2; x++)
                {
                    for (int y = y1; y <= y2; y++)
                    {
                        if (action == "on")
                            grid[x, y] = true;
                        else if (action == "off")
                            grid[x, y] = false;
                        else // toggle
                            grid[x, y] = !grid[x, y];
                    }
                }
            }

            lightsLit = 0;
            for (int x = 0; x < 1000; x++)
            {
                for (int y = 0; y < 1000; y++)
                {
                    if (grid[x, y]) lightsLit++;
                }
            }

            return grid;
        }

        private void SaveGridAsBitmap(bool[,] grid, string filePath)
        {
            int width = grid.GetLength(0);
            int height = grid.GetLength(1);
            using var bmp = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // Lit lights are yellow, off lights are black
                    bmp.SetPixel(x, y, grid[x, y] ? Color.Yellow : Color.Black);
                }
            }

            bmp.Save(filePath);
        }

        private int Totalbrightness(string[] lines)
        {
            int[,] grid = new int[1000, 1000]; // Each cell holds brightness

            foreach (var line in lines)
            {
                string action;
                int x1, y1, x2, y2;

                var parts = line.Split(' ');
                if (line.StartsWith("turn on"))
                {
                    action = "on";
                    var start = parts[2].Split(',');
                    var end = parts[4].Split(',');
                    x1 = int.Parse(start[0]);
                    y1 = int.Parse(start[1]);
                    x2 = int.Parse(end[0]);
                    y2 = int.Parse(end[1]);
                }
                else if (line.StartsWith("turn off"))
                {
                    action = "off";
                    var start = parts[2].Split(',');
                    var end = parts[4].Split(',');
                    x1 = int.Parse(start[0]);
                    y1 = int.Parse(start[1]);
                    x2 = int.Parse(end[0]);
                    y2 = int.Parse(end[1]);
                }
                else // toggle
                {
                    action = "toggle";
                    var start = parts[1].Split(',');
                    var end = parts[3].Split(',');
                    x1 = int.Parse(start[0]);
                    y1 = int.Parse(start[1]);
                    x2 = int.Parse(end[0]);
                    y2 = int.Parse(end[1]);
                }

                for (int x = x1; x <= x2; x++)
                {
                    for (int y = y1; y <= y2; y++)
                    {
                        if (action == "on")
                            grid[x, y] += 1;
                        else if (action == "off")
                            grid[x, y] = Math.Max(0, grid[x, y] - 1);
                        else // toggle
                            grid[x, y] += 2;
                    }
                }
            }

            int totalBrightness = 0;
            for (int x = 0; x < 1000; x++)
            {
                for (int y = 0; y < 1000; y++)
                {
                    totalBrightness += grid[x, y];
                }
            }

            return totalBrightness;
        }
    }
}