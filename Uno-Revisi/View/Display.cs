using UnoRevisi.Enums;
using UnoRevisi.Interfaces;

namespace UnoRevisi.View;

public class Display
{
  public void ShowWelcome()
  {
    Console.Clear();
    SetConsoleColor(ConsoleColor.Yellow);
    Console.WriteLine("🎮" + new string('=', 40) + "🎮");
    Console.WriteLine("      SELAMAT DATANG DI PERMAINAN UNO!");
    Console.WriteLine("🎮" + new string('=', 40) + "🎮");
    ResetConsoleColor();
    Console.WriteLine();
  }

  public void ShowInsufficientPlayers()
  {
    SetConsoleColor(ConsoleColor.Red);
    Console.WriteLine("Perlu minimal 2 permain untuk bermain!");
    ResetConsoleColor();
  }

  public void ShowGameStart(ICard firstCard, Color? wildColor = null)
  {
    Console.Write("🎯 Game dimulai! Kartu pertama: ");
    if (firstCard.GetColor().HasValue)
    {
      SetConsoleColor(GetColorFromEnum(firstCard.GetColor()!.Value));
    }
    else
    {
      SetConsoleColor(ConsoleColor.Magenta);
    }
    Console.WriteLine(firstCard.GetDisplayText());
    ResetConsoleColor();

    if (wildColor.HasValue)
    {
      SetConsoleColor(GetColorFromEnum(wildColor.Value));
      Console.WriteLine($"🌈 Kartu Wild dipilih: {wildColor}");
      ResetConsoleColor();
    }
  }

  public void ShowGameState(IPlayer currentPlayer, ICard topCard, Color? currentWildColor, List<ICard> playerHand)
  {
    Console.Clear();
    Console.WriteLine("\n" + new string('=', 50));
    SetConsoleColor(ConsoleColor.Green);
    Console.WriteLine($"🎯 Giliran {currentPlayer.GetName()}");
    ResetConsoleColor();
    Console.WriteLine(new string('=', 50));

    Console.Write("🎴 Kartu teratas: ");
    if (topCard?.GetColor().HasValue == true)
    {
      SetConsoleColor(GetColorFromEnum(topCard.GetColor()!.Value));
    }
    else
    {
      SetConsoleColor(ConsoleColor.Magenta);
    }
    Console.WriteLine(topCard?.GetDisplayText());
    ResetConsoleColor();

    if (currentWildColor.HasValue)
    {
      SetConsoleColor(GetColorFromEnum(currentWildColor.Value));
      Console.WriteLine($"🌈 Warna wild saat ini: {currentWildColor}");
      ResetConsoleColor();
    }

    DisplayPlayerHand(currentPlayer, playerHand);
  }

  public void ShowCardDrawn(ICard drawnCard)
  {
    SetConsoleColor(ConsoleColor.Red);
    Console.WriteLine("❌ Tidak ada kartu yang bisa dimainkan. Mengambil kartu...");
    ResetConsoleColor();

    Console.Write("📤 Mengambil Kartu: ");
    if (drawnCard.GetColor().HasValue)
    {
      SetConsoleColor(GetColorFromEnum(drawnCard.GetColor()!.Value));
    }
    else
    {
      SetConsoleColor(ConsoleColor.Magenta);
    }
    Console.WriteLine(drawnCard.GetDisplayText());
    ResetConsoleColor();
  }

  public void ShowTurnSkipped()
  {
    SetConsoleColor(ConsoleColor.Yellow);
    Console.WriteLine("⏭️  Masih tidak ada kartu yang bisa dimainkan. Giliran dilewati.");
    ResetConsoleColor();
  }

  public void ShowPlayableCards(List<ICard> playableCards)
  {
    SetConsoleColor(ConsoleColor.Yellow);
    Console.WriteLine("🎯 Pilihan kartu tersedia:");
    ResetConsoleColor();

    for (int i = 0; i < playableCards.Count; i++)
    {
      Console.Write($"{i + 1}. ");
      var card = playableCards[i];
      if (card.GetColor().HasValue)
      {
        SetConsoleColor(GetColorFromEnum(card.GetColor()!.Value));
      }
      else
      {
        SetConsoleColor(ConsoleColor.Magenta);
      }
      Console.WriteLine(card.GetDisplayText());
      ResetConsoleColor();
    }
  }

  public void ShowInvalidChoiceMessage()
  {
    SetConsoleColor(ConsoleColor.Red);
    Console.WriteLine("❌ Pilihan tidak valid. Silahkan coba lagi:");
    ResetConsoleColor();
  }

  public void ShowCardPlayed(IPlayer player, ICard card)
  {
    Console.Write($"✅ {player.GetName()} bermain: ");
    if (card.GetColor().HasValue)
    {
      SetConsoleColor(GetColorFromEnum(card.GetColor()!.Value));
    }
    else
    {
      SetConsoleColor(ConsoleColor.Magenta);
    }
    Console.WriteLine(card.GetDisplayText());
    ResetConsoleColor();
  }

  public bool CheckUnoCall(IPlayer player)
  {
    SetConsoleColor(ConsoleColor.Yellow);
    Console.WriteLine($"🎯 {player.GetName()}, ingin memanggil UNO? (y/n):");
    ResetConsoleColor();
    var input = Console.ReadLine()?.ToLower();
    return input == "y" || input == "yes";
  }

  public void ShowUnoViolation(IPlayer player)
  {
    SetConsoleColor(ConsoleColor.Red);
    Console.WriteLine($"⚠️ {player.GetName()} lupa memanggil UNO! Hukuman mengambil 2 kartu.");
    ResetConsoleColor();
  }

