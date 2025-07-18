// See https://aka.ms/new-console-template for more information
using System.Xml.XPath;

namespace DelegatesDemo
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("=== Delegates in C# - Complete Demonstration ===\n");

      // run all delegate demonstration
      BasicDelegateDemo();
      PluginMethodsDemo();
    }
    delegate int Transformer(int x);
    static void BasicDelegateDemo()
    {
      Console.WriteLine("1. Basic Delegate Usage - The Foundation");
      Console.WriteLine("=====================================");
      Transformer t = Square;
      int hasil = t(3);
      Console.WriteLine($"Square of 3 through delegate: {hasil}");

      t = Cube;
      hasil = t(2);
      Console.WriteLine($"Cube of 2 through delegate: {hasil}");

      // explicit invoke method
      hasil = t.Invoke(4);
      Console.WriteLine($"Cube of 4 using Invoke: {hasil}");

      Console.WriteLine();
    }
    static int Square(int x) => x * x;
    static int Cube(int x) => x * x * x;

    static void PluginMethodsDemo()
    {
      Console.WriteLine("2. Writing Plugin Methods with Delegates");
      Console.WriteLine("========================================");
      int[] values = { 1, 2, 3, 4, 5 };
      Console.WriteLine($"Original values: [{string.Join(", ", values)}]");
      Transform(values, Square);
      Console.WriteLine($"After Square Transform: [{string.Join(", ", values)}]");

      values = new int[] { 1, 2, 3, 4, 5 };
      Transform(values, Cube);
      Console.WriteLine($"After Cube Transform: [{string.Join(", ", values)}]");

      // lambda expressions
      values = new int[] { 1, 2, 3, 4, 5 };
      Transform(values, x => x + 5);
      Console.WriteLine($"After +5 Transform: [{string.Join(", ", values)}]");

      Console.WriteLine();
    }
    static void Transform(int[] values, Transformer t)
    {
      for (int i = 0; i < values.Length; i++)
      {
        values[i] = t(values[i]);
      }
    }
  }
}