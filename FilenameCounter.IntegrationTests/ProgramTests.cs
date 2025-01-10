using AutoFixture.Xunit2;
using FluentAssertions;
using ConsoleApp1;

namespace FilenameCounter.IntegrationTests;

public class ProgramTests
{
    [Fact] // TODO: Fix this bug.
    public void Main_WithoutArguments_UnfortunatelyThrowsIndexOutOfRangeException()
    {
        var act = () => Program.Main([]);

        act.Should().Throw<IndexOutOfRangeException>();
    }

    [Theory, AutoData] // TODO: Fix this bug.
    public void Main_WithNonExistingFile_UnfortunatelyThrowsFileNotFoundException(string filename)
    {
        var act = () => Program.Main([filename]);

        act.Should().Throw<FileNotFoundException>();
    }
    
    [Theory, AutoData] // TODO: Fix this bug.
    public void Main_WithFileWithoutExtension_UnfortunatelyThrowsArgumentOutOfRangeException(string filename)
    {
        var path = Path.Combine(Path.GetTempPath(), filename);
        using (File.Create(path)) { }

        var act = () => Program.Main([path]);
        
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory, AutoData]
    public void Main_WithFileWithExtension_ExitsSuccessfully(string filename, string extension)
    {
        var path = Path.Combine(Path.GetTempPath(), $"{filename}.{extension}");
        using (File.Create(path)) { }

        var act = () => Program.Main([path]);

        act.Should().NotThrow();
    }
}
