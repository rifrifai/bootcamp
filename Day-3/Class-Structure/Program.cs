// See https://aka.ms/new-console-template for more information
using Animal;
class Program
{
  static void Main()
  {
    Dog jack = new();
    jack.name = "dog";
    jack.age = 2;
    jack.isManja = false;

    Dragon rack = new();
    rack.name = "dragon";
    rack.age = 1000;
    rack.isFired = true;

    Console.WriteLine(jack.name);
    Console.WriteLine(jack.age);
    Console.WriteLine(jack.isManja);

    Console.WriteLine(rack.name);
    Console.WriteLine(rack.age);
    Console.WriteLine(rack.isFired);
  }
}
