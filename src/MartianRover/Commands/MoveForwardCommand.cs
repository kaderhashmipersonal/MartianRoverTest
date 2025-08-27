using RedBadgerTest.Models;

namespace RedBadgerTest.Commands;
    
public class MoveForwardCommand : IRobotCommand
{
    public void Execute(Robot robot, Grid grid)
    {
        robot.MoveForward(grid);
    }
}
