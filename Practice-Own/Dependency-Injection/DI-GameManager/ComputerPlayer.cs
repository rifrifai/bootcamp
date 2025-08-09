namespace DI_GameManager;

public class ComputerPlayer : IPlayer
{
  private Random _random = new();
  public Choice GetChoice()
  {
    Choice p2 = (Choice)_random.Next(0, 3);
    return p2;
  }
}