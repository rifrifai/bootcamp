namespace UnoGame;

public interface IPlayer
{
  public string GetName();
  public void SetName(string name);
};

public interface IDeck
{
  public List<ICard> GetCards();
  public ICard GetCardAt(int index);
  public void SetCards(List<ICard> cards);
  public void SetCardAt(int index, ICard card);
};

public interface IDiscardPile
{
  public List<ICard> GetCards();
  public ICard GetCardAt(int index);
  public void SetCard(List<ICard> cards);
  public void SetCardAt(int index, ICard card);
};

public interface ICard
{
  public CardType GetCardType();
  public Color? GetColor();
  public Number? GetNumber();
  public ActionType? GetActionType();
  public WildType? GetWildType();
  public void GetDisplayText();
  public void SetCardType(CardType type);
  public void SetColor(Color? color);
  public void SetNumber(Number? number);
  public void SetActionType(ActionType? actionType);
  public void SetWildType(WildType? wildType);
};