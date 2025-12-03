using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Years._2025
{
    public class Day03
    {

        public void Run(string inputPath)
        {
            var banks = File.ReadAllLines(inputPath).ToList();

            int totalOutputJoltagePartOne = TotalOutputJoltagePartOne(banks);
            long totalOutputJoltagePartTwo = TotalOutputJoltagePartTwo(banks);

            Console.WriteLine($"Part One: {totalOutputJoltagePartOne}");
            Console.WriteLine($"Part Two: {totalOutputJoltagePartTwo}");
        }
        public int TotalOutputJoltagePartOne(List<string> batteryBanks)
        {
            int totalOutputJoltage = 0;

            foreach (var batteryBank in batteryBanks)
            {
                if (string.IsNullOrWhiteSpace(batteryBank))
                    continue;

                int largestTwoDigitJoltage = 0;

                for (int firstIndex = 0; firstIndex < batteryBank.Length; firstIndex++)
                {
                    int firstDigit = batteryBank[firstIndex] - '0';

                    for (int secondIndex = firstIndex + 1; secondIndex < batteryBank.Length; secondIndex++)
                    {
                        int secondDigit = batteryBank[secondIndex] - '0';
                        int twoDigitJoltage = firstDigit * 10 + secondDigit;

                        if (twoDigitJoltage > largestTwoDigitJoltage)
                            largestTwoDigitJoltage = twoDigitJoltage;
                    }
                }

                totalOutputJoltage += largestTwoDigitJoltage;
            }

            return totalOutputJoltage;
        }

        public long TotalOutputJoltagePartTwo(List<string> batteryBanks)
        {
            long totalOutputJoltage = 0;

            foreach (var batteryBank in batteryBanks)
            {
                if (string.IsNullOrWhiteSpace(batteryBank))
                    continue;

                int digitsToPick = 12;
                int digitsToRemove = batteryBank.Length - digitsToPick;
                var stack = new List<char>();

                foreach (char digit in batteryBank)
                {
                    while (stack.Count > 0 &&
                           digitsToRemove > 0 &&
                           stack[stack.Count - 1] < digit)
                    {
                        stack.RemoveAt(stack.Count - 1);
                        digitsToRemove--;
                    }
                    stack.Add(digit);
                }

                string largestJoltageString = new string(stack.Take(digitsToPick).ToArray());
                long largestJoltage = long.Parse(largestJoltageString);
                totalOutputJoltage += largestJoltage;
            }

            return (long)totalOutputJoltage;
        }

    }
}
