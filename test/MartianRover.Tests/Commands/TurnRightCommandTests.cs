using FluentAssertions;
using RedBadgerTest.Commands;
using RedBadgerTest.Models;

namespace RedBadgerTest.Tests.Commands;

public class TurnRightCommandTests
{
    [Theory]
    [InlineData(Orientation.N, Orientation.E)]
    [InlineData(Orientation.E, Orientation.S)]
    [InlineData(Orientation.S, Orientation.W)]
    [InlineData(Orientation.W, Orientation.N)]
    public void Execute_TurnsRobotRight(Orientation start, Orientation expected)
    {
        var robot = new Robot(new Position(0, 0, start));
        var grid = new Grid(5, 5);
        var cmd = new TurnRightCommand();
        cmd.Execute(robot, grid);
        robot.Position.Orientation.Should().Be(expected);
    }
}