namespace Uno;

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

  public Action<IPlayer>? OnPlayerTurnChanged;
  public Action<IPlayer, ICard>? OnCardPlayed;
  public Action<IPlayer>? OnUnoViolation;
  public Action<IPlayer>? OnGameEnded;

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

  public void StartGame()
  {
    if (_players.Count < 2)
    {
      Console.WriteLine("Perlu minimal 2 permain untuk bermain!");
      return;
    }

    InitializeDeck();
    ShuffleDeck();
    DealCardsToPlayers();

    var firstCard = _deck.GetCards().First();
    _deck.GetCards().RemoveAt(0);
    _discardPile.GetCards().Add(firstCard);

    Console.WriteLine($"Game dimulai! Kartu pertama: {firstCard.GetDisplayText()}");

    if (firstCard.GetCardType() == CardType.Wild)
    {
      _currentWildColor = ChooseWildColor();
      Console.WriteLine($"Kartu Wild dipilih: {_currentWildColor}");
    }

    GameLoop();
  }

  private void InitializeDeck()
  {
    var cards = new List<ICard>();

    foreach (Color color in Enum.GetValues(typeof(Color)))
    {
      cards.Add(new Card(CardType.Number, color, Number.Zero));

      for (int i = 1; i <= 9; i++)
      {
        var number = (Number)i;
        cards.Add(new Card(CardType.Number, color, number));
        cards.Add(new Card(CardType.Number, color, number));
      }

      foreach (ActionType action in Enum.GetValues(typeof(ActionType)))
      {
        cards.Add(new Card(CardType.Action, color, actionType: action));
        cards.Add(new Card(CardType.Action, color, actionType: action));
      }
    }

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
      Console.Clear();

      Console.WriteLine("\n" + new string('=', 50));
      SetConsoleColor(ConsoleColor.Green);
      Console.WriteLine($"🎯 Giliran {currentPlayer.GetName()}");
      ResetConsoleColor();
      Console.WriteLine(new string('=', 50));

      var topCard = GetTopDiscardCard();
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

      if (_currentWildColor.HasValue)
      {
        SetConsoleColor(GetColorFromEnum(_currentWildColor.Value));
        Console.WriteLine($"🌈 Warna wild saat ini: {_currentWildColor}");
        ResetConsoleColor();
      }

      DisplayPlayerHand(currentPlayer);

      var playableCards = GetPlayableCardsFromPlayer(currentPlayer, GetTopDiscardCard()!);

      if (playableCards.Count == 0)
      {
        SetConsoleColor(ConsoleColor.Red);
        Console.WriteLine("❌ Tidak ada kartu,. Mengambil kartu...");
        ResetConsoleColor();
        var drawnCard = DrawCardFromDeck(currentPlayer);
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

        playableCards = GetPlayableCardsFromPlayer(currentPlayer, GetTopDiscardCard()!);
        if (playableCards.Count == 0)
        {
          SetConsoleColor(ConsoleColor.Yellow);
          Console.WriteLine("⏭️  Masih tidak ada kartu, Giliran dilewati.");
          ResetConsoleColor();
          NextPlayer();
          continue;
        }
      }

      var chosenCard = ChooseCard(currentPlayer, GetTopDiscardCard()!, playableCards);

      if (PlayCard(currentPlayer, chosenCard))
      {
        OnCardPlayed?.Invoke(currentPlayer, chosenCard);

        if (GetPlayerHandSize(currentPlayer) == 1)
        {
          var calledUno = UnoCallChecker?.Invoke(currentPlayer) ?? false;
          if (!calledUno)
          {
            SetConsoleColor(ConsoleColor.Red);
            Console.WriteLine($"⚠️ {currentPlayer.GetName()} Lupa memanggil UNO! hukuman mengambil 2 kartu.");
            ResetConsoleColor();
            OnUnoViolation?.Invoke(currentPlayer);
            PenalizePlayer(currentPlayer);
          }
          else
          {
            SetConsoleColor(ConsoleColor.Yellow);
            Console.WriteLine($"🎉 {currentPlayer.GetName()} memanggil UNO!");
            ResetConsoleColor();
          }
        }

        ExecuteCardEffect(chosenCard);

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
      Console.WriteLine("\n" + string.Concat(Enumerable.Repeat("🎉", 20)));
      SetConsoleColor(ConsoleColor.Green);
      Console.WriteLine($"🏆 {winner?.GetName()} MEMENANGKAN PERMAINAN! 🏆");
      ResetConsoleColor();
      Console.WriteLine(string.Concat(Enumerable.Repeat("🎉", 20)));
      OnGameEnded?.Invoke(winner!);

      Console.WriteLine($"\n📊 Kartu di tangan diakhir:");
      foreach (var player in GetAllPlayers())
      {
        var handSize = GetPlayerHandSize(player);
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

    SetConsoleColor(ConsoleColor.Yellow);
    Console.WriteLine("🎯 Memilih kartu untuk bermain:");
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
      Console.WriteLine("Invalid pilihan. Silahkan coba lagi:");
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
            NextPlayer();
            var skippedPlayer = GetCurrentPlayer();
            SetConsoleColor(ConsoleColor.Red);
            Console.WriteLine($"🚫 {skippedPlayer.GetName()} di skip!");
            ResetConsoleColor();
            NextPlayer();
            return;
          case ActionType.Reverse:
            SetConsoleColor(ConsoleColor.Magenta);
            Console.WriteLine("🔄 Direction dibalik!");
            ResetConsoleColor();
            ReverseDirection();
            if (_players.Count == 2)
            {
              Console.WriteLine("Kartu reverse bersifat skip, silahkan bermain lagi!");
              return;
            }
            break;
          case ActionType.DrawTwo:
            NextPlayer();
            var targetPlayer = GetCurrentPlayer();
            SetConsoleColor(ConsoleColor.Yellow);
            Console.WriteLine($"📥 {targetPlayer.GetName()} mengambil 2 kartu dan di skip!");
            ResetConsoleColor();
            DrawCardFromDeck(targetPlayer);
            DrawCardFromDeck(targetPlayer);
            NextPlayer(); // lewati pemain yang mengambil kartu
            return;
        }
        break;
      case CardType.Wild:
        _currentWildColor = ChooseWildColor();
        SetConsoleColor(GetColorFromEnum(_currentWildColor.Value));
        Console.WriteLine($"🎨 Kartu wild dipilih: {_currentWildColor}");
        ResetConsoleColor();

        if (card.GetWildType() == WildType.WildDrawFour)
        {
          NextPlayer();
          var targetPlayer = GetCurrentPlayer();
          SetConsoleColor(ConsoleColor.Red);
          Console.WriteLine($"💥 {targetPlayer.GetName()} mengambil 4 kartu dan di skip!");
          ResetConsoleColor();
          for (int i = 0; i < 4; i++)
          {
            DrawCardFromDeck(targetPlayer);
          }
          NextPlayer();
          return;
        }
        break;
    }
  }

  // rules
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

    if (card.GetCardType() == CardType.Wild) return true;

    if (_currentWildColor.HasValue)
    {
      return card.GetColor() == _currentWildColor.Value;
    }

    if (card.GetColor() == topCard.GetColor()) return true;

    if (card.GetCardType() == CardType.Number && topCard.GetCardType() == CardType.Number)
    {
      return card.GetNumber().HasValue && topCard.GetNumber().HasValue &&
             card.GetNumber()!.Value == topCard.GetNumber()!.Value;
    }

    if (card.GetCardType() == CardType.Action && topCard.GetCardType() == CardType.Action)
    {
      return card.GetActionType().HasValue && topCard.GetActionType().HasValue &&
             card.GetActionType()!.Value == topCard.GetActionType()!.Value;
    }

    return false;
  }

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
    Console.WriteLine("🎨 Memilih warna:");
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
      Console.WriteLine("Invalid pilihan. Silahkan coba lagi:");
    }

    _currentWildColor = colors[choice - 1];
    return _currentWildColor.Value;
  }

  public void RecycleDiscardPile()
  {
    var topCard = GetTopDiscardCard();
    var cards = _discardPile.GetCards().Take(_discardPile.GetCards().Count - 1).ToList();

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

    Console.WriteLine("Deck kosong. Mengocok ulang discard pile.");
  }

  // query
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

  // helper method
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
        SetConsoleColor(ConsoleColor.Magenta); // untuk kartu wild
      }

      Console.WriteLine(card.GetDisplayText());
      ResetConsoleColor();
    }
  }
}
