namespace RedBadgerTest.Models;

public class Robot
{
    public Position Position { get; private set; }
    public bool IsLost { get; private set; }

    public Robot(Position start)
    {
        Position = start;
        IsLost = false;
    }

    public void TurnLeft()
    {
        Position.Orientation = Position.Orientation switch
        {
            Orientation.N => Orientation.W,
            Orientation.W => Orientation.S,
            Orientation.S => Orientation.E,
            Orientation.E => Orientation.N,
            _ => Position.Orientation
        };
    }

    public void TurnRight()
    {
        Position.Orientation = Position.Orientation switch
        {
            Orientation.N => Orientation.E,
            Orientation.E => Orientation.S,
            Orientation.S => Orientation.W,
            Orientation.W => Orientation.N,
            _ => Position.Orientation
        };
    }

    public void MoveForward(Grid grid)
    {
        var (x, y, orientation) = (Position.X, Position.Y, Position.Orientation);
        int newX = x, newY = y;
        switch (orientation)
        {
            case Orientation.N: newY++; break;
            case Orientation.E: newX++; break;
            case Orientation.S: newY--; break;
            case Orientation.W: newX--; break;
        }
        if (grid.IsOffGrid(newX, newY))
        {
            if (!grid.HasScent(x, y, orientation))
            {
                grid.AddScent(x, y, orientation);
                IsLost = true;
            }
            // else: ignore move
        }
        else
        {
            Position = new Position(newX, newY, orientation);
        }
    }

    public string GetStatus()
    {
        return $"{Position}{(IsLost ? " LOST" : string.Empty)}";
    }
}