namespace FilenameCounter;

public class StringSearcher
{
    private readonly string _hay;

    public StringSearcher(string hay)
    {
        _hay = hay;
    }

    public int CountOccurrences(string needle)
    {
        var count = 0;
        var startIndex = 0;

        while (startIndex < _hay.Length)
        {
            var indexOfNeedle = _hay.IndexOf(needle, startIndex, StringComparison.InvariantCulture);

            if (indexOfNeedle < 0)
            {
                break;
            }

            count++;
            startIndex = indexOfNeedle + 1;
        }

        return count;
    }
}