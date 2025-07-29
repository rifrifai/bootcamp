using UnoGame;

public class Card : ICard
{
  private CardType _type;
  private Color? _color;
  private Number? _number;
  private ActionType? _actionType;
  private WildType? _wildType;


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
  public void GetDisplayText()
  {
    Console.WriteLine($"");
  }
}