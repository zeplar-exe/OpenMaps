using System;

namespace OpenMaps.Common;

public struct IntVector : IEquatable<IntVector>
{
    public int X { get; set; }
    public int Y { get; set; }

    public int Area => X * Y;
    public double Magnitude => Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));

    public IntVector(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool Equals(IntVector other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        return obj is IntVector other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public static bool operator ==(IntVector left, IntVector right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(IntVector left, IntVector right)
    {
        return !left.Equals(right);
    }

    public override string ToString()
    {
        return $"{X}, {Y}";
    }
}