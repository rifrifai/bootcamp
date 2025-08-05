using System;
using System.Collections.Generic;
using System.Linq;

// Enumerations
public enum Color
{
  Red,
  Blue,
  Green,
  Yellow
}

public enum CardType
{
  Number,
  Action,
  Wild
}

public enum ActionType
{
  Skip,
  Reverse,
  DrawTwo
}

public enum WildType
{
  Wild,
  WildDrawFour
}

public enum Number
{
  Zero,
  One,
  Two,
  Three,
  Four,
  Five,
  Six,
  Seven,
  Eight,
  Nine
}

// Interfaces
public interface ICard
{
  CardType GetCardType();
  Color? GetColor();
  Number? GetNumber();
  ActionType? GetActionType();
  WildType? GetWildType();
  string GetDisplayText();
  void SetCardType(CardType type);
  void SetColor(Color? color);
  void SetNumber(Number? number);
  void SetActionType(ActionType? actionType);
  void SetWildType(WildType? wildType);
}

public interface IPlayer
{
  string GetName();
  void SetName(string name);
}

public interface IDeck
{
  List<ICard> GetCards();
  ICard GetCardAt(int index);
  void SetCards(List<ICard> cards);
  void SetCardAt(int index, ICard card);
}

public interface IDiscardPile
{
  List<ICard> GetCards();
  ICard GetCardAt(int index);
  void SetCards(List<ICard> cards);
  void SetCardAt(int index, ICard card);
}

// Implementations
public class Card : ICard
{
  private CardType _type;
  private Color? _color;
  private Number? _number;
  private ActionType? _actionType;
  private WildType? _wildType;

  public Card(CardType type, Color? color = null, Number? number = null, ActionType? actionType = null, WildType? wildType = null)
  {
    _type = type;
    _color = color;
    _number = number;
    _actionType = actionType;
    _wildType = wildType;
  }

  public CardType GetCardType() => _type;
  public Color? GetColor() => _color;
  public Number? GetNumber() => _number;
  public ActionType? GetActionType() => _actionType;
  public WildType? GetWildType() => _wildType;

  public void SetCardType(CardType type) => _type = type;
  public void SetColor(Color? color) => _color = color;
  public void SetNumber(Number? number) => _number = number;
  public void SetActionType(ActionType? actionType) => _actionType = actionType;
  public void SetWildType(WildType? wildType) => _wildType = wildType;

  public string GetDisplayText()
  {
    switch (_type)
    {
      case CardType.Number:
        return $"{GetColorEmoji(_color)} {_color} {_number}";
      case CardType.Action:
        return $"{GetColorEmoji(_color)} {_color} {GetActionEmoji(_actionType)} {_actionType}";
      case CardType.Wild:
        return $"🌈 {_wildType}";
      default:
        return "❓ Unknown Card";
    }
  }

  private string GetColorEmoji(Color? color)
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
  // private string GetColorEmoji(Color color)
  //     {
  //         return color switch
  //         {
  //             Color.Red => "🔴",
  //             Color.Blue => "🔵",
  //             Color.Green => "🟢",
  //             Color.Yellow => "🟡",
  //             _ => "⚫"
  //         };
  //     }

  private string GetActionEmoji(ActionType? action)
  {
    return action switch
    {
      ActionType.Skip => "🚫",
      ActionType.Reverse => "🔄",
      ActionType.DrawTwo => "📥",
      _ => "❓"
    };
  }
}

public class Player : IPlayer
{
  private string _name;

  public Player(string name)
  {
    _name = name;
  }

  public string GetName() => _name;
  public void SetName(string name) => _name = name;
}

public class Deck : IDeck
{
  private List<ICard> _cards;

  public Deck()
  {
    _cards = new List<ICard>();
  }

  public List<ICard> GetCards() => _cards;
  public void SetCards(List<ICard> cards) => _cards = cards;
  public ICard GetCardAt(int index) => _cards[index];
  public void SetCardAt(int index, ICard card) => _cards[index] = card;
}

