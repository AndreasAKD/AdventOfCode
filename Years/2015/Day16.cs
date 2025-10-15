namespace AdventOfCode.Years._2015
{
    public class Day16
    {
        public void Run(string inputPath)
        {
            var lines = File.ReadAllLines(inputPath);

            int whatIsTheNumberOfTheSueThatGaveGift = WhatIsTheNumberOfTheSueThatGaveGift(lines);
            int whatIsTheNumberOfTheSueThatGaveGiftPartTwo = WhatIsTheNumberOfTheSueThatGaveGiftPartTwo(lines);

            Console.WriteLine($"Part One: Number is: {whatIsTheNumberOfTheSueThatGaveGift}");
            Console.WriteLine($"Part Two: Number is: {whatIsTheNumberOfTheSueThatGaveGiftPartTwo}");

        }

        public int WhatIsTheNumberOfTheSueThatGaveGift(string[] lines)
        {
            int number = 0;

            var tickerTape = new Dictionary<string, int>
    {
        { "children", 3 },
        { "cats", 7 },
        { "samoyeds", 2 },
        { "pomeranians", 3 },
        { "akitas", 0 },
        { "vizslas", 0 },
        { "goldfish", 5 },
        { "trees", 3 },
        { "cars", 2 },
        { "perfumes", 1 }
    };

            foreach (var line in lines)
            {
                number++;
                var sueProperties = new Dictionary<string, int>();
                var parts = line.Substring(line.IndexOf(':') + 1).Split(',', StringSplitOptions.TrimEntries);

                foreach (var part in parts)
                {
                    var kv = part.Split(':', StringSplitOptions.TrimEntries);
                    if (kv.Length == 2)
                        sueProperties[kv[0]] = int.Parse(kv[1]);
                }

                bool isMatch = true;
                foreach (var property in sueProperties)
                {
                    if (tickerTape.ContainsKey(property.Key))
                    {
                        if (property.Value != tickerTape[property.Key])
                        {
                            isMatch = false;
                            break;
                        }
                    }
                }
                if (isMatch)
                {
                    return number;
                }
            }
            return -1;
        }

        public int WhatIsTheNumberOfTheSueThatGaveGiftPartTwo(string[] lines)
        {
            int number = 0;

            var tickerTape = new Dictionary<string, int>
    {
        { "children", 3 },
        { "cats", 7 },
        { "samoyeds", 2 },
        { "pomeranians", 3 },
        { "akitas", 0 },
        { "vizslas", 0 },
        { "goldfish", 5 },
        { "trees", 3 },
        { "cars", 2 },
        { "perfumes", 1 }
    };

            foreach (var line in lines)
            {
                number++;
                var sueProperties = new Dictionary<string, int>();
                var parts = line.Substring(line.IndexOf(':') + 1).Split(',', StringSplitOptions.TrimEntries);

                foreach (var part in parts)
                {
                    var kv = part.Split(':', StringSplitOptions.TrimEntries);
                    if (kv.Length == 2)
                        sueProperties[kv[0]] = int.Parse(kv[1]);
                }

                bool isMatch = true;
                foreach (var property in sueProperties)
                {
                    if (tickerTape.ContainsKey(property.Key))
                    {
                        if (property.Key == "cats" || property.Key == "trees")
                        {
                            if (property.Value <= tickerTape[property.Key])
                            {
                                isMatch = false;
                                break;
                            }
                        }
                        else if (property.Key == "pomeranians" || property.Key == "goldfish")
                        {
                            if (property.Value >= tickerTape[property.Key])
                            {
                                isMatch = false;
                                break;
                            }
                        }
                        else
                        {
                            if (property.Value != tickerTape[property.Key])
                            {
                                isMatch = false;
                                break;
                            }
                        }
                    }
                }
                if (isMatch)
                {
                    return number;
                }
            }
            return -1;
        }


    }
}