using FluentAssertions;
using RedBadgerTest.Commands;
using RedBadgerTest.Models;

namespace RedBadgerTest.Tests.Commands;

public class TurnLeftCommandTests
{
    [Theory]
    [InlineData(Orientation.N, Orientation.W)]
    [InlineData(Orientation.W, Orientation.S)]
    [InlineData(Orientation.S, Orientation.E)]
    [InlineData(Orientation.E, Orientation.N)]
    public void Execute_TurnsRobotLeft(Orientation start, Orientation expected)
    {
        var robot = new Robot(new Position(0, 0, start));
        var grid = new Grid(5, 5);
        var cmd = new TurnLeftCommand();
        cmd.Execute(robot, grid);
        robot.Position.Orientation.Should().Be(expected);
    }
}