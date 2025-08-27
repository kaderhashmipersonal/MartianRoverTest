namespace RedBadgerTest.Models;

public class Grid
{
    public int MaxX { get; }
    public int MaxY { get; }
    private readonly HashSet<(int, int, Orientation)> scents = new();

    public Grid(int maxX, int maxY)
    {
        MaxX = maxX;
        MaxY = maxY;
    }

    public bool IsOffGrid(int x, int y)
        => x < 0 || y < 0 || x > MaxX || y > MaxY;

    public void AddScent(int x, int y, Orientation orientation)
        => scents.Add((x, y, orientation));

    public bool HasScent(int x, int y, Orientation orientation)
        => scents.Contains((x, y, orientation));
}