public class DiscardPile : IDiscardPile
{
  private List<ICard> _cards;

  public DiscardPile()
  {
    _cards = new List<ICard>();
  }

  public List<ICard> GetCards() => _cards;
  public void SetCards(List<ICard> cards) => _cards = cards;
  public ICard GetCardAt(int index) => _cards[index];
  public void SetCardAt(int index, ICard card) => _cards[index] = card;
}

public class GameController
{
  private List<IPlayer> _players;
  private Dictionary<IPlayer, List<ICard>> _playerHands;
  private IDeck _deck;
  private IDiscardPile _discardPile;
  private int _currentPlayerIndex;
  private bool _isClockwise;
  private Color? _currentWildColor;
  private Random _random;

  // Events
  public Action<IPlayer>? OnPlayerTurnChanged;
  public Action<IPlayer, ICard>? OnCardPlayed;
  public Action<IPlayer>? OnUnoViolation;
  public Action<IPlayer>? OnGameEnded;

  // Delegates
  public Func<IPlayer, ICard, List<ICard>, ICard>? CardChooser;
  public Func<Color>? WildColorChooser;
  public Func<IPlayer, bool>? UnoCallChecker;

  public GameController()
  {
    _players = new List<IPlayer>();
    _playerHands = new Dictionary<IPlayer, List<ICard>>();
    _deck = new Deck();
    _discardPile = new DiscardPile();
    _currentPlayerIndex = 0;
    _isClockwise = true;
    _random = new Random();
  }

  // Game Flow Methods
  public void StartGame()
  {
    if (_players.Count < 2)
    {
      Console.WriteLine("Need at least 2 players to start the game!");
      return;
    }

    InitializeDeck();
    ShuffleDeck();
    DealCardsToPlayers();

    // Place first card on discard pile
    var firstCard = _deck.GetCards().First();
    _deck.GetCards().RemoveAt(0);
    _discardPile.GetCards().Add(firstCard);

    Console.WriteLine($"Game started! First card: {firstCard.GetDisplayText()}");

    // Handle special first card
    if (firstCard.GetCardType() == CardType.Wild)
    {
      _currentWildColor = ChooseWildColor();
      Console.WriteLine($"Wild color chosen: {_currentWildColor}");
    }

    GameLoop();
  }

  private void InitializeDeck()
  {
    var cards = new List<ICard>();

    // Number cards (0-9 for each color)
    foreach (Color color in Enum.GetValues(typeof(Color)))
    {
      // One 0 card per color
      cards.Add(new Card(CardType.Number, color, Number.Zero));

      // Two of each number 1-9 per color
      for (int i = 1; i <= 9; i++)
      {
        var number = (Number)i;
        cards.Add(new Card(CardType.Number, color, number));
        cards.Add(new Card(CardType.Number, color, number));
      }

      // Action cards (2 of each per color)
      foreach (ActionType action in Enum.GetValues(typeof(ActionType)))
      {
        cards.Add(new Card(CardType.Action, color, actionType: action));
        cards.Add(new Card(CardType.Action, color, actionType: action));
      }
    }

    // Wild cards (4 of each type)
    for (int i = 0; i < 4; i++)
    {
      cards.Add(new Card(CardType.Wild, wildType: WildType.Wild));
      cards.Add(new Card(CardType.Wild, wildType: WildType.WildDrawFour));
    }

    _deck.SetCards(cards);
  }

  public void AddPlayer(IPlayer player)
  {
    _players.Add(player);
    _playerHands[player] = new List<ICard>();
  }

