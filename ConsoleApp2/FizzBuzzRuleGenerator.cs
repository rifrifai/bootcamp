using System;
using System.Collections.Generic;
namespace FizzBuzzRuleGenerator;

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
