using UnoRevisi.View;

namespace UnoRevisi;

class Program
{
  private static Display _display;

  public static void Main()
  {
    _display = new Display();
    _display.ShowWelcome();

    _display.GameLoop();
  }
}