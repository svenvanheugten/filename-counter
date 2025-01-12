namespace FilenameCounter;

public static class Program
{
    public static int Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.Error.WriteLine(
                $"Usage: {AppDomain.CurrentDomain.FriendlyName} [path]\n" +
                $"This program counts how many times a filename (minus the extension) " +
                $"appears in the file's contents."
            );
            return 1;
        }

        var path = args[0];
        var filenameWithoutExtension = Path.GetFileNameWithoutExtension(path);
        
        try
        {
            var fileContents = File.ReadAllText(path);
            var stringSearcherOverFileContents = new StringSearcher(fileContents);
            var count = stringSearcherOverFileContents.CountOccurrences(filenameWithoutExtension);

            Console.WriteLine($"Found {count} occurrences in file.");
            return 0;
        }
        catch (FileNotFoundException)
        {
            Console.Error.WriteLine($"Error: The file {path} does not exist.");
            return 1;
        }
    }
}
