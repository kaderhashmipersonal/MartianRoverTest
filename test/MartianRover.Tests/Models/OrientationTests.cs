using FluentAssertions;
using RedBadgerTest.Models;

namespace RedBadgerTest.Tests.Models;

public class OrientationTests
{
    [Theory]
    [InlineData(Orientation.N, 0)]
    [InlineData(Orientation.E, 1)]
    [InlineData(Orientation.S, 2)]
    [InlineData(Orientation.W, 3)]
    public void Orientation_Enum_HasExpectedValues(Orientation orientation, int expectedValue)
    {
        ((int)orientation).Should().Be(expectedValue);
    }
}