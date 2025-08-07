using UnoRevisi.Enums;
using UnoRevisi.Interfaces;
using UnoRevisi.Models;
using UnoRevisi.View;

namespace UnoRevisi.Controller;

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
  // private Display _display;

  public Action<IPlayer>? OnPlayerTurnChanged;
  public Action<IPlayer, ICard>? OnCardPlayed;
  public Action<IPlayer>? OnUnoViolation;
  public Action<IPlayer>? OnGameEnded;

  public GameController(List<IPlayer> players, DiscardPile discardPile, Deck deck)
  {
    _players = players;
    _playerHands = new Dictionary<IPlayer, List<ICard>>();
    _deck = deck;
    _discardPile = discardPile;
    _currentPlayerIndex = 0;
    _isClockwise = true;
    _random = new Random();
  }

  public bool StartGame()
  {
    if (_players.Count < 2)
    {
      return false;
    }

    InitializeDeck();
    ShuffleDeck();
    InitializePlayerHands(_players);
    DealCardsToPlayers();

    var firstCard = _deck.GetCards().First();
    _deck.GetCards().RemoveAt(0);
    _discardPile.GetCards().Add(firstCard);

    if (firstCard.GetCardType() == CardType.Wild)
    {
      _currentWildColor = ChooseWildColor();
    }

    GameLoop();
    return true;
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

  public void AddPlayerHand(IPlayer player)
  {
    _playerHands[player] = new List<ICard>();
  }

  public void InitializePlayerHands(List<IPlayer> players)
  {
    foreach (var player in players)
    {
      AddPlayerHand(player);
    }
  }

  public void GameLoop()
  {
    while (!IsGameOver())
    {
      var currentPlayer = GetCurrentPlayer();
      OnPlayerTurnChanged?.Invoke(currentPlayer);
      Console.Clear();

      var topCard = GetTopDiscardCard();
      var playerHand = GetPlayerHand(currentPlayer);
      var playableCards = GetPlayableCardsFromPlayer(currentPlayer, topCard!);

      // display current game state
      _display.ShowGameState(currentPlayer, topCard!, _currentWildColor, playerHand);

      if (playableCards.Count == 0)
      {
        var drawnCard = DrawCardFromDeck(currentPlayer);
        _display.ShowCardDrawn(drawnCard);

        playableCards = GetPlayableCardsFromPlayer(currentPlayer, GetTopDiscardCard()!);
        if (playableCards.Count == 0)
        {
          _display.ShowTurnSkipped();
          NextPlayer();
          continue;
        }
      }

      var chosenCard = ChooseCard(playableCards);

      if (PlayCard(currentPlayer, chosenCard))
      {
        OnCardPlayed?.Invoke(currentPlayer, chosenCard);

        if (GetPlayerHandSize(currentPlayer) == 1)
        {
          var calledUno = _display.CheckUnoCall(currentPlayer);
          if (!calledUno)
          {
            _display.ShowUnoViolation(currentPlayer);
            OnUnoViolation?.Invoke(currentPlayer);
            PenalizePlayer(currentPlayer);
          }
          else
          {
            _display.ShowUnoCall(currentPlayer);
          }
        }

        var cardEffect = ExecuteCardEffect(chosenCard);
        if (cardEffect.HasValue)
        {
          _display.PrintEffect(cardEffect.Value);
        }

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
      var finalStats = GetFinalGameStats();
      _display.ShowGameEnd(winner!, finalStats);
      OnGameEnded?.Invoke(winner!);
    }
  }

  public ICard GetFirstCard()
  {
    var cards = _discardPile.GetCards();
    var result = cards.Count > 0 ? cards.First() : null!;
    return result;
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

  public void SetCurrentWildColor(Color? color)
  {
    _currentWildColor = color;
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

  public List<ICard> GetPlayableCardsFromPlayer(IPlayer player, ICard topCard)
  {
    var hand = GetPlayerHand(player);
    return hand.Where(card => CanPlayCard(card, topCard)).ToList();
  }

  public ICard ChooseCard(List<ICard> playableCards)
  {
    _display.ShowPlayableCards(playableCards); // Just shows the cards

    int choice;
    while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > playableCards.Count)
    {
      _display.ShowInvalidChoiceMessage(); // Optional method in Display
    }

    return playableCards[choice - 1];
  }


  public (string message, ConsoleColor color)? ExecuteCardEffect(ICard card)
  {
    switch (card.GetCardType())
    {
      case CardType.Action:
        switch (card.GetActionType())
        {
          case ActionType.Skip:
            NextPlayer();
            var skipped = GetCurrentPlayer();
            NextPlayer();
            return ($"🚫 {skipped.GetName()} di skip!", ConsoleColor.Red);

          case ActionType.Reverse:
            ReverseDirection();
            if (_players.Count == 2)
            {
              return ("🔄 Direction dibalik! Kartu reverse bersifat skip, silahkan bermain lagi!", ConsoleColor.Magenta);
            }
            return ("🔄 Direction dibalik!", ConsoleColor.Magenta);

          case ActionType.DrawTwo:
            NextPlayer();
            var target = GetCurrentPlayer();
            DrawCardFromDeck(target);
            DrawCardFromDeck(target);
            NextPlayer();
            return ($"📥 {target.GetName()} mengambil 2 kartu dan di skip!", ConsoleColor.Yellow);
        }
        break;

      case CardType.Wild:
        _currentWildColor = ChooseWildColor();

        if (card.GetWildType() == WildType.WildDrawFour)
        {
          NextPlayer();
          var wildTarget = GetCurrentPlayer();
          for (int i = 0; i < 4; i++)
          {
            DrawCardFromDeck(wildTarget);
          }
          NextPlayer();
          return ($"💥 Kartu wild dipilih: {_currentWildColor}. {wildTarget.GetName()} mengambil 4 kartu dan di skip!", ConsoleColor.Red);
        }

        return ($"🎨 Kartu wild dipilih: {_currentWildColor}", GetColorFromEnum(_currentWildColor));
    }

    return null;
  }

  public ConsoleColor GetColorFromEnum(Color? color)
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


  // Rules
  public void PenalizePlayer(IPlayer player)
  {
    DrawCardFromDeck(player);
    DrawCardFromDeck(player);
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
    _display.ShowWildColorChoices();

    var colors = (Color[])Enum.GetValues(typeof(Color));
    int choice;

    while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > colors.Length)
    {
      _display.ShowInvalidColorChoice();
    }

    return colors[choice - 1];
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

    _display.ShowDeckRecycled();
  }

  // Query methods
  public IPlayer GetCurrentPlayer()
  {
    return _players[_currentPlayerIndex];
  }

  public List<IPlayer> GetAllPlayers()
  {
    return _players.ToList();
  }

  public Color? GetCurrentWildColor()
  {
    return _currentWildColor;
  }

  public List<(IPlayer Player, int HandSize)> GetFinalGameStats()
  {
    return _players.Select(p => (p, GetPlayerHandSize(p))).ToList();
  }
}
