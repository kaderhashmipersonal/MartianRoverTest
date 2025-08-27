using RedBadgerTest.Commands;
using RedBadgerTest.Models;

public class RobotProcessor
{
    public List<string> Process(string[] inputLines)
    {
        var output = new List<string>();
        var grid = GetGridDimensions(inputLines);

        for (int i = 1; i < inputLines.Length; i += 2)
        {
            if (string.IsNullOrWhiteSpace(inputLines[i]))
            {
                continue;
            }
            var positionParts = inputLines[i].Split(' ');
            var startXCoord = int.Parse(positionParts[0]);
            var startYCoord = int.Parse(positionParts[1]);
            var startOrientation = Enum.Parse<Orientation>(positionParts[2]);
            var startPosition = new Position(startXCoord, startYCoord, startOrientation);
            var robot = new Robot(startPosition);
            var instructions = inputLines[i + 1].Trim();
            foreach (var instructionChar in instructions)
            {
                var command = RobotCommandFactory.Create(instructionChar);
                command.Execute(robot, grid);
                if (robot.IsLost)
                {
                    break;
                }
            }
            output.Add(robot.GetStatus());
        }
        return output;
    }

    private Grid GetGridDimensions(string[] inputLines)
    {
        var gridParts = inputLines[0].Split(' ');
        var gridWidth = int.Parse(gridParts[0]);
        var gridHeight = int.Parse(gridParts[1]);

        return new Grid(gridWidth, gridHeight);
    }
}