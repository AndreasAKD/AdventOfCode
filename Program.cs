using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        // dotnet run -- 2025 01/2025 1 or just debugging with args in properties/launchSettings.json
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: AdventOfCode <year> <day>");
            return;
        }

        var year = args[0];
        var day = args[1].PadLeft(2, '0'); 

        var projectDir = AppContext.BaseDirectory;
        var inputPath = Path.Combine(projectDir, "..", "..", "..", "Inputs", year, $"Day{day}.txt");
        inputPath = Path.GetFullPath(inputPath);

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