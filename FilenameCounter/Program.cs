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

        var f = File.Open(args[0], FileMode.Open);
        int pos = args[0].IndexOf('.');
        string name = args[0].Substring(0, pos);
        System.IO.StreamReader file = new System.IO.StreamReader(f);
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
