namespace AdventOfCode.Years._2015
{
    public class Day02
    {
        public void Run(string inputPath)
        {
            var lines = File.ReadAllLines(inputPath);
            int totalPaper = 0;
            int totalRibbon = 0;

            foreach (var line in lines)
            {
                var dims = line.Split('x').Select(int.Parse).ToArray();
                int l = dims[0], w = dims[1], h = dims[2];

                // Part One: Wrapping paper
                int side1 = l * w;
                int side2 = w * h;
                int side3 = h * l;
                int surfaceArea = 2 * side1 + 2 * side2 + 2 * side3;
                int slack = Math.Min(side1, Math.Min(side2, side3));
                totalPaper += surfaceArea + slack;

                // Part Two: Ribbon
                int[] sides = new[] { l, w, h };
                Array.Sort(sides);
                int perimeter = 2 * (sides[0] + sides[1]);
                int bow = l * w * h;
                totalRibbon += perimeter + bow;
            }

            Console.WriteLine($"Part One: Total square feet of wrapping paper needed: {totalPaper}");
            Console.WriteLine($"Part Two: Total feet of ribbon needed: {totalRibbon}");
        }
    }
}