// See https://aka.ms/new-console-template for more information
namespace ExpressionAndOperatorsDemo
{
  public class Product
  {
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public Product(string name, decimal price, int stock)
    {
      Name = name;
      Price = price;
      Stock = stock;
    }
    public decimal CalculateTotal(int quantity) => Price * quantity;
    public bool IsInStock()
    {
      return Stock > 0;
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("=== EXPRESSION AND OPERATORS IN C# ===");
      Console.WriteLine("This's where you learn to build complex logic from simple building blocks\n");

      ConstantAndVariables();
      BinaryOperators();
    }
    static void ConstantAndVariables()
    {
      Console.WriteLine("=== CONSTANT AND VARIABLES ===");
      Console.WriteLine("These are the simplest expression - the atoms of your code\n");

      // constant - values that never change
      Console.WriteLine("--- Constant (Fixed Values) ---");
      const int MaxUsers = 1000;
      const double Pi = 3.14159;
      const string AppName = "MyApp";
      const bool IsProduction = false;

      Console.WriteLine($"Integer Constant: {MaxUsers}");
      Console.WriteLine($"Double Constant: {Pi}");
      Console.WriteLine($"String Constant: {AppName}");
      Console.WriteLine($"Boolean Constant: {IsProduction}");

      // variables - values that can change during execution
      Console.WriteLine("\n--- Variables (Changeable Values) ---");
      int currentUsers = 250;
      double radius = 0.5;
      string userName = "Ahmad";
      bool isLoggedIn = true;

      Console.WriteLine($"Current users: {currentUsers}");
      Console.WriteLine($"Circle radius: {radius}");
      Console.WriteLine($"Username: {userName}");
      Console.WriteLine($"Logged In: {isLoggedIn}");

      // variables can be changed, that's the point
      currentUsers = 300;
      userName = "Putri";
      isLoggedIn = false;

      Console.WriteLine("\nAfter changes: ");
      Console.WriteLine($"Current users: {currentUsers}");
      Console.WriteLine($"Username: {userName}");
      Console.WriteLine($"Logged In: {isLoggedIn}");

      Console.WriteLine("\nKunci Utama: Constants provide stability, variables provide flexibility");
    }

    static void BinaryOperators()
    {
      Console.WriteLine("=== BINARY OPERATORS  ===");
      Console.WriteLine("Two operands, one operator - the workhorses of programming \n");

      // arithmatic binary operators
      Console.WriteLine("--- Arithmetic Operators ---");
      int a = 12, b = 10;

      Console.WriteLine($"Starting values: a = {a}, b = {b}");
      Console.WriteLine($"Addition: {a} + {b} = {a + b}");
      Console.WriteLine($"Substraction: {a} - {b} = {a - b}");
      Console.WriteLine($"Multiplication: {a} * {b} = {a * b}");
      Console.WriteLine($"Division: {a} / {b} = {a / b}");
      Console.WriteLine($"Remainder: {a} % {b} = {a % b}");

      // string concatenation - binary operators for strings
      Console.WriteLine("\n--- String Concatenation ---");
      string firstName = "Ahmad";
      string lastName = "Dahlan";
      string fullName = firstName + " " + lastName; // binary + operator for strings

      Console.WriteLine($"First name: {firstName}");
      Console.WriteLine($"Last name: {lastName}");
      Console.WriteLine($"Full name: {fullName}");

      // comparison operators
      Console.WriteLine("\n--- Comparison Operators ---");
      int x = 10, y = 30;

      Console.WriteLine($"Values: x = {x}, y = {y}");
      Console.WriteLine($"x == y: {x == y}");
      Console.WriteLine($"x != y: {x != y}");
      Console.WriteLine($"x < y: {x < y}");
      Console.WriteLine($"x > y: {x > y}");
      Console.WriteLine($"x <= y: {x <= y}");
      Console.WriteLine($"x >= y: {x >= y}");

      // logical operators
      Console.WriteLine("\n--- Logical Operatos ---");
      bool isWeekend = true;
      bool hasFreetime = false;

      Console.WriteLine($"Is weekend: {isWeekend}");
      Console.WriteLine($"Has free time: {hasFreetime}");
      Console.WriteLine($"Can relax (AND): {isWeekend && hasFreetime}");
      Console.WriteLine($"Can enjoy (OR): {isWeekend || hasFreetime}");

      Console.WriteLine($"\nRemember: Binary operators need exactly two operands to work\n");
    }
  }
}