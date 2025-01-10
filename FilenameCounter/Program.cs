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

        var filenameWithoutExtension = Path.GetFileNameWithoutExtension(path);
        
        using var file = File.Open(path, FileMode.Open);
        using var streamReader = new StreamReader(file);
        var counter = 0;

        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();

            if (line != null && line.Contains(filenameWithoutExtension))
            {
                counter++;
            }
        }

        Console.WriteLine("found " + counter);

        return 0;
    }
}
