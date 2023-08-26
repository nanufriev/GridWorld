namespace GridWorld;

public struct Vector2
{
    public int X { get; }
    public int Y { get; }

    public Vector2(int x, int y)
    {
        X = x;
        Y = y;
    }

    public double DistanceTo(Vector2 other)
    {
        return Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
    }

    public override bool Equals(object obj)
    {
        return obj is Vector2 other && X == other.X && Y == other.Y;
    }
}