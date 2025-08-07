using UnoRevisi.Enums;
using UnoRevisi.Interfaces;
using UnoRevisi.Models;

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
  private bool _gameStarted;

  // Events for Program.cs to listen
  public Action<IPlayer>? OnPlayerTurnChanged;
  public Action<IPlayer, ICard>? OnCardPlayed;
  public Action<IPlayer>? OnUnoViolation;
  public Action<IPlayer>? OnGameEnded;

  public GameController(List<IPlayer> players)
  {
    _players = players;
    _playerHands = new Dictionary<IPlayer, List<ICard>>();
    _deck = new Deck();
    _discardPile = new DiscardPile();
    _currentPlayerIndex = 0;
    _isClockwise = true;
    _random = new Random();
    _gameStarted = false;
  }

  #region Game Initialization

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

    _gameStarted = true;
    return true;
  }

  public bool IsGameStarted()
  {
    return _gameStarted;
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

  public void ShuffleDeck()
  {
    var cards = _deck.GetCards();
    for (int i = cards.Count - 1; i > 0; i--)
    {
      int j = _random.Next(i + 1);
      (cards[i], cards[j]) = (cards[j], cards[i]);
    }
  }

  public void InitializePlayerHands(List<IPlayer> players)
  {
    _playerHands.Clear();
    foreach (var player in players)
    {
      _playerHands[player] = new List<ICard>();
    }
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

  public ICard GetFirstCard()
  {
    var cards = _discardPile.GetCards();
    var result = cards.Count > 0 ? cards.First() : null!;
    return result;
  }

  #endregion

  #region Player Management

  public IPlayer GetCurrentPlayer()
  {
    return _players[_currentPlayerIndex];
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

    OnPlayerTurnChanged?.Invoke(GetCurrentPlayer());
  }

  public void ReverseDirection()
  {
    _isClockwise = !_isClockwise;
  }

  public List<IPlayer> GetAllPlayers()
  {
    return _players.ToList();
  }

  #endregion

  #region Card Management

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
      bool result = _playerHands[player].Remove(card);
      return result;
    }
    return false;
  }

  public List<ICard> GetPlayerHand(IPlayer player)
  {
    var result = _playerHands.ContainsKey(player) ? _playerHands[player] : new List<ICard>();
    return result;
  }

  public int GetPlayerHandSize(IPlayer player)
  {
    int result = _playerHands.ContainsKey(player) ? _playerHands[player].Count : 0;
    return result;
  }

  public bool PlayerHasCard(IPlayer player, ICard card)
  {
    bool result = _playerHands.ContainsKey(player) && _playerHands[player].Contains(card);
    return result;
  }

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

    OnCardPlayed?.Invoke(player, card);
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

  public List<ICard> GetPlayableCardsFromPlayer(IPlayer player, ICard topCard)
  {
    var hand = GetPlayerHand(player);
    var result = hand.Where(card => CanPlayCard(card, topCard)).ToList();
    return result;
  }

  public void ChooseWildColor(Color? color)
  {
    _currentWildColor = color;
  }

  #endregion

  #region Deck & Discard Management

  public bool IsDeckEmpty()
  {
    bool result = _deck.GetCards().Count == 0;
    return result;
  }

  public void AddCardToDiscardPile(ICard card)
  {
    _discardPile.GetCards().Add(card);
  }

  public ICard? GetTopDiscardCard()
  {
    var cards = _discardPile.GetCards();
    var result = cards.Count > 0 ? cards.Last() : null;
    return result;
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
  }

  #endregion

  #region Game Rules & Effects

  public bool CanPlayCard(ICard card, ICard topCard)
  {
    if (topCard == null) return true;

    if (card.GetCardType() == CardType.Wild) return true;

    if (_currentWildColor.HasValue)
    {
      bool result = card.GetColor() == _currentWildColor.Value;
      return result;
    }

    if (card.GetColor() == topCard.GetColor()) return true;

    if (card.GetCardType() == CardType.Number && topCard.GetCardType() == CardType.Number)
    {
      bool result = card.GetNumber().HasValue && topCard.GetNumber().HasValue && card.GetNumber()!.Value == topCard.GetNumber()!.Value;
      return result;
    }

    if (card.GetCardType() == CardType.Action && topCard.GetCardType() == CardType.Action)
    {
      bool result = card.GetActionType().HasValue && topCard.GetActionType().HasValue && card.GetActionType()!.Value == topCard.GetActionType()!.Value;
      return result;
    }

    return false;
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
            // NextPlayer();
            var result = ($"🚫 {skipped.GetName()} di skip!", ConsoleColor.Red);
            return result;

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
            // NextPlayer();
            return ($"📥 {target.GetName()} mengambil 2 kartu dan di skip!", ConsoleColor.Yellow);
        }
        break;

      case CardType.Wild:
        if (card.GetWildType() == WildType.WildDrawFour)
        {
          NextPlayer();
          var wildTarget = GetCurrentPlayer();
          for (int i = 0; i < 4; i++)
          {
            DrawCardFromDeck(wildTarget);
          }
          // NextPlayer();
          return ($"💥 Kartu wild: {_currentWildColor}. {wildTarget.GetName()} mengambil 4 kartu dan di skip!", ConsoleColor.Red);
        }
        return ($"🎨 Kartu wild dipilih: {_currentWildColor}", GetColorFromEnum(_currentWildColor));
    }

    return null;
  }

  public void PenalizePlayer(IPlayer player)
  {
    DrawCardFromDeck(player);
    DrawCardFromDeck(player);
    OnUnoViolation?.Invoke(player);
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

  #endregion

  #region Game State

  public bool IsGameOver()
  {
    bool result = _players.Any(p => GetPlayerHandSize(p) == 0);
    return result;
  }

  public IPlayer? GetWinner()
  {
    var result = _players.FirstOrDefault(p => GetPlayerHandSize(p) == 0);
    return result;
  }

  public Color? GetCurrentWildColor()
  {
    return _currentWildColor;
  }

  public List<(IPlayer Player, int HandSize)> GetFinalGameStats()
  {
    var result = _players.Select(p => (p, GetPlayerHandSize(p))).ToList();
    return result;
  }

  #endregion
}