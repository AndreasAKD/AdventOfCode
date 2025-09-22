namespace AdventOfCode.Years._2015
{
    public class Day08
    {
        public void Run(string inputPath)
        {
            var lines = File.ReadAllLines(inputPath);


            int numberOfStringsLiteralsValuesOfStringSubtracted = NumberOfStringsLiteralsValuesOfStringSubtracted(lines);
            int totalNumber = NumberofCharactersMinusEachOriginalStringLiteral(lines);

            Console.WriteLine($"Part One: numberOfStringsLiteralsValuesOfStringSubtracted {numberOfStringsLiteralsValuesOfStringSubtracted}");
            Console.WriteLine($"Part Two: NumberofCharactersMinusEachOriginalStringLiteral {totalNumber}");
        }



        private int NumberOfStringsLiteralsValuesOfStringSubtracted(string[] lines)
        {
            int totalCharactersInStringLiteral = 0;
            int totalCharactersInMemory = 0;

            foreach (var line in lines)
            {
                int stringLiteralLength = line.Length;
                int memoryLength = 0;

                // Skip the surrounding quotes
                for (int i = 1; i < line.Length - 1; i++)
                {
                    if (line[i] == '\\')
                    {
                        if (i + 1 < line.Length)
                        {
                            if (line[i + 1] == '\\' || line[i + 1] == '\"')
                            {
                                memoryLength += 1;
                                i += 1; // Skip the next character as it's part of the escape sequence
                            }
                            else if (line[i + 1] == 'x' && i + 3 < line.Length &&
                                     IsHexDigit(line[i + 2]) && IsHexDigit(line[i + 3]))
                            {
                                memoryLength += 1;
                                i += 3; // Skip the next three characters as they're part of the hex escape sequence
                            }
                            else
                            {
                                memoryLength += 1;
                            }
                        }
                        else
                        {
                            memoryLength += 1;
                        }
                    }
                    else
                    {
                        memoryLength += 1;
                    }
                }

                totalCharactersInStringLiteral += stringLiteralLength;
                totalCharactersInMemory += memoryLength;
            }

            return totalCharactersInStringLiteral - totalCharactersInMemory;
        }

        // Helper method for hex digit check
        private static bool IsHexDigit(char c)
        {
            return (c >= '0' && c <= '9') ||
                   (c >= 'a' && c <= 'f') ||
                   (c >= 'A' && c <= 'F');
        }

        private int NumberofCharactersMinusEachOriginalStringLiteral(string[] lines)
        {
            int totalEncodedLength = 0;
            int totalOriginalLength = 0;

            foreach (var line in lines)
            {
                int encodedLength = 2; // Start with 2 for the new surrounding quotes

                foreach (char c in line)
                {
                    if (c == '\\' || c == '\"')
                    {
                        // Escaped backslash or quote becomes two characters
                        encodedLength += 2;
                    }
                    else
                    {
                        // Regular character
                        encodedLength += 1;
                    }
                }

                totalEncodedLength += encodedLength;
                totalOriginalLength += line.Length;
            }

            return totalEncodedLength - totalOriginalLength;
        }
    }
}