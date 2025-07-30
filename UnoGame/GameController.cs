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

  public GameController()
  {
    _players = new List<IPlayer>();
    _playerhands = new Dictionary<IPlayer, List<ICard>>();
    _deck = new Deck();
    _discardPile = new DiscardPile();
    _currentPlayerIndex = 0;
    _isClockWise = true;
    _currentWildColor = null;

  }
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
  public void ShuffleDeck() { }
  public void DealCardsToPlayers() { }
  public ICard DrawCardFromDeck(IPlayer player) => {};
}