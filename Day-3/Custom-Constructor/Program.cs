// See https://aka.ms/new-console-template for more information
// constructor with parameter
class Santri
{
  public int age;
  public int tall;
  public string? name;
  public string? address;
  public Santri(int age, int tall, string name, string address)
  {
    this.age = age;
    this.tall = tall;
    this.name = name;
    this.address = address;
  }
}
class Program
{
  static void Main()
  {
    Santri santri1 = new(12, 175, "rakha", "salatiga");
    Console.WriteLine(santri1.name);
    Console.WriteLine(santri1.address);
    Console.WriteLine(santri1.age);
    Console.WriteLine(santri1.tall);
    Santri santri2 = new(13, 160, "ahmad", "kebumen");
    Console.WriteLine(santri2.name);
    Console.WriteLine(santri2.address);
    Console.WriteLine(santri2.age);
    Console.WriteLine(santri2.tall);
  }
}
