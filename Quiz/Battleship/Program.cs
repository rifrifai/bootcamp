namespace Battleships
{
  public class Program
  {
    public static void Main()
    {
      var game = new GameController("Alice", "Bob");
      game.StartGame();

      while (!game.IsGameOver())
      {
        Console.WriteLine("Enter X and Y (0-9) separated by space:");
        var input = Console.ReadLine();
        var parts = input!.Split(' ');
        int x = int.Parse(parts[0]);
        int y = int.Parse(parts[1]);

        var hit = game.TakeTurn(new Coordinate(x, y));
        Console.WriteLine(hit ? "Hit!" : "Miss.");
        game.SwitchTurn();
      }

      var winner = game.CheckWinner();
      Console.WriteLine($"Game Over! Winner: {winner?.Name}");
    }
  }

}