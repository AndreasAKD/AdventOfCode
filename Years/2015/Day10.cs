namespace AdventOfCode.Years._2015
{
    public class Day10
    {
        public void Run(string inputPath)
        {
            var line = File.ReadAllText(inputPath);

            int lengthOfTheResult40 = LengthOfTheResult40(line);
            int lengthOfTheResult50 = LengthOfTheResult50(line);
            PrintLongestRun(line);

            Console.WriteLine($"Part One: 40 length: {lengthOfTheResult40}");
            Console.WriteLine($"Part Two: 50 length {lengthOfTheResult40}");
        }

        private int LengthOfTheResult40(string line)
        {
            for (int i = 0; i < 40; i++)
            {
                var newLine = new System.Text.StringBuilder();
                var currentChar = line[0];
                var count = 1;
                for (int j = 1; j < line.Length; j++)
                {
                    if (line[j] == currentChar)
                    {
                        count++;
                    }
                    else
                    {
                        newLine.Append(count);
                        newLine.Append(currentChar);
                        currentChar = line[j];
                        count = 1;
                    }
                }
                newLine.Append(count);
                newLine.Append(currentChar);
                line = newLine.ToString();

                PrintLongestRun(line);

            }
            return line.Length;
        }

        private void PrintLongestRun(string line)
        {
            int maxRun = 0, currentRun = 1;
            char maxChar = line[0];
            for (int i = 1; i < line.Length; i++)
            {
                if (line[i] == line[i - 1])
                {
                    currentRun++;
                }
                else
                {
                    if (currentRun > maxRun)
                    {
                        maxRun = currentRun;
                        maxChar = line[i - 1];
                    }
                    currentRun = 1;
                }
            }
            // Check the last run
            if (currentRun > maxRun)
            {
                maxRun = currentRun;
                maxChar = line[line.Length - 1];
            }
            Console.WriteLine($"Longest run: {maxRun} consecutive '{maxChar}'s");
        }

        private int LengthOfTheResult50(string line)
        {
            for (int i = 0; i < 50; i++)
            {
                var newLine = new System.Text.StringBuilder();
                var currentChar = line[0];
                var count = 1;
                for (int j = 1; j < line.Length; j++)
                {
                    if (line[j] == currentChar)
                    {
                        count++;
                    }
                    else
                    {
                        newLine.Append(count);
                        newLine.Append(currentChar);
                        currentChar = line[j];
                        count = 1;
                    }
                }
                newLine.Append(count);
                newLine.Append(currentChar);
                line = newLine.ToString();
            }
            return line.Length;
        }

    }
}