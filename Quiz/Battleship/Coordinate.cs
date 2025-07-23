namespace Battleships
{
  public class Coordinate
  {
    public int X { get; }
    public int Y { get; }

    public Coordinate(int x, int y)
    {
      X = x;
      Y = y;
    }

    public bool Equals(Coordinate other)
    {
      return X == other.X && Y == other.Y;
    }

    public override string ToString()
    {
      return $"({X}, {Y})";
    }
  }
}