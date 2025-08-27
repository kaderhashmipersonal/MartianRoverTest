using RedBadgerTest.Models;

namespace RedBadgerTest.Commands;

public class TurnRightCommand : IRobotCommand
{
    public void Execute(Robot robot, Grid grid)
    {
        robot.TurnRight();
    }
}