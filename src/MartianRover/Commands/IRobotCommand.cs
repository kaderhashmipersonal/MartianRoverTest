using RedBadgerTest.Models;

namespace RedBadgerTest.Commands
{
    public interface IRobotCommand
    {
        void Execute(Robot robot, Grid grid);
    }
}
