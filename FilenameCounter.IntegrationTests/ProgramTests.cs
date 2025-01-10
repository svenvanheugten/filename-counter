using AutoFixture.Xunit2;
using FluentAssertions;

namespace FilenameCounter.IntegrationTests;

public class ProgramTests
{
    [Fact] // TODO: Fix this bug.
    public void Main_WithoutArguments_UnfortunatelyThrowsIndexOutOfRangeException()
    {
        var act = () => Act();

        act.Should().Throw<IndexOutOfRangeException>();
    }

    [Theory, AutoData] // TODO: Fix this bug.
    public void Main_WithNonExistingFile_UnfortunatelyThrowsFileNotFoundException(string filename)
    {
        var act = () => Act(filename);

        act.Should().Throw<FileNotFoundException>();
    }
    
    [Theory, AutoData] // TODO: Fix this bug.
    public void Main_WithFileWithoutExtension_UnfortunatelyThrowsArgumentOutOfRangeException(string filename)
    {
        var act = () => ActWithExistingFile(filename, "");
        
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory, AutoData]
    public void Main_WithFileWithExtension_WhichIsEmpty_FindsZeroOccurrences(string filename, string extension)
    {
        ActWithExistingFile($"{filename}.{extension}", "")
            .Should()
            .Be(new ProgramOutput(0, "found 0", ""));
    }
    
    [Theory, AutoData] // TODO: Fix this bug.
    public void Main_WithFileWithExtension_WhichContainsTwoOccurrences_UnfortunatelyFindsZeroOccurrences(
        string filename,
        string extension
    )
    {
        ActWithExistingFile($"{filename}.{extension}", $"{filename}\n{filename}")
            .Should()
            .Be(new ProgramOutput(0, "found 0", ""));
    }
    
    [Theory, AutoData] // TODO: Fix this bug.
    public void Main_WithFileWithTwoExtensions_WhichContainsOneOccurrence_UnfortunatelyFindsZeroOccurrences(
        string filename,
        string extension1,
        string extension2
    )
    {
        ActWithExistingFile($"{filename}.{extension1}.{extension2}", $"{filename}")
            .Should()
            .Be(new ProgramOutput(0, "found 0", ""));
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
