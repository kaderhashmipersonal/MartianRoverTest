using RedBadgerTest.Models;

namespace RedBadgerTest.Commands;

public class TurnLeftCommand : IRobotCommand
{
    public void Execute(Robot robot, Grid grid)
    {
        robot.TurnLeft();
    }
}