using FluentAssertions;
using RedBadgerTest.Models;

namespace RedBadgerTest.Tests.Models;

public class PositionTests
{
    [Fact]
    public void Constructor_SetsPropertiesCorrectly()
    {
        var pos = new Position(1, 2, Orientation.E);
        pos.X.Should().Be(1);
        pos.Y.Should().Be(2);
        pos.Orientation.Should().Be(Orientation.E);
    }

    [Fact]
    public void ToString_ReturnsCorrectFormat()
    {
        var pos = new Position(3, 4, Orientation.W);
        pos.ToString().Should().Be("3 4 W");
    }
}