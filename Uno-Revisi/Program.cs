using UnoRevisi.Controller;
using UnoRevisi.Enums;
using UnoRevisi.Interfaces;
using UnoRevisi.Models;

namespace UnoRevisi;

class Program
{
  static List<IPlayer> players = new();
  static GameController gameController = null;
  static int numPlayers;



  public static void Main()
  {
    InitGame();
    InitPlayer();
    CreatePlayers();
    StartGame();
    PlayerChangedConfirm();
    CardPlayed();
    UnoCalled();
    GetWinner();
    EndGame();
  }

  static void SetConsoleColor(ConsoleColor color)
  {
    Console.ForegroundColor = color;
  }

  static void ResetConsoleColor()
  {
    Console.ResetColor();
  }

  private static void InitGame()
  {
    if (gameController == null)
    {
      gameController = new GameController(players);
    }
  }
  private static void InitPlayer()
  {
    Console.Clear();
    SetConsoleColor(ConsoleColor.Yellow);
    Console.WriteLine("🎮" + new string('=', 40) + "🎮");
    Console.WriteLine("      SELAMAT DATANG DI PERMAINAN UNO!");
    Console.WriteLine("🎮" + new string('=', 40) + "🎮");
    ResetConsoleColor();
    Console.WriteLine();

    SetConsoleColor(ConsoleColor.Cyan);
    Console.Write("👥 Masukkan jumlah pemain (2-10): ");
    ResetConsoleColor();
    while (!int.TryParse(Console.ReadLine(), out numPlayers) || numPlayers < 2 || numPlayers > 10)
    {
      SetConsoleColor(ConsoleColor.Red);
      Console.Write("❌ Invalid input. Masukkan jumlah pemain (2-10): ");
      ResetConsoleColor();
    }
  }

  private static void CreatePlayers()
  {
    for (int i = 1; i <= numPlayers; i++)
    {
      SetConsoleColor(ConsoleColor.Green);
      Console.Write($"🏷️  Masukkan nama pemain {i}: ");
      ResetConsoleColor();
      string name = Console.ReadLine() ?? $"Pemain {i}";
      players.Add(new Player(name));
    }
  }

  static void StartGame()
  {
    Console.WriteLine();
    SetConsoleColor(ConsoleColor.Green);
    Console.WriteLine("🚀 Memulai permainan UNO...");
    ResetConsoleColor();
    gameController.StartGame();
  }

  private static void PlayerChangedConfirm()
  {
    // setup event
    gameController.OnPlayerTurnChanged += (player) =>
    {
      SetConsoleColor(ConsoleColor.Cyan);
      Console.WriteLine($"\n⏳ Tekan Enter ketika {player.GetName()} bersiap untuk bermain...");
      ResetConsoleColor();
      Console.ReadLine();
    };
  }

  private static void CardPlayed()
  {
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
  }
















  private static void UnoCalled()
  {
    // setup delegates
    gameController.UnoCallChecker = (player) =>
    {
      Console.WriteLine($"{player.GetName()}, ingin memanggil UNO? (y/n):");
      var input = Console.ReadLine()?.ToLower();
      return input == "y" || input == "yes";
    };
  }

  private static void GetWinner()
  {
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
  }

  private static void EndGame()
  {
    Console.WriteLine();
    SetConsoleColor(ConsoleColor.Yellow);
    Console.WriteLine("🎮 Terima kasih telah bermain UNO! HAVE A GOOOD DAYY...");
    ResetConsoleColor();
    Console.ReadKey();
  }
}
