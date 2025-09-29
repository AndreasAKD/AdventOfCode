using System.Text.Json;

namespace AdventOfCode.Years._2015
{
    public class Day12
    {
        public void Run(string inputPath)
        {
            var lines = File.ReadAllText(inputPath);

            int sumOfAllNumbersInDocument = SumOfAllNumbersInDocument(lines);
            int sumOfAllNumbersInDocumentPartTwo = SumOfAllNumbersInDocumentPartTwo(lines);


            Console.WriteLine($"Part One: Sum of all numbers in document: {sumOfAllNumbersInDocument}");
            Console.WriteLine($"Part Two: Sum of all numbers in document: {sumOfAllNumbersInDocumentPartTwo}");


        }

        public int SumOfAllNumbersInDocument(string document)
        {
            var matches = System.Text.RegularExpressions.Regex.Matches(document, @"-?\d+");
            int sum = 0;
            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                sum += int.Parse(match.Value);
            }
            return sum;
        }

        public int SumOfAllNumbersInDocumentPartTwo(string document)
        {
            using var doc = JsonDocument.Parse(document);
            return SumElement(doc.RootElement);
        }

        private int SumElement(JsonElement element)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.Object:
                    // If any property value is "red", ignore this object
                    foreach (var prop in element.EnumerateObject())
                    {
                        if (prop.Value.ValueKind == JsonValueKind.String && prop.Value.GetString() == "red")
                            return 0;
                    }
                    int objSum = 0;
                    foreach (var prop in element.EnumerateObject())
                    {
                        objSum += SumElement(prop.Value);
                    }
                    return objSum;

                case JsonValueKind.Array:
                    int arrSum = 0;
                    foreach (var item in element.EnumerateArray())
                    {
                        arrSum += SumElement(item);
                    }
                    return arrSum;

                case JsonValueKind.Number:
                    return element.GetInt32();

                default:
                    return 0;
            }
        }


    }
}