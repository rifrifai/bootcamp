namespace ExceptionHandling
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("=== Exception Handling in C# - Complete Training Demonstration ===\n");
      Console.WriteLine("This program demonstrates all major concepts of exception handling:");

      BasicTryCatch();
    }

    static void BasicTryCatch()
    {
      Console.WriteLine("1. BASIC TRY-CATCH DEMONSTRATION");
      Console.WriteLine("=================================");
      Console.WriteLine("The most fundamental concept in exception handling is the try-catch block.");
      Console.WriteLine("A try block contains code that might throw an exception.");
      Console.WriteLine("A catch block handles the exception if it occurs.\n");

      // This demonstrates the basic structure: try { risky code } catch { handle error }
      Console.WriteLine("Testing division by zero - without try-catch this would crash:");
      try
      {
        // This line will throw a DevideByZeroException
        int result = Calc(0);
        Console.WriteLine($"Result: {result}");  //this line won't execute

      }
      catch (DivideByZeroException ex)
      {
        // Execution jumps here when the exception is thrown
        Console.WriteLine("✓ Caught DivideByZeroException - program continues running");
        Console.WriteLine($"  Exception message: {ex.Message}");
        Console.WriteLine($"  Exception type: {ex.GetType().Name}");
      }
      Console.WriteLine("✓ Program execution continues after exception handling\n");

      // Important principle: Prevention is better than exception handling
      Console.WriteLine("Better approach - validate input before risky operations:");
      int safeResult = SafeCalc(0);
      Console.WriteLine($"Safe result: {safeResult}");
      Console.WriteLine("Remember: Exceptions are expensive - use them for truly exceptional situations!\n");
    }

    // Method that demonstrates what happens without input validation
    static int Calc(int x)
    {
      return 10 / x;
    }

    // Better approach - defensive programming with validation
    static int SafeCalc(int x)
    {
      // Always validate inputs when possible rather than relying on exception handling
      if (x == 0)
      {
        Console.WriteLine("  Warning: Division by zero attempted, returning safe value");
        return 0; // Or throw a more descriptive exception
      }
      return 10 / x;
    }
  }
}