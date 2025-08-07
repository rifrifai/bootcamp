using UnoRevisi.View;
using UnoRevisi.Controller;
using UnoRevisi.Interfaces;

namespace UnoRevisi;

class Program
{
  private static Display _display;
  private static List<IPlayer> _players = null;
  private static GameController _gameController = null;

  public static void Main()
  {
    ShowWelcome();

    while (true)
    {

      if (!SetupAndStartNewGame()) break;

      _display.RunGame();

      _display.ShowGameEnd();

      if (!_display.AskPlayAgain()) break;
    }
  }

  public static bool SetupAndStartNewGame()
  {
    _players = _display.SetupPlayers();
    _gameController = new GameController(_players);

    if (_players.Count < 2)
    {
      _display.ShowInsufficientPlayers();
      return false;
    }

    _display.SetupEventHandlers();

    _display.ShowGameStarting();

    return _display.StartGame();
  }

  public static void ShowWelcome()
  {
    Console.Clear();
    Console.WriteLine("🎮" + new string('=', 40) + "🎮");
    Console.WriteLine("      SELAMAT DATANG DI PERMAINAN UNO!");
    Console.WriteLine("🎮" + new string('=', 40) + "🎮");
    Console.WriteLine();
  }
}