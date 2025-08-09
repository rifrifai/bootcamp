namespace DI_GameManager;

internal class Program
{
  static void Main(string[] args)
  {
    HumanPlayer humanPlayer = new();
    ComputerPlayer computerPlayer = new();
    GameManager gm = new GameManager(humanPlayer, computerPlayer);

    do
    {
      RoundResult result = gm.PlayGround();

      if (result == RoundResult.Player1Win)
      {
        Console.WriteLine("Player 1 Win");
      }
      else if (result == RoundResult.Player2Win)
      {
        Console.WriteLine("Player 2 Win");
      }
      else
      {
        Console.WriteLine("It\'s a draw!");
      }

      Console.Write("Play Again (Y/N)? ");
    } while (Console.ReadLine()!.ToUpper() == "Y");
  }
}
