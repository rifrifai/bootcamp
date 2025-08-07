public class CardEffectResult
{
  public string Message { get; set; }
  public ConsoleColor Color { get; set; }

  public CardEffectResult(string message, ConsoleColor color)
  {
    Message = message;
    Color = color;
  }
}
