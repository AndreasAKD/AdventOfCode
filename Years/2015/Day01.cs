namespace AdventOfCode.Years._2015
{
    public class Day01
    {
        public void Run(string inputPath)
        {
            var instructions = File.ReadAllText(inputPath);

            int floor = 0;
            int firstBasementPosition = -1;

            for (int i = 0; i < instructions.Length; i++)
            {
                char c = instructions[i];
                if (c == '(') floor++;
                else if (c == ')') floor--;

                if (floor == -1 && firstBasementPosition == -1)
                {
                    firstBasementPosition = i + 1; // 1-based index
                }
            }

            Console.WriteLine($"Part One: Santa ends up on floor: {floor}");
            Console.WriteLine($"Part Two: Position of first basement entry: {firstBasementPosition}");
        }
    }
}