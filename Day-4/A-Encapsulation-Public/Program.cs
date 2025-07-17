// See https://aka.ms/new-console-template for more information
// public
class Santri
{
  public string? name;
  public int age;
  public string? hobby;
  public void Do()
  {
    Console.WriteLine("Santri Mengaji");
  }
}

class Program
{
  static void Main()
  {
    Santri santri = new();
    santri.name = "Ahmad";
    santri.age = 22;
    santri.hobby = "Gaming";
    Console.WriteLine(santri.name);
    santri.Do();
  }
}