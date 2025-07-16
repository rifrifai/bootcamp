// See https://aka.ms/new-console-template for more information
// inheritance grandParent

class A
{
  public A()
  {
    Console.WriteLine("A created");
  }
  public void Running()
  {
    Console.WriteLine("Running");
  }
}

class B : A
{
  public B()
  {
    Console.WriteLine("B created");
  }
}
class C : B
{
  public C()
  {
    Console.WriteLine("C created");
  }
}
class D : C
{
  public D()
  {
    Console.WriteLine("D created");
  }
}
class Program
{
  static void Main()
  {
    D d = new();
    d.Running();
  }
}