// See https://aka.ms/new-console-template for more information
class Hunter
{
  public int totalShot = 5;
  public void ReplaceShot(int shot)
  {
    totalShot += shot;
  }
}

class Program
{
  static void Main()
  {
    int shot = 10;
    Hunter hunter = new();
    hunter.ReplaceShot(shot);
    Console.WriteLine(hunter.totalShot);

    hunter = new();
    hunter.ReplaceShot(shot);
    Console.WriteLine(hunter.totalShot);
  }
}