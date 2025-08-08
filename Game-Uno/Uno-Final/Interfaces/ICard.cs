namespace Uno;

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