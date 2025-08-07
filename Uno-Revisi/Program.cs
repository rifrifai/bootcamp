using UnoRevisi.Controller;
using UnoRevisi.Interfaces;
using UnoRevisi.Models;
using UnoRevisi.View;

namespace UnoRevisi;

class Program
{
  static void Main()
  {
    List<IPlayer> players = new List<IPlayer>();
    GameController gameController = new GameController(players);
    Display display = new Display(gameController);

    display.ShowWelcome();

    // var gameController = new GameController();

    // IPlayer player = new Player(name: "player 1");

    // Display display = new Display(gameController);

    // SetupEventHandlers(gameController, display);

    // display.ShowGameStarting();

    // bool gameStarted = gameController.StartGame();
    // if (!gameStarted)
    // {
    //   display.ShowInsufficientPlayers();
    //   return;
    // }

    // display.ShowGameEnd();
  }


  // static Display SetupDisplay()
  // {
  //   var display = new Display();
  //   display.ShowWelcome();
  //   return display;
  // }

  // static List<IPlayer> SetupPlayers(Display display)
  // {
  //   int numPlayers = display.GetPlayerCount();
  //   var players = new List<IPlayer>();

  //   for (int i = 1; i < numPlayers; i++)
  //   {
  //     string name = display.GetPlayerName(i);
  //     players.Add(new Player(name));
  //   }
  //   return players;set
  // }

  // static GameController CreateGameController(List<IPlayer> players)
  // {
  //   var startGame = new GameController(players);
  //   return startGame;
  // }
}
