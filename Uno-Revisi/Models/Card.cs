using UnoRevisi.Enums;
using UnoRevisi.Interfaces;

namespace UnoRevisi.Models;

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