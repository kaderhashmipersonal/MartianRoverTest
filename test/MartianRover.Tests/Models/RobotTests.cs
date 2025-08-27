using FluentAssertions;
using RedBadgerTest.Models;

namespace RedBadgerTest.Tests.Models;

public class RobotTests
{
    [Theory]
    [InlineData(0, 0, Orientation.S, "LFF", 2, 0, Orientation.E, false)]
    [InlineData(1, 1, Orientation.E, "RFRFRFRF", 1, 1, Orientation.E, false)]
    [InlineData(3, 2, Orientation.N, "FRRFLLFFRRFLL", 3, 3, Orientation.N, true)]
    [InlineData(0, 3, Orientation.W, "LLFFFLFLFL", 3, 3, Orientation.N, true)]
    [InlineData(4, 3, Orientation.N, "RFLFF", 5, 3, Orientation.N, true)]
    public void Robot_ExecutesSampleInputScenario(int x, int y, Orientation orientation, string instructions, int expectedX, int expectedY, Orientation expectedOrientation, bool expectedLost)
    {
        var grid = new Grid(5, 3);
        var robot = new Robot(new Position(x, y, orientation));
        foreach (var c in instructions)
        {
            var command = RedBadgerTest.Commands.RobotCommandFactory.Create(c);
            command.Execute(robot, grid);
            if (robot.IsLost) break;
        }
        robot.Position.X.Should().Be(expectedX);
        robot.Position.Y.Should().Be(expectedY);
        robot.Position.Orientation.Should().Be(expectedOrientation);
        robot.IsLost.Should().Be(expectedLost);
    }

    [Fact]
    public void TurnLeft_And_TurnRight_ChangeOrientationCorrectly()
    {
        var robot = new Robot(new Position(0, 0, Orientation.N));
        robot.TurnLeft();
        robot.Position.Orientation.Should().Be(Orientation.W);
        robot.TurnRight();
        robot.Position.Orientation.Should().Be(Orientation.N);
    }

    [Fact]
    public void MoveForward_UpdatesPositionAndLostStatus()
    {
        var grid = new Grid(1, 1);
        var robot = new Robot(new Position(1, 1, Orientation.N));
        robot.MoveForward(grid);
        robot.IsLost.Should().BeTrue();
        robot.Position.X.Should().Be(1);
        robot.Position.Y.Should().Be(1);
    }

    [Fact]
    public void GetStatus_ReturnsCorrectString()
    {
        var robot = new Robot(new Position(1, 2, Orientation.N));
        robot.GetStatus().Should().Be("1 2 N");
        var grid = new Grid(1, 1);
        robot = new Robot(new Position(1, 1, Orientation.N));
        robot.MoveForward(grid);
        robot.GetStatus().Should().Be("1 1 N LOST");
    }
}