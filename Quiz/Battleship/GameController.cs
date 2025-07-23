namespace Battleships
{
  public class GameController
  {
    private Player player1;
    private Player player2;
    private Player currentPlayer;

    public GameController(string name1, string name2)
    {
      player1 = new Player(name1);
      player2 = new Player(name2);
      currentPlayer = player1;
    }

    public void StartGame()
    {
      // Sample manual ship placement (for demo only)
      var ship1 = new Ship("Destroyer", 2);
      player1.Board.PlaceShip(ship1, new List<Coordinate> { new(0, 0), new(0, 1) });

      var ship2 = new Ship("Destroyer", 2);
      player2.Board.PlaceShip(ship2, new List<Coordinate> { new(1, 0), new(1, 1) });
    }

    public bool TakeTurn(Coordinate coord)
    {
      Player opponent = currentPlayer == player1 ? player2 : player1;
      return currentPlayer.Shoot(opponent, coord);
    }

    public void SwitchTurn()
    {
      currentPlayer = currentPlayer == player1 ? player2 : player1;
    }

    public bool IsGameOver()
    {
      return player1.Board.AllShipsSunk() || player2.Board.AllShipsSunk();
    }

    public Player? CheckWinner()
    {
      if (player1.Board.AllShipsSunk()) return player2;
      if (player2.Board.AllShipsSunk()) return player1;
      return null;
    }
  }
}