  public void ShowUnoCall(IPlayer player)
  {
    SetConsoleColor(ConsoleColor.Yellow);
    Console.WriteLine($"🎉 {player.GetName()} memanggil UNO!");
    ResetConsoleColor();
  }

  public void PrintEffect((string message, ConsoleColor color)? effect)
  {
    if (effect is null) return;

    var (message, color) = effect.Value;
    SetConsoleColor(color);
    Console.WriteLine(message);
    ResetConsoleColor();
  }

  public void ShowWildColorChoices()
  {
    SetConsoleColor(ConsoleColor.Cyan);
    Console.WriteLine("🎨 Memilih warna:");
    ResetConsoleColor();

    var colors = (Color[])Enum.GetValues(typeof(Color));
    for (int i = 0; i < colors.Length; i++)
    {
      SetConsoleColor(GetColorFromEnum(colors[i]));
      Console.WriteLine($"{i + 1}. {GetColorEmoji(colors[i])} {colors[i]}");
      ResetConsoleColor();
    }
  }

  public void ShowInvalidColorChoice()
  {
    SetConsoleColor(ConsoleColor.Red);
    Console.WriteLine("❌ Pilihan tidak valid. Silahkan coba lagi:");
    ResetConsoleColor();
  }

  public void ShowDeckRecycled()
  {
    SetConsoleColor(ConsoleColor.Cyan);
    Console.WriteLine("🔄 Deck kosong. Mengocok ulang discard pile.");
    ResetConsoleColor();
  }

  public void ShowGameEnd(IPlayer winner, List<(IPlayer Player, int HandSize)> finalStats)
  {
    Console.WriteLine("\n" + string.Concat(Enumerable.Repeat("🎉", 20)));
    SetConsoleColor(ConsoleColor.Green);
    Console.WriteLine($"🏆 {winner.GetName()} MEMENANGKAN PERMAINAN! 🏆");
    ResetConsoleColor();
    Console.WriteLine(string.Concat(Enumerable.Repeat("🎉", 20)));

    Console.WriteLine($"\n📊 Kartu di tangan di akhir:");
    foreach (var (player, handSize) in finalStats)
    {
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
  }

  public void ShowPlayerTurnWait(IPlayer player)
  {
    SetConsoleColor(ConsoleColor.Cyan);
    Console.WriteLine($"\n⏳ Tekan Enter ketika {player.GetName()} bersiap untuk bermain...");
    ResetConsoleColor();
    Console.ReadLine();
  }

  public int GetPlayerCount()
  {
    SetConsoleColor(ConsoleColor.Cyan);
    Console.Write("👥 Masukkan jumlah pemain (2-10): ");
    ResetConsoleColor();

    int numPlayers;
    while (!int.TryParse(Console.ReadLine(), out numPlayers) || numPlayers < 2 || numPlayers > 10)
    {
      SetConsoleColor(ConsoleColor.Red);
      Console.Write("❌ Input tidak valid. Masukkan jumlah pemain (2-10): ");
      ResetConsoleColor();
    }
    return numPlayers;
  }

  public string GetPlayerName(int playerNumber)
  {
    SetConsoleColor(ConsoleColor.Green);
    Console.Write($"🏷️  Masukkan nama pemain {playerNumber}: ");
    ResetConsoleColor();
    string? input = Console.ReadLine();
    string name = string.IsNullOrWhiteSpace(input) ? $"Pemain {playerNumber + 1}" : input;
    return name;
  }

  public void ShowGameStarting()
  {
    Console.WriteLine();
    SetConsoleColor(ConsoleColor.Green);
    Console.WriteLine("🚀 Memulai permainan UNO...");
    ResetConsoleColor();
  }

  public void ShowGameEnd()
  {
    Console.WriteLine();
    SetConsoleColor(ConsoleColor.Yellow);
    Console.WriteLine("🎮 Terima kasih telah bermain UNO! HAVE A GOOD DAY...");
    ResetConsoleColor();
    Console.ReadKey();
  }

  private void DisplayPlayerHand(IPlayer player, List<ICard> hand)
  {
    SetConsoleColor(ConsoleColor.Cyan);
    Console.WriteLine($"\n🎴 {player.GetName()}'s hand ({hand.Count} kartu):");
    ResetConsoleColor();

    for (int i = 0; i < hand.Count; i++)
    {
      Console.Write($"{i + 1}. ");

      var card = hand[i];
      if (card.GetColor().HasValue)
      {
        SetConsoleColor(GetColorFromEnum(card.GetColor()!.Value));
      }
      else
      {
        SetConsoleColor(ConsoleColor.Magenta);
      }

      Console.WriteLine(card.GetDisplayText());
      ResetConsoleColor();
    }
  }

  private ConsoleColor GetColorFromEnum(Color color)
  {
    return color switch
    {
      Color.Red => ConsoleColor.Red,
      Color.Blue => ConsoleColor.Blue,
      Color.Green => ConsoleColor.Green,
      Color.Yellow => ConsoleColor.Yellow,
      _ => ConsoleColor.White
    };
  }

  public void SetConsoleColor(ConsoleColor color)
  {
    Console.ForegroundColor = color;
  }

  static void ResetConsoleColor()
  {
    Console.ResetColor();
  }

  private string GetColorEmoji(Color color)
  {
    return color switch
    {
      Color.Red => "🔴",
      Color.Blue => "🔵",
      Color.Green => "🟢",
      Color.Yellow => "🟡",
      _ => "⚫"
    };
  }
}