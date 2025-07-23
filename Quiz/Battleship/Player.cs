namespace Battleships
{
  public class Player
  {
    public string Name { get; }
    public Board Board { get; }

    public Player(string name)
    {
      Name = name;
      Board = new Board();
    }

    public bool Shoot(Player opponent, Coordinate target)
    {
      return opponent.Board.ReceiveShot(target);
    }
  }
}