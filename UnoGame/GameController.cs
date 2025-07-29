namespace UnoGame;

public class GameController
{
  private List<IPlayer> _players = new();
  private Dictionary<IPlayer, List<ICard>> _playerHands = new();
  private IDeck? _deck;
  private IDiscardPile? _discardPile;
  private int _currentPlayerIndex = 0;
  private bool _isClockwise = true;
  private Color? _currentWildColor = null;

  public Action<IPlayer>? OnPlayerTurnChanged;
  public Action<IPlayer, ICard>? OnCardPlayed;
  public Action<IPlayer>? OnUnoViolation;
  public Action<IPlayer>? OnGameEnded;
  public Func<IPlayer, ICard, List<ICard>, ICard>? CardChooser;
  public Func<Color>? WildColorChooser;
  public Func<IPlayer, bool>? UnoCallChecker;

  public void StartGame()
  {
    InitializeDeck();
    DealCardsToPlayers();
    _discardPile?.AddCard(_deck.GetCardAt(0));
    _deck?.SetCards(_deck.GetCards().Skip(1).ToList());
    GameLoop();
  }

  private void InitializeDeck()
  {
    List<ICard> cards = new();
    foreach (Color color in Enum.GetValues(typeof(Color)))
    {
      foreach (Number number in Enum.GetValues(typeof(Number)))
      {
        cards.Add(new Card { GetCardType = CardType.Number, Color = color, Number = number });
      }
      foreach (ActionType action in Enum.GetValues(typeof(ActionType)))
      {
        cards.Add(new Card { CardType = CardType.Action, Color = color, ActionType = action });
      }
    }
    foreach (WildType wild in Enum.GetValues(typeof(WildType)))
    {
      cards.Add(new Card { CardType = CardType.Wild, WildType = wild });
    }
    _deck = new Deck();
    _deck.SetCards(cards);
    ShuffleDeck();
    _discardPile = new DiscardPile();
  }

  public void AddPlayer(IPlayer player)
  {
    _players.Add(player);
    _playerHands[player] = new List<ICard>();
  }

  private void GameLoop()
  {
    while (!IsGameOver())
    {
      var player = GetCurrentPlayer();
      OnPlayerTurnChanged?.Invoke(player);
      var hand = GetPlayerHand(player);
      var topCard = GetTopDiscardCard();
      var playableCards = GetPlayableCardsFromPlayer(player, topCard);

      if (playableCards.Count == 0)
      {
        var drawn = DrawCardFromDeck(player);
        AddCardToPlayer(player, drawn);
      }
      else
      {
        var chosenCard = ChooseCard(player, topCard, playableCards);
        PlayCard(player, chosenCard);
      }

      if (CheckUnoViolation(player))
      {
        PenalizePlayer(player);
        OnUnoViolation?.Invoke(player);
      }

      if (IsGameOver())
      {
        OnGameEnded?.Invoke(player);
        break;
      }

      NextPlayer();
    }
  }

  public void NextPlayer()
  {
    _currentPlayerIndex = (_currentPlayerIndex + (_isClockwise ? 1 : -1) + _players.Count) % _players.Count;
  }

  public IPlayer GetCurrentPlayer() => _players[_currentPlayerIndex];

  public void AddCardToPlayer(IPlayer player, ICard card) => _playerHands[player].Add(card);

  public bool RemoveCardFromPlayer(IPlayer player, ICard card) => _playerHands[player].Remove(card);

  public List<ICard> GetPlayerHand(IPlayer player) => _playerHands[player];

  public bool IsGameOver() => _playerHands.Values.Any(hand => hand.Count == 0);

  public IPlayer? GetWinner() => _playerHands.FirstOrDefault(kvp => kvp.Value.Count == 0).Key;

  public bool PlayCard(IPlayer player, ICard card)
  {
    if (!PlayerHasCard(player, card) || !CanPlayCard(card, GetTopDiscardCard()))
      return false;

    RemoveCardFromPlayer(player, card);
    AddCardToDiscardPile(card);
    ExecuteCardEffect(card);
    OnCardPlayed?.Invoke(player, card);
    return true;
  }

  public bool PlayerHasCard(IPlayer player, ICard card) => _playerHands[player].Contains(card);

  public ICard DrawCardFromDeck(IPlayer player)
  {
    if (IsDeckEmpty()) RecycleDiscardPile();
    var card = _deck.GetCardAt(0);
    _deck.SetCards(_deck.GetCards().Skip(1).ToList());
    return card;
  }