  public void GameLoop()
  {
    while (!IsGameOver())
    {
      var currentPlayer = GetCurrentPlayer();
      OnPlayerTurnChanged?.Invoke(currentPlayer);

      // Display turn header with colors
      Console.WriteLine("\n" + new string('=', 50));
      SetConsoleColor(ConsoleColor.Green);
      Console.WriteLine($"🎯 {currentPlayer.GetName()}'s Turn");
      ResetConsoleColor();
      Console.WriteLine(new string('=', 50));

      var topCard = GetTopDiscardCard();
      Console.Write("🎴 Top card: ");
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

      if (_currentWildColor.HasValue)
      {
        SetConsoleColor(GetColorFromEnum(_currentWildColor.Value));
        Console.WriteLine($"🌈 Current wild color: {_currentWildColor}");
        ResetConsoleColor();
      }

      DisplayPlayerHand(currentPlayer);

      var playableCards = GetPlayableCardsFromPlayer(currentPlayer, GetTopDiscardCard()!);

      if (playableCards.Count == 0)
      {
        SetConsoleColor(ConsoleColor.Red);
        Console.WriteLine("❌ No playable cards. Drawing a card...");
        ResetConsoleColor();
        var drawnCard = DrawCardFromDeck(currentPlayer);
        Console.Write("📤 Drew: ");
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

        playableCards = GetPlayableCardsFromPlayer(currentPlayer, GetTopDiscardCard()!);
        if (playableCards.Count == 0)
        {
          SetConsoleColor(ConsoleColor.Yellow);
          Console.WriteLine("⏭️ Still no playable cards. Turn skipped.");
          ResetConsoleColor();
          NextPlayer();
          continue;
        }
      }

      var chosenCard = ChooseCard(currentPlayer, GetTopDiscardCard()!, playableCards);

      if (PlayCard(currentPlayer, chosenCard))
      {
        OnCardPlayed?.Invoke(currentPlayer, chosenCard);

        // Check for UNO call
        if (GetPlayerHandSize(currentPlayer) == 1)
        {
          var calledUno = UnoCallChecker?.Invoke(currentPlayer) ?? false;
          if (!calledUno)
          {
            SetConsoleColor(ConsoleColor.Red);
            Console.WriteLine($"⚠️ {currentPlayer.GetName()} forgot to call UNO! Drawing 2 penalty cards.");
            ResetConsoleColor();
            OnUnoViolation?.Invoke(currentPlayer);
            PenalizePlayer(currentPlayer);
          }
          else
          {
            SetConsoleColor(ConsoleColor.Yellow);
            Console.WriteLine($"🎉 {currentPlayer.GetName()} called UNO!");
            ResetConsoleColor();
          }
        }

        ExecuteCardEffect(chosenCard);

        // Only call NextPlayer if ExecuteCardEffect didn't handle the turn progression
        if (chosenCard.GetCardType() != CardType.Action ||
            (chosenCard.GetActionType() != ActionType.Skip &&
             chosenCard.GetActionType() != ActionType.DrawTwo &&
             !(chosenCard.GetActionType() == ActionType.Reverse && _players.Count == 2)))
        {
          if (chosenCard.GetCardType() != CardType.Wild ||
              chosenCard.GetWildType() != WildType.WildDrawFour)
          {
            NextPlayer();
          }
        }
      }
    }
    if (IsGameOver())
    {
      var winner = GetWinner();
      Console.WriteLine("\n" + string.Concat(Enumerable.Repeat("🎉", 30)));
      SetConsoleColor(ConsoleColor.Green);
      Console.WriteLine($"🏆 {winner?.GetName()} WINS THE GAME! 🏆");
      ResetConsoleColor();
      Console.WriteLine(string.Concat(Enumerable.Repeat("🎉", 30)));
      OnGameEnded?.Invoke(winner!);

      // Display final hand sizes
      Console.WriteLine($"\n📊 Final hand sizes:");
      foreach (var player in GetAllPlayers())
      {
        var handSize = GetPlayerHandSize(player);
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
      return;
    }

  }

  public void NextPlayer()
  {
    if (_isClockwise)
    {
      _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;
    }
    else
    {
      _currentPlayerIndex = (_currentPlayerIndex - 1 + _players.Count) % _players.Count;
    }
  }

  public bool IsGameOver()
  {
    return _players.Any(p => GetPlayerHandSize(p) == 0);
  }

  public IPlayer? GetWinner()
  {
    return _players.FirstOrDefault(p => GetPlayerHandSize(p) == 0);
  }

  // Player Hand Management Methods
  public void AddCardToPlayer(IPlayer player, ICard card)
  {
    if (_playerHands.ContainsKey(player))
    {
      _playerHands[player].Add(card);
    }
  }

  public bool RemoveCardFromPlayer(IPlayer player, ICard card)
  {
    if (_playerHands.ContainsKey(player))
    {
      return _playerHands[player].Remove(card);
    }
    return false;
  }

  public int GetPlayerHandSize(IPlayer player)
  {
    return _playerHands.ContainsKey(player) ? _playerHands[player].Count : 0;
  }

  public bool PlayerHasCard(IPlayer player, ICard card)
  {
    return _playerHands.ContainsKey(player) && _playerHands[player].Contains(card);
  }

  public List<ICard> GetPlayerHand(IPlayer player)
  {
    return _playerHands.ContainsKey(player) ? _playerHands[player] : new List<ICard>();
  }

  public void ClearPlayerHand(IPlayer player)
  {
    if (_playerHands.ContainsKey(player))
    {
      _playerHands[player].Clear();
    }
  }

  // Card Management Methods
  public bool PlayCard(IPlayer player, ICard card)
  {
    if (!PlayerHasCard(player, card) || !CanPlayCard(card, GetTopDiscardCard()!))
    {
      return false;
    }

    // Reset wild color when a non-wild card is played
    if (card.GetCardType() != CardType.Wild)
    {
      _currentWildColor = null;
    }

    RemoveCardFromPlayer(player, card);
    AddCardToDiscardPile(card);
    return true;
  }

  public ICard DrawCardFromDeck(IPlayer player)
  {
    if (IsDeckEmpty())
    {
      RecycleDiscardPile();
    }

    var card = _deck.GetCardAt(0);
    _deck.GetCards().RemoveAt(0);
    AddCardToPlayer(player, card);
    return card;
  }

  public void AddCardToDeck(ICard card)
  {
    _deck.GetCards().Add(card);
  }

  public int GetDeckCardCount()
  {
    return _deck.GetCards().Count;
  }

  public bool IsDeckEmpty()
  {
    return _deck.GetCards().Count == 0;
  }

  public void ShuffleDeck()
  {
    var cards = _deck.GetCards();
    for (int i = cards.Count - 1; i > 0; i--)
    {
      int j = _random.Next(i + 1);
      (cards[i], cards[j]) = (cards[j], cards[i]);
    }
  }

  public void AddCardToDiscardPile(ICard card)
  {
    _discardPile.GetCards().Add(card);
  }

  public ICard? GetTopDiscardCard()
  {
    var cards = _discardPile.GetCards();
    return cards.Count > 0 ? cards.Last() : null;
  }

  public int GetDiscardPileCardCount()
  {
    return _discardPile.GetCards().Count;
  }

  public bool IsDiscardPileEmpty()
  {
    return _discardPile.GetCards().Count == 0;
  }

  public List<ICard> GetPlayableCardsFromPlayer(IPlayer player, ICard topCard)
  {
    var hand = GetPlayerHand(player);
    return hand.Where(card => CanPlayCard(card, topCard)).ToList();
  }

  public ICard ChooseCard(IPlayer player, ICard topCard, List<ICard> playableCards)
  {
    if (CardChooser != null)
    {
      return CardChooser(player, topCard, playableCards);
    }

    // Default implementation - let player choose
    SetConsoleColor(ConsoleColor.Yellow);
    Console.WriteLine("🎯 Choose a card to play:");
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

    int choice;
    while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > playableCards.Count)
    {
      Console.WriteLine("Invalid choice. Please try again:");
    }

    return playableCards[choice - 1];
  }

