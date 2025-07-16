// See https://aka.ms/new-console-template for more information
class Hunter
{
  public int totalShot = 10;
  public void ReplaceShot(int shot)
  {
    totalShot += shot;
  }
}
class Program
{
  static void Main()
  {
    int shot = 5;
    Hunter hunter = new();
    hunter.ReplaceShot(shot);
    Console.WriteLine(hunter.totalShot);

    shot = 3;
    hunter = new Hunter();
    hunter.ReplaceShot(shot);
    Console.WriteLine(hunter.totalShot);
  }
}
