namespace FilenameCounter;

public static class Program
{
    public static int Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.Error.WriteLine($"Usage: {AppDomain.CurrentDomain.FriendlyName} [path]");
            return 1;
        }

        var path = args[0];

        if (!File.Exists(path))
        {
            Console.Error.WriteLine($"Error: The file {path} does not exist.");
            return 1;
        }

        using var f = File.Open(path, FileMode.Open);
        var name = Path.GetFileNameWithoutExtension(path);
        using var file = new System.IO.StreamReader(f);
        string line;
        int counter = 0;
        while (true)
        {
            line = file.ReadLine();
            if (line == null) break;
            if (line.Contains(name))
                counter++;  
        }
        Console.WriteLine("found " + counter);
        return 0;
    }
}
