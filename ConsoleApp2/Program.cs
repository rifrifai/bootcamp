// logic exercise
using System;
using System.Collections.Generic;

public class RuleGenerator
{
  private readonly List<(int Divisor, string Output)> _rules = new();

  public void AddRule(int divisor, string output)
  {
    _rules.Add((divisor, output));
  }

  public void Generate(int maxNumber)
  {
    if (maxNumber <= 0)
    {
      Console.WriteLine("Invalid number!");
      return;
    }

    for (int i = 1; i <= maxNumber; i++)
    {
      string result = "";

      foreach (var rule in _rules)
      {
        if (i % rule.Divisor == 0)
        {
          result += rule.Output;
        }
      }

      Console.Write(string.IsNullOrEmpty(result) ? i.ToString() : result);
      if (i < maxNumber) Console.Write(", ");
    }

    Console.WriteLine();
  }
}

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Enter a valid number please!");
    int.TryParse(Console.ReadLine(), out int answer);
    if (answer > 0)
    {
      for (int i = 1; i <= answer; i++)
      {
        string hasil = (i % 3 == 0 ? "foo" : "") +
          (i % 5 == 0 ? "bar" : "") +
          (i % 7 == 0 ? "jazz" : "") +
          (i % 4 == 0 ? "baz" : "") +
          (i % 9 == 0 ? "huzz" : "");

        Console.Write(string.IsNullOrEmpty(hasil) ? i.ToString() : hasil);
        if (i < answer) Console.Write(", ");
      }
    }
    else
    {
      Console.WriteLine("Invalid number!");
    }

  }
}