// See https://aka.ms/new-console-template for more information
class Hunter
{
  public int totalShot = 2;
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
    Hunter hunter1 = new();
    hunter1.ReplaceShot(shot);
    Console.WriteLine(hunter1.totalShot);

    Hunter hunter2 = new();
    hunter2.ReplaceShot(shot);
    Console.WriteLine(hunter2.totalShot);
  }
}