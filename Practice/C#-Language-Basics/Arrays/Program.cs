// See https://aka.ms/new-console-template for more information
using System.Net;

namespace ArraysDemo
{
  public class Point
  {
    public int X { get; set; } //method/property
    public int Y { get; set; }
    public Point() : this(0, 0) { }
    public Point(int x, int y)
    {
      X = x;
      Y = y;
    }
    public override string ToString()
    {
      return $"({X}, {Y})";
    }
  }

  class Program
  {
    static void Main()
    {
      Console.WriteLine("=== ARRAYS IN C# - COMPLETE MASTERY GUIDE ===\n");

      ArrayDeclaration();
      ArrayInitialization();
    }

    static void ArrayDeclaration()
    {
      Console.WriteLine("1. ARRAY DECLARATION FUNDAMENTALS");
      Console.WriteLine("==================================");

      char[] vokals = new char[5];
      int[] numbers = new int[10];
      string[] names = new string[3];

      Console.WriteLine($"Created vokals array with {vokals.Length}");
      Console.WriteLine($"Created numbers array with {numbers.Length}");
      Console.WriteLine($"Created names array with {names.Length} elements");

      // key insight: at this point, arrays exist but contain default values
      Console.WriteLine($"Default char value: '{vokals[0]}' (appears empty but it's \\0)");
      Console.WriteLine($"Default int value: {numbers[0]}");
      Console.WriteLine($"Default string value: {names[0] ?? "null"}");

      // pro tip: always remember that Length gives tou the total capacity,
      // NOT the number of meaningful elements you've added
      Console.WriteLine($"\nImportant: Length = {vokals.Length} means indices 0 to {vokals.Length - 1}");

      Console.WriteLine();
    }

    static void ArrayInitialization()
    {
      Console.WriteLine("2. ARRAY INITIALIZATION");
      Console.WriteLine("=======================");

      char[] vokals = { 'a', 'i', 'u', 'e', 'o' };
      Console.WriteLine("Method 1 - Direct initialization: ");
      Console.WriteLine($"Vokals: {string.Join(", ", vokals)}");

      char[] consonant = ['b', 'c', 'd', 'f', 'g'];
      Console.WriteLine($"Consonants (C# 12 syntax): {string.Join(", ", consonant)}");

      // declare first, then assign element individually
      // use this when you need to calculate or input values dynamically
      int[] fibonacci = new int[15];
      fibonacci[0] = 0;
      fibonacci[1] = 1;

      // hasilkan urutan fibonacci/ generate fibonacci sequence
      for (int i = 2; i < fibonacci.Length; i++)
      {
        fibonacci[i] = fibonacci[i - 1] + fibonacci[i - 2];
      }
      Console.WriteLine($"Fibonacci sequence: {string.Join(", ", fibonacci)}");

      // Array.Fill for identical values
      // perfect for initialization scenarios
      int[] scores = new int[5];
      Array.Fill(scores, 100);    //set all elements to 100
      Console.WriteLine($"All perfect scores: {string.Join(", ", scores)}");

      // menggunakan loop untuk inisialisasi berbasis pola/ pattern-based
      double[] powers = new double[8];
      for (int i = 0; i < powers.Length; i++)
      {
        powers[i] = Math.Pow(2, i);
      }
      Console.WriteLine($"Power of 2: {string.Join(", ", powers)}");
      Console.WriteLine();
    }

  }
}
