namespace Uno;

public class Player : IPlayer
{
  private string _name;

  public Player(string name)
  {
    _name = name;
  }

  public string GetName() => _name;
  public void SetName(string name) => _name = name;
}