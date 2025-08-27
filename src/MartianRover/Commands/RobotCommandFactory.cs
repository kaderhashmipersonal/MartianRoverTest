namespace RedBadgerTest.Commands;

public static class RobotCommandFactory
{
    public static IRobotCommand Create(char commandChar)
    {
        return commandChar switch
        {
            'L' => new TurnLeftCommand(),
            'R' => new TurnRightCommand(),
            'F' => new MoveForwardCommand(),
            _ => throw new ArgumentException($"Unknown command: {commandChar}")
        };
    }
}