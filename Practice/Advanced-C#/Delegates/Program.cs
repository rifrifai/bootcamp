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
      InstanceAndStaticMethodTargetsDemo();
      MulticastDelegatesDemo();
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

    static void InstanceAndStaticMethodTargetsDemo()
    {
      Console.WriteLine("3. Instance and Static Method Targets");
      Console.WriteLine("=====================================");

      Console.WriteLine("Static method delegation: ");
      Transformer staticDelegate = Square;
      Console.WriteLine($"Static Square of 4 : {staticDelegate(4)}");

      Console.WriteLine("\nInstance method delegation: ");
      Calculator calculator = new(5);
      Transformer instanceDelegate = calculator.MultipliBy;

      Console.WriteLine($"Multiply 8 by {calculator.Multiplier}: {instanceDelegate(8)}");
      Console.WriteLine($"Delegate target is null (static): {staticDelegate.Target == null}");
      Console.WriteLine($"Delegate target is Calculator instance: {instanceDelegate.Target is Calculator}");

      Calculator calculator2 = new(3);
      Transformer instanceDelegate2 = calculator2.MultipliBy;

      Console.WriteLine($"Different instance - multiply 8 by {calculator2.Multiplier}: {instanceDelegate2(8)}");
      Console.WriteLine();
    }
    public class Calculator
    {
      private int _multiplier;
      public Calculator(int multiplier)
      {
        this._multiplier = multiplier;
      }
      public int Multiplier => _multiplier;
      public int MultipliBy(int input)
      {
        return input * _multiplier;
      }

    }
    delegate void ProgressReporter(int percentComplete);

    static void MulticastDelegatesDemo()
    {
      Console.WriteLine("4. Multiple Delegates - Combining Multiple Methods");
      Console.WriteLine("==================================================");

      ProgressReporter reporter = WriteProgressToConsole;
      reporter += WriteProgressToFile;
      reporter += SendProgressAlert;

      Console.WriteLine("Progress reporting with multicast delegate (3 methods):");
      reporter(76);

      Console.WriteLine("\nRemoving console reporter using -= operator:");
      reporter -= WriteProgressToConsole;

      Console.WriteLine("Progress reporting after removal (2 methods): ");
      if (reporter != null)
      {
        reporter(80);
      }

      Console.WriteLine("\nMulticast with return values (only last one is kept): ");
      Transformer multiTransformer = Square;
      multiTransformer += Cube;

      int lastResult = multiTransformer(3);
      Console.WriteLine($"Only last result is returned: {lastResult}");
      Console.WriteLine();
    }
    static void WriteProgressToConsole(int percentComplete)
    {
      Console.WriteLine($"  Console Log: {percentComplete}% complete");
    }
    static void WriteProgressToFile(int percentComplete)
    {
      Console.WriteLine($"  File Log: {percentComplete}% complete");
    }
    static void SendProgressAlert(int percentComplete)
    {
      if (percentComplete >= 80)
      {
        Console.WriteLine($"  Alert: High progress reached - {percentComplete}%");
      }
    }
  }
}