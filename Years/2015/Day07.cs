namespace AdventOfCode.Years._2015
{
    public class Day07
    {
        public void Run(string inputPath)
        {
            var lines = File.ReadAllLines(inputPath);


            int whichNumberWireA = WhichNumberWireA(lines);
            int partTwo = PartTwo(lines);

            Console.WriteLine($"Part One: Number for Wire A {whichNumberWireA}");
            Console.WriteLine($"Part Two: ModifiedLines {partTwo}");
        }



        private int WhichNumberWireA(string[] lines)
        {
            // Step 1: Parse all instructions and map each wire to its instruction
            var instructions = new Dictionary<string, string>();
            foreach (string line in lines)
            {
                var parts = line.Split(" -> ");
                var outputWire = parts[1].Trim();
                var instruction = parts[0].Trim();
                instructions[outputWire] = instruction;
            }

            // Step 3: Create a cache to store computed wire values
            var cache = new Dictionary<string, int>();

            // Step 4: Define a recursive function to evaluate a wire's value
            int Evaluate(string wire)
            {
                // If wire is a number, return its value
                if (ushort.TryParse(wire, out var value))
                    return value;

                // If wire is in cache, return cached value
                if (cache.ContainsKey(wire))
                    return cache[wire];

                var instr = instructions[wire];
                int result;

                var tokens = instr.Split(' ');
                if (tokens.Length == 1)
                {
                    // Direct assignment
                    result = Evaluate(tokens[0]);
                }
                else if (tokens.Length == 2 && tokens[0] == "NOT")
                {
                    // NOT operation
                    result = ~Evaluate(tokens[1]) & 0xFFFF;
                }
                else if (tokens.Length == 3)
                {
                    var left = tokens[0];
                    var op = tokens[1];
                    var right = tokens[2];

                    switch (op)
                    {
                        case "AND":
                            result = (Evaluate(left) & Evaluate(right)) & 0xFFFF;
                            break;
                        case "OR":
                            result = (Evaluate(left) | Evaluate(right)) & 0xFFFF;
                            break;
                        case "LSHIFT":
                            result = (Evaluate(left) << int.Parse(right)) & 0xFFFF;
                            break;
                        case "RSHIFT":
                            result = (Evaluate(left) >> int.Parse(right)) & 0xFFFF;
                            break;
                        default:
                            throw new InvalidOperationException($"Unknown operation: {op}");
                    }
                }
                else
                {
                    throw new InvalidOperationException($"Unknown instruction format: {instr}");
                }

                cache[wire] = result;
                return result;
            }

            // Step 5: Evaluate the value for wire "a" using the recursive function
            return Evaluate("a");
        }

        private int PartTwo(string[] lines)
        {
            int valueForWireA = WhichNumberWireA(lines);
            // Modify the instructions to set wire "b" to the value of wire "a"
            var modifiedLines = lines.Select(line =>
            {
                if (line.EndsWith(" -> b"))
                {
                    return $"{valueForWireA} -> b";
                }
                return line;
            }).ToArray();
            // Re-evaluate with the modified instructions
            return WhichNumberWireA(modifiedLines);
        }
    }
}