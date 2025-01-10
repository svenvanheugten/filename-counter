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
        var stringSearcherOverFileContents = new StringSearcher(File.ReadAllText(path));
        var count = stringSearcherOverFileContents.CountOccurrences(filenameWithoutExtension);

        Console.WriteLine("found " + count);

        return 0;
    }
}
