// See https://aka.ms/new-console-template for more information
class Animal
{
  public Animal()
  {
    Console.WriteLine("Animal Created");
  }
}
class Cat : Animal
{
  public Cat()
  {
    Console.WriteLine("Cat Created");
  }
}

class Program
{
  static void Main()
  {
    Animal animal = new();
    Cat cat = new();
  }
}