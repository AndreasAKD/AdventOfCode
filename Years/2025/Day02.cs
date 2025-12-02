using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Years._2025
{
    public class Day02
    {

        public void Run(string inputPath)
        {
            var input = File.ReadAllLines(inputPath).ToList();

            BigInteger invalidIDsPartOne = InvalidIDsPartOne(input);
            BigInteger invalidIDsPartTwo = InvalidIDsPartTwo(input);

            Console.WriteLine($"Part One: {invalidIDsPartOne}");
            Console.WriteLine($"Part Two: {invalidIDsPartTwo}");
        }

        public BigInteger InvalidIDsPartOne(List<string> input)
        {
            string[] ranges = input[0].Split(',');
            BigInteger bi = BigInteger.Zero;

            foreach (var range in ranges)
            {
                if (string.IsNullOrWhiteSpace(range))
                    continue;

                string[] values = range.Split('-'); 

                long first = long.Parse(values[0]);
                long second = long.Parse(values[1]);

                for (long i = first; i <= second; i++)
                {
                    if (invalidNumber(i))
                    {
                        bi += i; 
                    }
                }
            }

            return bi;
        }

        public BigInteger InvalidIDsPartTwo(List<string> input)
        {
            string[] ranges = input[0].Split(',');
            BigInteger bi = BigInteger.Zero;

            foreach (var range in ranges)
            {
                if (string.IsNullOrWhiteSpace(range))
                    continue;

                string[] values = range.Split('-');

                long first = long.Parse(values[0]);
                long second = long.Parse(values[1]);

                for (long i = first; i <= second; i++)
                {
                    if (invalidNumberTwo(i))
                    {
                        bi += i;
                    }
                }
            }

            return bi;
        }


        public bool invalidNumber(long candidateNumber)
        {
            string candidateNumberString = candidateNumber.ToString();
            int candidateNumberLength = candidateNumberString.Length;

            if (candidateNumberLength < 2 || candidateNumberLength % 2 != 0)
                return false;

            int halfLength = candidateNumberLength / 2;
            string firstHalf = candidateNumberString.Substring(0, halfLength);
            string secondHalf = candidateNumberString.Substring(halfLength, halfLength);

            return firstHalf == secondHalf;
        }

        public bool invalidNumberTwo(long candidateNumber)
        {
            string candidateNumberString = candidateNumber.ToString();
            int candidateNumberLength = candidateNumberString.Length;

            for (int substringLength = 1; substringLength <= candidateNumberLength / 2; substringLength++)
            {
                if (candidateNumberLength % substringLength != 0)
                    continue;

                string repeatedPattern = candidateNumberString.Substring(0, substringLength);
                int repeatCount = candidateNumberLength / substringLength;
                string constructedString = string.Concat(Enumerable.Repeat(repeatedPattern, repeatCount));

                if (constructedString == candidateNumberString)
                    return true;
            }
            return false;
        }
    }
}