  public void ExecuteCardEffect(ICard card)
  {
    switch (card.GetCardType())
    {
      case CardType.Action:
        switch (card.GetActionType())
        {
          case ActionType.Skip:
            NextPlayer(); // Move to next player (who will be skipped)
            var skippedPlayer = GetCurrentPlayer();
            SetConsoleColor(ConsoleColor.Red);
            Console.WriteLine($"🚫 {skippedPlayer.GetName()} is skipped!");
            ResetConsoleColor();
            NextPlayer(); // Move to the player after the skipped one
            return; // Don't call NextPlayer() again in GameLoop
          case ActionType.Reverse:
            SetConsoleColor(ConsoleColor.Magenta);
            Console.WriteLine("🔄 Direction reversed!");
            ResetConsoleColor();
            ReverseDirection();
            // For 2 players, reverse acts like skip
            if (_players.Count == 2)
            {
              NextPlayer();
              return; // Don't call NextPlayer() again in GameLoop
            }
            break;
          case ActionType.DrawTwo:
            NextPlayer();
            var targetPlayer = GetCurrentPlayer();
            SetConsoleColor(ConsoleColor.Yellow);
            Console.WriteLine($"📥 {targetPlayer.GetName()} draws 2 cards and is skipped!");
            ResetConsoleColor();
            DrawCardFromDeck(targetPlayer);
            DrawCardFromDeck(targetPlayer);
            NextPlayer(); // Skip the player who drew cards
            return; // Don't call NextPlayer() again in GameLoop
        }
        break;
      case CardType.Wild:
        _currentWildColor = ChooseWildColor();
        SetConsoleColor(GetColorFromEnum(_currentWildColor.Value));
        Console.WriteLine($"🎨 Wild color chosen: {_currentWildColor}");
        ResetConsoleColor();

        if (card.GetWildType() == WildType.WildDrawFour)
        {
          NextPlayer();
          var targetPlayer = GetCurrentPlayer();
          SetConsoleColor(ConsoleColor.Red);
          Console.WriteLine($"💥 {targetPlayer.GetName()} draws 4 cards and is skipped!");
          ResetConsoleColor();
          for (int i = 0; i < 4; i++)
          {
            DrawCardFromDeck(targetPlayer);
          }
          NextPlayer(); // Skip the player who drew cards
          return; // Don't call NextPlayer() again in GameLoop
        }
        break;
    }
  }

