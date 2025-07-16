// See https://aka.ms/new-console-template for more information
class ParentKandung
{
  static void Running()
  {
    Console.WriteLine("Parent Running");
  }
}

class ParentAngkat
{
  static void Running()
  {
    Console.WriteLine("Parent Angkat Running");
  }
}

class Child : ParentKandung, ParentAngka
{
  Child child = new();
  child.Running();
}