// See https://aka.ms/new-console-template for more information
class People
{
  public string? name = "noname";
  public string? address = "salatiga";
  public int age = 20;
  public void Job()
  {
    Console.WriteLine("has job");
  }
  public void Hobby()
  {
    Console.WriteLine("has hobby");
  }
}
class Santri : People
{
  public int idSantri = 2121;

}
class Program
{
  static void Main()
  {
    Santri santri = new();
    santri.name = "rifai";
    Console.WriteLine(santri.name);
    santri.Job();
  }
}
