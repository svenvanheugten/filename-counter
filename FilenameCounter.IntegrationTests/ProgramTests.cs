using AutoFixture.Xunit2;
using FluentAssertions;

namespace FilenameCounter.IntegrationTests;

public class ProgramTests
{
    [Fact]
    public void Main_WithoutArguments_PrintsUsageInstructionsToStderr()
    {
        Act()
            .Should()
            .FailWithError(
                $"Usage: {AppDomain.CurrentDomain.FriendlyName} [path]\n" +
                $"This program counts how many times a filename (minus the extension) " +
                $"appears in the file's contents."
            );
    }

    [Theory, AutoData]
    public void Main_WithNonExistingFile_PrintsReadableError(string path)
    {
        Act(path)
            .Should()
            .FailWithError($"Error: The file {path} does not exist.");
    }
    
    [Theory, AutoData]
    public void Main_WithFileWithoutExtension_WhichIsEmpty_FindsZeroOccurrences(string filename)
    {
        ActWithExistingFile(filename, "")
            .Should()
            .SucceedWithCount(0);
    }

    [Theory, AutoData]
    public void Main_WithFileWithoutExtension_WhichContainsTwoOccurrences_FindsTwoOccurrences(string filename)
    {
        ActWithExistingFile(filename, $"{filename}\n{filename}")
            .Should()
            .SucceedWithCount(2);
    }

    [Theory, AutoData]
    public void Main_WithFileWithExtension_WhichIsEmpty_FindsZeroOccurrences(string filename, string extension)
    {
        ActWithExistingFile($"{filename}.{extension}", "")
            .Should()
            .SucceedWithCount(0);
    }
    
    [Theory, AutoData]
    public void Main_WithFileWithExtension_WhichContainsTwoOccurrences_FindsTwoOccurrences(
        string filename,
        string extension
    )
    {
        ActWithExistingFile($"{filename}.{extension}", $"{filename}\n{filename}")
            .Should()
            .SucceedWithCount(2);
    }
    
    [Theory, AutoData]
    public void Main_WithFileWithTwoExtensions_WhichContainsOneOccurrence_FindsOneOccurrence(
        string filename,
        string extension1,
        string extension2
    )
    {
        ActWithExistingFile($"{filename}.{extension1}.{extension2}", $"{filename}.{extension1}")
            .Should()
            .SucceedWithCount(1);
    }
    
    [Theory, AutoData]
    public void Main_WithFile_WhichContainsTwoOccurrencesInOneLine_FindsTwoOccurrences(string filename)
    {
        ActWithExistingFile(filename, $"{filename}{filename}")
            .Should()
            .SucceedWithCount(2);
    }

    private static ProgramOutput ActWithExistingFile(string filename, string contents)
    {
        var path = Path.Combine(Path.GetTempPath(), filename);
        File.WriteAllText(path, contents);
        return Act(path);
    }

    private static ProgramOutput Act(params string[] args)
    {
        using var stdout = new StringWriter();
        using var stderr = new StringWriter();
        Console.SetOut(stdout);
        Console.SetError(stderr);
        var exitCode = Program.Main(args);
        return new(exitCode, stdout.ToString().Trim(), stderr.ToString().Trim());
    }
}