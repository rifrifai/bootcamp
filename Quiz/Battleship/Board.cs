namespace Battleships
{
  public class Board
  {
    private const int Size = 10;
    private Cell[,] grid;
    public List<Ship> Ships { get; }

    public Board()
    {
      grid = new Cell[Size, Size];
      Ships = new List<Ship>();

      for (int x = 0; x < Size; x++)
      {
        for (int y = 0; y < Size; y++)
        {
          grid[x, y] = new Cell(new Coordinate(x, y));
        }
      }
    }

    public void PlaceShip(Ship ship, List<Coordinate> coords)
    {
      ship.Coordinates.AddRange(coords);
      Ships.Add(ship);

      foreach (var coord in coords)
      {
        grid[coord.X, coord.Y].Ship = ship;
      }
    }

    public bool ReceiveShot(Coordinate coord)
    {
      var cell = GetCell(coord);
      cell.MarkHit();

      if (cell.HasShip())
      {
        cell.Ship?.RegisterHit(coord);
        return true;
      }
      return false;
    }

    public bool AllShipsSunk()
    {
      return Ships.All(ship => ship.IsSunk());
    }

    public Cell GetCell(Coordinate coord)
    {
      return grid[coord.X, coord.Y];
    }
  }
}