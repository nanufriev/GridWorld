namespace GridWorld;

public readonly struct Vector2
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
        return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
    }

    public bool Equals(Vector2 other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object obj)
    {
        return obj is Vector2 other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}
