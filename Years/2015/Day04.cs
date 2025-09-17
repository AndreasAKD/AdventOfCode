using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Years._2015
{
    public class Day04
    {
        public void Run(string inputPath)
        {
            var secretKey = File.ReadAllText(inputPath);

            int whichNumberAdventCoins = MineAdventCoins(secretKey);
            int whichNumberAdventCoinsSixZeros = MineAdventCoinsWithSixZeros(secretKey);

            Console.WriteLine($"Part One: 5 Zero {whichNumberAdventCoins}");
            Console.WriteLine($"Part Two: 6 Zero: {whichNumberAdventCoinsSixZeros}");
        }

        private int MineAdventCoins(string secretKey)
        {
            var encoding = Encoding.ASCII;
            using var md5 = MD5.Create();

            for (int n = 1; ; n++)
            {
                var input = encoding.GetBytes($"{secretKey}{n}");
                var hash = md5.ComputeHash(input);

                if (hash[0] == 0 && hash[1] == 0 && (hash[2] >> 4) == 0)
                {
                    return n;
                }
            }
        }

        private int MineAdventCoinsWithSixZeros(string secretKey)
        {
            var encoding = Encoding.ASCII;
            using var md5 = MD5.Create();

            for (int n = 1; ; n++)
            {
                var input = encoding.GetBytes($"{secretKey}{n}");
                var hash = md5.ComputeHash(input);

                // Check for 6 leading hex zeroes (first 3 bytes == 0)
                if (hash[0] == 0 && hash[1] == 0 && hash[2] == 0)
                {
                    return n;
                }
            }
        }
    }
}