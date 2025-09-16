using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        // dotnet run -- 2015 01
        // Kontrollera att användaren har angett både år och dag som argument
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: AdventOfCode <year> <day>");
            return;
        }

        // Hämta år och dag från argumenten
        var year = args[0];
        var day = args[1].PadLeft(2, '0'); // Lägg till nolla om dagen är ensiffrig

        // Bygg absolut sökväg till input-filen baserat på projektkatalogen
        var projectDir = AppContext.BaseDirectory;
        var inputPath = Path.Combine(projectDir, "..", "..", "..", "Inputs", year, $"Day{day}.txt");
        inputPath = Path.GetFullPath(inputPath);

        // Kontrollera att input-filen finns
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Bygg klassnamnet dynamiskt, t.ex. AdventOfCode.Years._2015.Day01
        var className = $"AdventOfCode.Years._{year}.Day{day}";
        var assembly = Assembly.GetExecutingAssembly(); // Hämta nuvarande assembly
        var type = assembly.GetType(className); // Hämta typen (klass) med rätt namn

        // Kontrollera att lösningsklassen finns
        if (type == null)
        {
            Console.WriteLine($"Solution class not found: {className}");
            return;
        }

        // Hämta metoden "Run" från klassen
        var runMethod = type.GetMethod("Run");
        var instance = Activator.CreateInstance(type); // Skapa en instans av klassen

        // Kör metoden "Run" och skicka in sökvägen till input-filen
        runMethod?.Invoke(instance, new object[] { inputPath });
    }
}