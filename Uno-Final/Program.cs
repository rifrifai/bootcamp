namespace Uno;

class Program
{
  public static void Main()
  {
    Console.Clear();
    SetConsoleColor(ConsoleColor.Yellow);
    Console.WriteLine("🎮" + new string('=', 30) + "🎮");
    Console.WriteLine("          WELCOME TO UNO GAME!");
    Console.WriteLine("🎮" + new string('=', 30) + "🎮");
    ResetConsoleColor();
    Console.WriteLine();

    var gameController = new GameController();

    // setup delegates for interactive gameplay
    gameController.UnoCallChecker = (player) =>
    {
      Console.WriteLine($"{player.GetName()}, do you want to call UNO? (y/n):");
      var input = Console.ReadLine()?.ToLower();
      return input == "y" || input == "yes";
    };

    // get number of players
    SetConsoleColor(ConsoleColor.Cyan);
    Console.Write("👥 Enter number of players (2-10): ");
    ResetConsoleColor();
    int numPlayers;
    while (!int.TryParse(Console.ReadLine(), out numPlayers) || numPlayers < 2 || numPlayers > 10)
    {
      SetConsoleColor(ConsoleColor.Red);
      Console.Write("❌ Invalid input. Enter number of players (2-10): ");
      ResetConsoleColor();
    }

    // Add players
    for (int i = 1; i <= numPlayers; i++)
    {
      SetConsoleColor(ConsoleColor.Green);
      Console.Write($"🏷️ Enter name for Player {i}: ");
      ResetConsoleColor();
      string name = Console.ReadLine() ?? $"Player {i}";
      gameController.AddPlayer(new Player(name));
    }

    // Setup event handlers
    gameController.OnPlayerTurnChanged += (player) =>
    {
      SetConsoleColor(ConsoleColor.Cyan);
      Console.WriteLine($"\n⏳ Press Enter when {player.GetName()} is ready to play...");
      ResetConsoleColor();
      Console.ReadLine();
    };

    gameController.OnCardPlayed += (player, card) =>
    {
      Console.Write($"✅ {player.GetName()} played: ");
      if (card.GetColor().HasValue)
      {
        Console.ForegroundColor = card.GetColor().Value switch
        {
          Color.Red => ConsoleColor.Red,
          Color.Blue => ConsoleColor.Blue,
          Color.Green => ConsoleColor.Green,
          Color.Yellow => ConsoleColor.Yellow,
          _ => ConsoleColor.White
        };
      }
      else
      {
        Console.ForegroundColor = ConsoleColor.Magenta;
      }
      Console.WriteLine(card.GetDisplayText());
      Console.ResetColor();
    };

    gameController.OnGameEnded += (winner) =>
    {
      Console.WriteLine($"\n📊 Final hand sizes:");
      foreach (var player in gameController.GetAllPlayers())
      {
        var handSize = gameController.GetPlayerHandSize(player);
        if (handSize == 0)
        {
          SetConsoleColor(ConsoleColor.Green);
          Console.WriteLine($"🏆 {player.GetName()}: {handSize} cards (WINNER!)");
          ResetConsoleColor();
        }
        else
        {
          Console.WriteLine($"📋 {player.GetName()}: {handSize} cards");
        }
      }
    };

    // Helper method for main program
    static void SetConsoleColor(ConsoleColor color)
    {
      Console.ForegroundColor = color;
    }

    static void ResetConsoleColor()
    {
      Console.ResetColor();
    }

    // Start the game
    Console.WriteLine();
    SetConsoleColor(ConsoleColor.Green);
    Console.WriteLine("🚀 Starting UNO Game...");
    ResetConsoleColor();
    gameController.StartGame();

    Console.WriteLine();
    SetConsoleColor(ConsoleColor.Yellow);
    Console.WriteLine("🎮 Thanks for playing UNO! Press any key to exit...");
    ResetConsoleColor();
    Console.ReadKey();
  }
}
