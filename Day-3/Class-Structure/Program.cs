// See https://aka.ms/new-console-template for more information
using Animal;
class Program
{
  static void Main()
  {
    Dog jack = new();
    jack.name = "jack";
    jack.age = 2;
    jack.isManja = false;

    Console.WriteLine(jack.name);
    Console.WriteLine(jack.age);
    Console.WriteLine(jack.isManja);
  }
}
