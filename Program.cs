using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: AdventOfCode <year> <day>");
            return;
        }

        var year = args[0];
        var day = args[1].PadLeft(2, '0');
        var inputPath = Path.Combine("Inputs", year, $"Day{day}.txt");

        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        var className = $"AdventOfCode.Years._{year}.Day{day}";
        var assembly = Assembly.GetExecutingAssembly();
        var type = assembly.GetType(className);

        if (type == null)
        {
            Console.WriteLine($"Solution class not found: {className}");
            return;
        }

        var runMethod = type.GetMethod("Run");
        var instance = Activator.CreateInstance(type);

        runMethod?.Invoke(instance, new object[] { inputPath });
    }
}