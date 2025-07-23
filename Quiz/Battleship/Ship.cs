namespace Battleships
{
  public class Ship
  {
    public string Name { get; }
    public int Size { get; }
    private int hits;
    public List<Coordinate> Coordinates { get; }

    public Ship(string name, int size)
    {
      Name = name;
      Size = size;
      Coordinates = new List<Coordinate>();
      hits = 0;
    }

    public void RegisterHit(Coordinate coord)
    {
      if (Occupies(coord))
      {
        hits++;
      }
    }

    public bool IsSunk()
    {
      return hits >= Size;
    }

    public bool Occupies(Coordinate coord)
    {
      return Coordinates.Any(c => c.Equals(coord));
    }
  }
}