using FluentAssertions;
using RedBadgerTest.Commands;
using RedBadgerTest.Models;

namespace RedBadgerTest.Tests.Commands;

public class MoveForwardCommandTests
{
    [Theory]
    [InlineData(0, 0, Orientation.N, 0, 1)]
    [InlineData(1, 1, Orientation.E, 2, 1)]
    [InlineData(2, 2, Orientation.S, 2, 1)]
    [InlineData(3, 3, Orientation.W, 2, 3)]
    public void Execute_MovesRobotForward(int x, int y, Orientation orientation, int expectedX, int expectedY)
    {
        var robot = new Robot(new Position(x, y, orientation));
        var grid = new Grid(5, 5);
        var cmd = new MoveForwardCommand();
        cmd.Execute(robot, grid);
        robot.Position.X.Should().Be(expectedX);
        robot.Position.Y.Should().Be(expectedY);
    }

    [Fact]
    public void Execute_SetsLost_WhenMovingOffGrid()
    {
        var robot = new Robot(new Position(0, 0, Orientation.S));
        var grid = new Grid(5, 5);
        var cmd = new MoveForwardCommand();
        cmd.Execute(robot, grid);
        robot.IsLost.Should().BeTrue();
    }
}