  // Rule Enforcement Methods
  public bool CallUno(IPlayer player)
  {
    return GetPlayerHandSize(player) == 1;
  }

  public bool CheckUnoViolation(IPlayer player)
  {
    return GetPlayerHandSize(player) == 1;
  }

  public void PenalizePlayer(IPlayer player)
  {
    DrawCardFromDeck(player);
    DrawCardFromDeck(player);
  }

  public bool ValidateCard(ICard card)
  {
    return card != null;
  }

  public bool CanPlayCard(ICard card, ICard topCard)
  {
    if (topCard == null) return true;

    // Wild cards can always be played
    if (card.GetCardType() == CardType.Wild) return true;

    // Check if there's a current wild color
    if (_currentWildColor.HasValue)
    {
      return card.GetColor() == _currentWildColor.Value;
    }

    // Same color
    if (card.GetColor() == topCard.GetColor()) return true;

    // Same number (Fixed: Added null checks)
    if (card.GetCardType() == CardType.Number && topCard.GetCardType() == CardType.Number)
    {
      return card.GetNumber().HasValue && topCard.GetNumber().HasValue &&
             card.GetNumber()!.Value == topCard.GetNumber()!.Value;
    }

    // Same action (Fixed: Added null checks)
    if (card.GetCardType() == CardType.Action && topCard.GetCardType() == CardType.Action)
    {
      return card.GetActionType().HasValue && topCard.GetActionType().HasValue &&
             card.GetActionType()!.Value == topCard.GetActionType()!.Value;
    }

    return false;
  }

