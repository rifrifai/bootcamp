using UnoRevisi.Controller;
using UnoRevisi.Interfaces;
using UnoRevisi.Models;
using UnoRevisi.View;

namespace UnoRevisi;

class Program
{
  static void Main()
  {
    var display = SetupDisplay();

    var players = SetupPlayers(display);

    var gameController = CreateGameController(players, display);

    SetupEventHandlers(gameController, display);

    display.ShowGameStarting();

    bool gameStarted = gameController.StartGame();
    if (!gameStarted)
    {
      display.ShowInsufficientPlayers();
      return;
    }

    display.ShowGameEnd();
  }


  static Display SetupDisplay()
  {
    var display = new Display();
    display.ShowWelcome();
    return display;
  }

  static List<IPlayer> SetupPlayers(Display display)
  {
    int numPlayers = display.GetPlayerCount();
    var players = new List<IPlayer>();

    for (int i = 1; i < numPlayers; i++)
    {
      string name = display.GetPlayerName(i);
      players.Add(new Player(name));
    }
    return players;
  }

  static GameController CreateGameController(List<IPlayer> players, Display display)
  {
    var startGame = new GameController(players, display);
    return startGame;
  }

  static void SetupEventHandlers(GameController gameController, Display display)
  {
    gameController.OnPlayerTurnChanged += (player) =>
    {
      display.ShowPlayerTurnWait(player);
    };

    gameController.OnCardPlayed += (player, card) =>
    {
      display.ShowCardPlayed(player, card);
    };
  }
}
