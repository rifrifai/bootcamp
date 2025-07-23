namespace Battleships
{
  public class Cell
  {
    public Coordinate Position { get; }
    public bool IsHit { get; private set; }
    public Ship? Ship { get; set; }

    public Cell(Coordinate position)
    {
      Position = position;
      IsHit = false;
    }

    public void MarkHit()
    {
      IsHit = true;
    }

    public bool HasShip()
    {
      return Ship != null;
    }
  }
}