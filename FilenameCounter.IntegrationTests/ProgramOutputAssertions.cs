using FluentAssertions;
using FluentAssertions.Primitives;

namespace FilenameCounter.IntegrationTests;

public class ProgramOutputAssertions : ObjectAssertions<ProgramOutput, ProgramOutputAssertions>
{
    public ProgramOutputAssertions(ProgramOutput programOutput) : base(programOutput) { }
    
    public AndConstraint<ProgramOutputAssertions> SucceedWithCount(int count)
    {
        Subject.Should().Be(new ProgramOutput(0, $"Found {count} occurrences in file.", ""));
        return new(this);
    }

    public AndConstraint<ProgramOutputAssertions> FailWithError(string error)
    {
        Subject.Should().Be(new ProgramOutput(1, "", error));
        return new(this);
    }
}

public static class ProgramOutputExtensions
{
    public static ProgramOutputAssertions Should(this ProgramOutput programOutput)
    {
        return new(programOutput);
    }
}
