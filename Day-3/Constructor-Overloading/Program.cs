// See https://aka.ms/new-console-template for more information
// constructor overloading
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
    Console.WriteLine($"Santri created, name: {name}, age: {age} years old, tall: {tall} cm, address: {address}");
  }
  public Santri(string name, string address)
  {
    Console.WriteLine("Santri created");
  }
}
class Program
{
  static void Main()
  {
    Santri santri1 = new(17, 180, "dzulqarnain", "demak");
    Santri santri2 = new("sodik", "jepara");
  }
}
