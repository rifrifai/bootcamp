namespace UnoGame;

public class GameController
{
  private List<IPlayer>? _players;
  private Dictionary<IPlayer, List<ICard>>? _playerhands;
  private IDeck? _deck;
  private IDiscardPile? _discardPile;
  private int _currentPlayerIndex;
  private bool _isClockWise;
  private Color? _currentWildColor;

  public Action<IPlayer>? OnPlayerTurnChanged;
  public Action<IPlayer, ICard>? OnCardPlayed;
  public Action<IPlayer>? OnUnoViolation;
  public Action<IPlayer>? OnGameEnded;
  public Func<IPlayer, ICard, List<ICard>, ICard>? CardChooser;
  public Func<Color>? WildColorChooser;
  public Func<IPlayer, bool>? UnoCallChecker;

  // public GameController()
  // {
  //   _players = new List<IPlayer>();
  //   _playerhands = new Dictionary<IPlayer, List<ICard>>();
  //   _deck = new Deck();
  //   _discardPile = new DiscardPile();
  //   _currentPlayerIndex = 0;
  //   _isClockWise = true;
  //   _currentWildColor = null;
  // }
  public void StartGame()
  {
    InitializeDeck();
    ShuffleDeck();
    DealCardsToPlayers();
    _discardPile?.SetCards(new List<ICard>());

    ICard topCard = DrawCardFromDeck(GetCurrentPlayer());
    _discardPile?.GetCards().Add(topCard);

    _currentPlayerIndex = 0;
    _isClockWise = true;
    _currentWildColor = null;

    OnPlayerTurnChanged?.Invoke(GetCurrentPlayer());
  }
  public IPlayer GetCurrentPlayer() => _players![_currentPlayerIndex];
  public void AddPlayer(IPlayer player)
  {
    _players?.Add(player);
    _playerhands?.Add(player, new List<ICard>());
  }

  private void InitializeDeck()
  {
    List<ICard> cards = new List<ICard>();

    foreach (Color color in Enum.GetValues(typeof(Color)))
    {
      cards.Add(CreateCard(CardType.Number, color, Number.Zero, null, null));

      for (int i = 1; i <= 9; i++)
      {
        cards.Add(CreateCard(CardType.Number, color, (Number)i, null, null));
        cards.Add(CreateCard(CardType.Number, color, (Number)i, null, null));
      }

      foreach (ActionType action in Enum.GetValues(typeof(ActionType)))
      {
        cards.Add(CreateCard(CardType.Action, color, null, action, null));
        cards.Add(CreateCard(CardType.Action, color, null, action, null));
      }
    }

    foreach (WildType wild in Enum.GetValues(typeof(WildType)))
    {
      for (int i = 1; i <= 4; i++)
      {
        cards.Add(CreateCard(CardType.Wild, null, null, null, wild));
      }
    }
    _deck?.SetCards(cards);
  }

  public void NextPlayer()
  {
    int direction = _isClockWise ? 1 : -1;
    _currentPlayerIndex = (_currentPlayerIndex + direction + _players!.Count) % _players!.Count;
  }
  private ICard CreateCard(CardType type, Color? color, Number? number, ActionType? action, WildType? wild)
  {
    var card = new Card();
    card.SetCardType(type);
    card.SetColor(color);
    card.SetNumber(number);
    card.SetActionType(action);
    card.SetWildType(wild);
    return card;
  }

  // private void GiveCardToPlayer(IPlayer player, ICard card)
  // {
  //   if (player is Player concretePlayer)
  //   {
  //     concretePlayer.GetHand().Add(card);
  //   }
  // }
  public void ShuffleDeck()
  {
    List<ICard> cards = _deck!.GetCards();
    Random random = new();

    int n = cards.Count;
    while (n > 1)
    {
      int k = random.Next(n + 1);
      ICard temp = cards[k];
      cards[k] = cards[n];
      cards[n] = temp;
    }
    _deck.SetCards(cards);
  }
  public ICard DrawCardFromDeck(IPlayer player)
  {
    List<ICard> cards = _deck!.GetCards();

    if (cards.Count == 0)
      throw new InvalidOperationException("Deck is empty");

    ICard topCard = cards[0];
    cards.RemoveAt(0);

    _deck.SetCards(cards);
    _playerhands![player].Add(topCard);

    return topCard;
  }
  public void AddCardToPlayer(IPlayer player, ICard card)
  {
    if (!_playerhands!.ContainsKey(player))
    {
      _playerhands[player] = new List<ICard>();
    }
  }
  public void DealCardsToPlayers()
  {
    for (int i = 0; i < 7; i++)
    {
      foreach (var player in _players!)
      {
        ICard card = DrawCardFromDeck(player);
        AddCardToPlayer(player, card);
      }
    }
  }

  public List<ICard> GetPlayerHand(IPlayer player) => _playerhands![player];
  public bool PlayCard(IPlayer player, int cardIndex)
  {
    if (!_playerhands!.ContainsKey(player)) return false;

    List<ICard> hand = _playerhands[player];

    if (cardIndex < 0 || cardIndex >= hand.Count) return false;

    ICard selectedCard = hand[cardIndex];

    hand.RemoveAt(cardIndex);

    List<ICard> discardPileCards = _discardPile!.GetCards();
    discardPileCards.Add(selectedCard);
    _discardPile.SetCards(discardPileCards);

    return true;
  }
}