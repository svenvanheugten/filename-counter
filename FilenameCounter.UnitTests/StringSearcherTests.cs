using AutoFixture.Xunit2;
using FluentAssertions;

namespace FilenameCounter.UnitTests;

public class StringSearcherTests
{
    [Theory, AutoData]
    public void SearchForHayShouldReturnOne(string hay)
    {
        new StringSearcher(hay).CountOccurrences(hay).Should().Be(1);
    }

    [Theory, AutoData]
    public void SearchForEmptyStringShouldReturnHayLength(string hay)
    {
        new StringSearcher(hay).CountOccurrences("").Should().Be(hay.Length);
    }

    [Theory]
    [InlineData("abcbca", "bc", 2)]
    [InlineData("abcxbca", "bc", 2)]
    [InlineData("abcxbca", "c", 2)]
    [InlineData("bca", "bc", 1)]
    [InlineData("abc", "bc", 1)]
    [InlineData("", "bc", 0)]
    public void SearchVarietyOfNeedlesInHayStacks(string hay, string needle, int expected)
    {
        new StringSearcher(hay).CountOccurrences(needle).Should().Be(expected);
    }
}