  // Setup/Utility Methods
  public void DealCardsToPlayers()
  {
    const int cardsPerPlayer = 7;
    for (int i = 0; i < cardsPerPlayer; i++)
    {
      foreach (var player in _players)
      {
        var card = _deck.GetCardAt(0);
        _deck.GetCards().RemoveAt(0);
        AddCardToPlayer(player, card);
      }
    }
  }

  public void ReverseDirection()
  {
    _isClockwise = !_isClockwise;
  }

  public Color ChooseWildColor()
  {
    if (WildColorChooser != null)
    {
      return WildColorChooser();
    }

    // Default implementation
    SetConsoleColor(ConsoleColor.Cyan);
    Console.WriteLine("🎨 Choose a color:");
    ResetConsoleColor();
    var colors = (Color[])Enum.GetValues(typeof(Color));
    for (int i = 0; i < colors.Length; i++)
    {
      SetConsoleColor(GetColorFromEnum(colors[i]));
      Console.WriteLine($"{i + 1}. {GetColorEmoji(colors[i])} {colors[i]}");
      ResetConsoleColor();
    }

    int choice;
    while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > colors.Length)
    {
      Console.WriteLine("Invalid choice. Please try again:");
    }

    _currentWildColor = colors[choice - 1];
    return _currentWildColor.Value;
  }

  public void RecycleDiscardPile()
  {
    var topCard = GetTopDiscardCard();
    var cards = _discardPile.GetCards().Take(_discardPile.GetCards().Count - 1).ToList();

    // Reset wild colors on recycled cards
    foreach (var card in cards)
    {
      if (card.GetCardType() == CardType.Wild)
      {
        card.SetColor(null);
      }
    }

    _deck.SetCards(cards);
    _discardPile.SetCards(new List<ICard> { topCard! });
    ShuffleDeck();

    Console.WriteLine("Deck was empty. Recycled discard pile.");
  }

  // Query Methods
  public IPlayer GetCurrentPlayer()
  {
    return _players[_currentPlayerIndex];
  }

  public List<Color> GetValidColors()
  {
    return ((Color[])Enum.GetValues(typeof(Color))).ToList();
  }

  public IPlayer? GetPlayerByName(string name)
  {
    return _players.FirstOrDefault(p => p.GetName().Equals(name, StringComparison.OrdinalIgnoreCase));
  }

  public List<int> GetPlayerHandSizes()
  {
    return _players.Select(GetPlayerHandSize).ToList();
  }

  public List<IPlayer> GetAllPlayers()
  {
    return _players.ToList();
  }

  // Helper methods for console colors
  private void SetConsoleColor(ConsoleColor color)
  {
    Console.ForegroundColor = color;
  }

  private void ResetConsoleColor()
  {
    Console.ResetColor();
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

  // Helper method for displaying player hand with colors
  private void DisplayPlayerHand(IPlayer player)
  {
    var hand = GetPlayerHand(player);
    SetConsoleColor(ConsoleColor.Cyan);
    Console.WriteLine($"\n🎴 {player.GetName()}'s hand ({hand.Count} cards):");
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
        SetConsoleColor(ConsoleColor.Magenta); // For wild cards
      }

      Console.WriteLine(card.GetDisplayText());
      ResetConsoleColor();
    }
  }
}

// Main Program
public class Program
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

    // Setup delegates for interactive gameplay
    gameController.UnoCallChecker = (player) =>
    {
      Console.WriteLine($"{player.GetName()}, do you want to call UNO? (y/n):");
      var input = Console.ReadLine()?.ToLower();
      return input == "y" || input == "yes";
    };

    // Get number of players
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

