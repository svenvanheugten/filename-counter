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
            .Be(new ProgramOutput(1, "", $"Usage: {AppDomain.CurrentDomain.FriendlyName} [path]"));
    }

    [Theory, AutoData]
    public void Main_WithNonExistingFile_PrintsReadableError(string path)
    {
        Act(path)
            .Should()
            .Be(new ProgramOutput(1, "", $"Error: The file {path} does not exist."));
    }
    
    [Theory, AutoData]
    public void Main_WithFileWithoutExtension_WhichIsEmpty_FindsZeroOccurrences(string filename)
    {
        ActWithExistingFile(filename, "")
            .Should()
            .Be(new ProgramOutput(0, "found 0", ""));
    }

    [Theory, AutoData]
    public void Main_WithFileWithoutExtension_WhichContainsTwoOccurrences_FindsTwoOccurrences(string filename)
    {
        ActWithExistingFile(filename, $"{filename}\n{filename}")
            .Should()
            .Be(new ProgramOutput(0, "found 2", ""));
    }

    [Theory, AutoData]
    public void Main_WithFileWithExtension_WhichIsEmpty_FindsZeroOccurrences(string filename, string extension)
    {
        ActWithExistingFile($"{filename}.{extension}", "")
            .Should()
            .Be(new ProgramOutput(0, "found 0", ""));
    }
    
    [Theory, AutoData]
    public void Main_WithFileWithExtension_WhichContainsTwoOccurrences_FindsTwoOccurrences(
        string filename,
        string extension
    )
    {
        ActWithExistingFile($"{filename}.{extension}", $"{filename}\n{filename}")
            .Should()
            .Be(new ProgramOutput(0, "found 2", ""));
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
            .Be(new ProgramOutput(0, "found 1", ""));
    }
    
    [Theory, AutoData]
    public void Main_WithFile_WhichContainsTwoOccurrencesInOneLine_FindsTwoOccurrences(string filename)
    {
        ActWithExistingFile(filename, $"{filename}{filename}")
            .Should()
            .Be(new ProgramOutput(0, "found 2", ""));
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

    private record ProgramOutput(int ExitCode, string Stdout, string Stderr);
}
