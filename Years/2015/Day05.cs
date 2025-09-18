namespace AdventOfCode.Years._2015
{
    public class Day05
    {
        public void Run(string inputPath)
        {
            var lines = File.ReadAllLines(inputPath);

            int howManyStringsAreNice = HowManyStringsAreNice(lines);
            int nowManyStringsAreNiceNewRules = HowManyStringsAreNiceNewRules(lines);

            Console.WriteLine($"Part One: Number of nie Strings {howManyStringsAreNice}");
            Console.WriteLine($"Part Two: 6 Zero: {nowManyStringsAreNiceNewRules}");
        }



        private int HowManyStringsAreNice(string[] lines)
        {

            int niceCount = 0;
            foreach (var line in lines)
            {
                if (line.Length > 0)
                {
                    char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };
                    int vowelCount = line.Count(c => vowels.Contains(c));

                    bool hasDoubleLetter = false;
                    for (int i = 1; i < line.Length; i++)
                    {
                        if (line[i] == line[i - 1])
                        {
                            hasDoubleLetter = true;
                            break;
                        }
                    }
                    string[] forbiddenSubstrings = new string[] { "ab", "cd", "pq", "xy" };
                    bool hasForbiddenSubstring = forbiddenSubstrings.Any(sub => line.Contains(sub));
                    if (vowelCount >= 3 && hasDoubleLetter && !hasForbiddenSubstring)
                    {
                        niceCount++;
                    }
                }
            }
            return niceCount;
        }


        //        --- Part Two ---
        //Realizing the error of his ways, Santa has switched to a better model of determining whether a string is naughty or nice.None of the old rules apply, as they are all clearly ridiculous.

        //Now, a nice string is one with all of the following properties:

        //It contains a pair of any two letters that appears at least twice in the string without overlapping, like xyxy(xy) or aabcdefgaa(aa), but not like aaa(aa, but it overlaps).
        //It contains at least one letter which repeats with exactly one letter between them, like xyx, abcdefeghi(efe), or even aaa.
        //For example:

        //qjhvhtzxzqqjkmpb is nice because is has a pair that appears twice(qj) and a letter that repeats with exactly one letter between them(zxz).
        //xxyxx is nice because it has a pair that appears twice and a letter that repeats with one between, even though the letters used by each rule overlap.
        //uurcxstgmygtbstg is naughty because it has a pair(tg) but no repeat with a single letter between them.
        //ieodomkazucvgmuy is naughty because it has a repeating letter with one between(odo), but no pair that appears twice.
        //How many strings are nice under these new rules?

        private int HowManyStringsAreNiceNewRules(string[] lines)
        {
            int niceCount = 0;
            foreach (var line in lines)
            {

                bool hasPair = false;
                for (int i = 0; i < line.Length - 1; i++)
                {
                    string pair = line.Substring(i, 2);
                    if (line.IndexOf(pair, i + 2) != -1)
                    {
                        hasPair = true;
                        break;
                    }
                }

                bool hasRepeatWithOneBetween = false;
                for (int i = 0; i < line.Length - 2; i++)
                {
                    if (line[i] == line[i + 2])
                    {
                        hasRepeatWithOneBetween = true;
                        break;
                    }
                }

                if (hasPair && hasRepeatWithOneBetween)
                {
                    niceCount++;
                }
            }
            return niceCount;
        }
    }
}