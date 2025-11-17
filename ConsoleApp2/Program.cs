// logic exercise
using System;
using System.Collections.Generic;
namespace FizzBuzzRuleGenerator;

using System;

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

// public class Program
// {
//   public static void Main()
//   {
//     Console.WriteLine("Enter a valid number please!");
//     if (int.TryParse(Console.ReadLine(), out int answer))
//     {
//       var generator = new RuleGenerator();

//       // Configurable rules via AddRule:
//       generator.AddRule(3, "foo");
//       generator.AddRule(5, "bar");
//       generator.AddRule(7, "jazz");
//       generator.AddRule(4, "baz");
//       generator.AddRule(9, "huzz");

//       // Run the generator
//       generator.Generate(answer);
//     }
//     else
//     {
//       Console.WriteLine("Invalid number!");
//     }
//   }
// }