  public void ShuffleDeck()
  {
    var rng = new Random();
    var cards = _deck.GetCards();
    _deck.SetCards(cards.OrderBy(_ => rng.Next()).ToList());
  }

  public void AddCardToDiscardPile(ICard card) => _discardPile.SetCards(new List<ICard>(_discardPile.GetCards()) { card });

  public ICard GetTopDiscardCard() => _discardPile.GetCards().Last();

  public bool CanPlayCard(ICard card, ICard topCard)
  {
    if (card.GetCardType() == CardType.Wild) return true;
    if (_currentWildColor != null && card.GetColor() == _currentWildColor) return true;
    return card.GetColor() == topCard.GetColor() ||
           card.GetNumber() == topCard.GetNumber() ||
           card.GetActionType() == topCard.GetActionType();
  }

  public List<ICard> GetPlayableCardsFromPlayer(IPlayer player, ICard topCard) =>
      _playerHands[player].Where(card => CanPlayCard(card, topCard)).ToList();

  public ICard ChooseCard(IPlayer player, ICard topCard, List<ICard> playableCards) =>
      CardChooser?.Invoke(player, topCard, playableCards) ?? playableCards.First();

  public void ExecuteCardEffect(ICard card)
  {
    switch (card.GetCardType())
    {
      case CardType.Action:
        switch (card.GetActionType())
        {
          case ActionType.Skip:
            NextPlayer();
            break;
          case ActionType.Reverse:
            ReverseDirection();
            break;
          case ActionType.DrawTwo:
            var target = GetNextPlayer();
            AddCardToPlayer(target, DrawCardFromDeck(target));
            AddCardToPlayer(target, DrawCardFromDeck(target));
            break;
        }
        break;
      case CardType.Wild:
        _currentWildColor = ChooseWildColor();
        if (card.GetWildType() == WildType.WildDrawFour)
        {
          var target = GetNextPlayer();
          for (int i = 0; i < 4; i++) AddCardToPlayer(target, DrawCardFromDeck(target));
        }
        break;
    }
  }

  public IPlayer GetNextPlayer()
  {
    int nextIndex = (_currentPlayerIndex + (_isClockwise ? 1 : -1) + _players.Count) % _players.Count;
    return _players[nextIndex];
  }

  public void ReverseDirection() => _isClockwise = !_isClockwise;

  public bool CheckUnoViolation(IPlayer player)
  {
    return GetPlayerHandSize(player) == 1 && !UnoCallChecker?.Invoke(player) == true;
  }

  public void PenalizePlayer(IPlayer player)
  {
    AddCardToPlayer(player, DrawCardFromDeck(player));
    AddCardToPlayer(player, DrawCardFromDeck(player));
  }

  public int GetPlayerHandSize(IPlayer player) => _playerHands[player].Count;

  public Color ChooseWildColor() => WildColorChooser?.Invoke() ?? Color.Red;

  public void RecycleDiscardPile()
  {
    var cards = _discardPile.GetCards();
    var lastCard = cards.Last();
    cards.RemoveAt(cards.Count - 1);
    _deck.SetCards(cards);
    _discardPile.SetCards(new List<ICard> { lastCard });
    ShuffleDeck();
  }

  public void DealCardsToPlayers()
  {
    foreach (var player in _players)
    {
      for (int i = 0; i < 7; i++)
        AddCardToPlayer(player, DrawCardFromDeck(player));
    }
  }

  public List<IPlayer> GetAllPlayers() => _players;

  public List<Color> GetValidColors() => Enum.GetValues(typeof(Color)).Cast<Color>().ToList();

  public IPlayer? GetPlayerByName(string name) => _players.FirstOrDefault(p => p.GetName() == name);

  public List<int> GetPlayerHandSizes() => _players.Select(p => GetPlayerHandSize(p)).ToList();

  public bool CallUno(IPlayer player) => true;

  public bool ValidateCard(ICard card) => true;

  public int GetDeckCardCount() => _deck.GetCards().Count;

  public int GetDiscardPileCardCount() => _discardPile.GetCards().Count;

  public bool IsDeckEmpty() => !_deck.GetCards().Any();

  public bool IsDiscardPileEmpty() => !_discardPile.GetCards().Any();

  public void ClearPlayerHand(IPlayer player) => _playerHands[player].Clear();
}