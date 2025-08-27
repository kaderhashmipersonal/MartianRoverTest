using FluentAssertions;
using RedBadgerTest.Models;

namespace RedBadgerTest.Tests.Models;

public class GridTests
{
    [Fact]
    public void Constructor_SetsMaxXAndMaxY()
    {
        var grid = new Grid(10, 20);
        grid.MaxX.Should().Be(10);
        grid.MaxY.Should().Be(20);
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(0, -1)]
    [InlineData(11, 0)]
    [InlineData(0, 21)]
    public void IsOffGrid_ReturnsTrue_WhenOutOfBounds(int x, int y)
    {
        var grid = new Grid(10, 20);
        grid.IsOffGrid(x, y).Should().BeTrue();
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(10, 20)]
    [InlineData(5, 5)]
    public void IsOffGrid_ReturnsFalse_WhenInBounds(int x, int y)
    {
        var grid = new Grid(10, 20);
        grid.IsOffGrid(x, y).Should().BeFalse();
    }

    [Fact]
    public void AddScent_And_HasScent_WorkCorrectly()
    {
        var grid = new Grid(2, 2);
        grid.HasScent(1, 1, Orientation.N).Should().BeFalse();
        grid.AddScent(1, 1, Orientation.N);
        grid.HasScent(1, 1, Orientation.N).Should().BeTrue();
    }

    [Fact]
    public void HasScent_ReturnsFalse_ForDifferentOrientation()
    {
        var grid = new Grid(2, 2);
        grid.AddScent(1, 1, Orientation.N);
        grid.HasScent(1, 1, Orientation.E).Should().BeFalse();
    }
}