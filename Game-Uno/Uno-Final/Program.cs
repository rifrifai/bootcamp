namespace Uno;

class Program
{
  public static void Main()
  {
    Console.Clear();
    SetConsoleColor(ConsoleColor.Yellow);
    Console.WriteLine("🎮" + new string('=', 40) + "🎮");
    Console.WriteLine("      SELAMAT DATANG DI PERMAINAN UNO!");
    Console.WriteLine("🎮" + new string('=', 40) + "🎮");
    ResetConsoleColor();
    Console.WriteLine();

    var gameController = new GameController();

    // setup delegates
    gameController.UnoCallChecker = (player) =>
    {
      Console.WriteLine($"{player.GetName()}, ingin memanggil UNO? (y/n):");
      var input = Console.ReadLine()?.ToLower();
      return input == "y" || input == "yes";
    };

    SetConsoleColor(ConsoleColor.Cyan);
    Console.Write("👥 Masukkan jumlah pemain (2-10): ");
    ResetConsoleColor();
    int numPlayers;
    while (!int.TryParse(Console.ReadLine(), out numPlayers) || numPlayers < 2 || numPlayers > 10)
    {
      SetConsoleColor(ConsoleColor.Red);
      Console.Write("❌ Invalid input. Masukkan jumlah pemain (2-10): ");
      ResetConsoleColor();
    }

    for (int i = 1; i <= numPlayers; i++)
    {
      SetConsoleColor(ConsoleColor.Green);
      Console.Write($"🏷️  Masukkan nama pemain {i}: ");
      ResetConsoleColor();
      string name = Console.ReadLine() ?? $"Pemain {i}";
      gameController.AddPlayer(new Player(name));
    }

    // setup event
    gameController.OnPlayerTurnChanged += (player) =>
    {
      SetConsoleColor(ConsoleColor.Cyan);
      Console.WriteLine($"\n⏳ Tekan Enter ketika {player.GetName()} bersiap untuk bermain...");
      ResetConsoleColor();
      Console.ReadLine();
    };

    gameController.OnCardPlayed += (player, card) =>
    {
      Console.Write($"✅ {player.GetName()} bermain: ");
      if (card.GetColor().HasValue)
      {
        Console.ForegroundColor = card.GetColor()!.Value switch
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
      Console.WriteLine($"\n📊 Kartu di tangan diakhir:");
      foreach (var player in gameController.GetAllPlayers())
      {
        var handSize = gameController.GetPlayerHandSize(player);
        if (handSize == 0)
        {
          SetConsoleColor(ConsoleColor.Green);
          Console.WriteLine($"🏆 {player.GetName()}: {handSize} kartu (WINNER!)");
          ResetConsoleColor();
        }
        else
        {
          Console.WriteLine($"📋 {player.GetName()}: {handSize} kartu");
        }
      }
    };

    static void SetConsoleColor(ConsoleColor color)
    {
      Console.ForegroundColor = color;
    }

    static void ResetConsoleColor()
    {
      Console.ResetColor();
    }

    Console.WriteLine();
    SetConsoleColor(ConsoleColor.Green);
    Console.WriteLine("🚀 Memulai permainan UNO...");
    ResetConsoleColor();
    gameController.StartGame();

    Console.WriteLine();
    SetConsoleColor(ConsoleColor.Yellow);
    Console.WriteLine("🎮 Terima kasih telah bermain UNO! HAVE A GOOOD DAYY...");
    ResetConsoleColor();
    Console.ReadKey();
  }
}
