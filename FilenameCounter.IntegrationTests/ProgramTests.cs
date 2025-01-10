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
}