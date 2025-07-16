// See https://aka.ms/new-console-template for more information
using CalculatorLib;
class Program
{
  static void Main()
  {
    Calculator calculator = new();
    // take input user from terminal
    string userInput = Console.ReadLine() ?? "10";
    string userInput2 = Console.ReadLine() ?? "10";

    // convert string to int using parse
    int intUserInput = Convert.ToInt32(userInput);
    int intUserInput2 = Convert.ToInt32(userInput2);

    // call calculator method
    int result = calculator.Add(intUserInput, intUserInput2);
    int result2 = calculator.Multiply(intUserInput, intUserInput2);
    Console.WriteLine(result);
    Console.WriteLine(result2);
  }
}