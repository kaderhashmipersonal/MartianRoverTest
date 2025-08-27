using FluentAssertions;
using RedBadgerTest.Commands;

namespace RedBadgerTest.Tests.Commands;

public class RobotCommandFactoryTests
{
    [Theory]
    [InlineData('L', typeof(TurnLeftCommand))]
    [InlineData('R', typeof(TurnRightCommand))]
    [InlineData('F', typeof(MoveForwardCommand))]
    public void Create_ReturnsCorrectCommandType(char c, Type expectedType)
    {
        var cmd = RobotCommandFactory.Create(c);
        cmd.Should().BeOfType(expectedType);
    }

    [Fact]
    public void Create_Throws_OnInvalidCommand()
    {
        Action act = () => RobotCommandFactory.Create('X');
        act.Should().Throw<ArgumentException>();
    }
}