namespace AdventOfCode.Years._2015
{
    public class Day01
    {
        public void Run(string inputPath)
        {
            var instructions = File.ReadAllText(inputPath);

            int floor = PartOneFloors(instructions);
            int firstBasementPosition = PartTwoFirstBasementPosition(instructions);

            Console.WriteLine($"Part One: Santa ends up on floor: {floor}");
            Console.WriteLine($"Part Two: Position of first basement entry: {firstBasementPosition}");
        }

        private int PartOneFloors(string instructions)
        {
            int floor = 0;
            foreach (char c in instructions)
            {
                if (c == '(') floor++;
                else if (c == ')') floor--;
            }
            return floor;
        }

        private int PartTwoFirstBasementPosition(string instructions)
        {
            int floor = 0;
            for (int i = 0; i < instructions.Length; i++)
            {
                char c = instructions[i];
                if (c == '(') floor++;
                else if (c == ')') floor--;

                if (floor == -1)
                {
                    return i + 1; // 1-based index
                }
            }
            return -1; // If basement is never entered
        }
    }
}