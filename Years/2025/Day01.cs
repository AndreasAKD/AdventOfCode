using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Years._2025
{
    public class Day01
    {

        public void Run(string inputPath)
        {
            var passwordInput = File.ReadAllLines(inputPath).ToList();

            int passwordOne = passwordPartOne(passwordInput);
            int passwordTwo = passwordPartTwo(passwordInput);

            Console.WriteLine($"Part One: Password Input: {passwordOne}");
            Console.WriteLine($"Part Two: Password Input: {passwordTwo}");
        }

        public int passwordPartOne(List<string> passwordInput)
        {
            int position = 50;
            int zeroCount = 0;

            foreach (var line in passwordInput)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                char direction = line[0];
                int distance = int.Parse(line.Substring(1));

                if (direction == 'L')
                {
                    position = (position - distance + 100) % 100;
                }
                else if (direction == 'R')
                {
                    position = (position + distance) % 100;
                }

                if (position == 0)
                {
                    zeroCount++;
                }
            }

            return zeroCount;
        }

        public int passwordPartTwo(List<string> passwordInput)
        {
            int position = 50;
            int zeroCount = 0;

            foreach (var line in passwordInput)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                char direction = line[0];
                int distance = int.Parse(line.Substring(1));

                int step = direction == 'L' ? -1 : 1;
                int absDistance = Math.Abs(distance);

                for (int i = 1; i <= absDistance; i++)
                {
                    position = (position + step + 100) % 100;
                    if (position == 0)
                        zeroCount++;
                }
            }

            return zeroCount;
        }
    }
}
