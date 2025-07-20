using System.Text;

namespace VariablesAndParameters
{
  public struct BigCalculationData
  {
    public double[] Values;
    public string Description;
    public DateTime Timestamp;
    public BigCalculationData(int size, string desc)
    {
      Values = new double[size];
      Description = desc;
      Timestamp = DateTime.Now;

      // fill with some sample data
      for (int i = 0; i < size; i++)
      {
        Values[i] = i * 1.5;
      }
    }
    public double Sum()
    {
      double total = 0;
      foreach (double var in Values)
      {
        total += var;
      }
      return total;
    }
  }
  class Program
  {
    // Static field to demonstrate ref returns
    // In production code, be careful with global state like this
    // private static string globalMessage = "Initial Global Value";
    static void Main(string[] args)
    {
      Console.WriteLine("=== VARIABLES AND PARAMETERS IN C# ===");
      Console.WriteLine("This demo covers everything from basic variables to advanced parameter techniques\n");

      // Section 1: Stack vs Heap Demonstration
      StackAndHeap();

      // Section 2: Definite Assignment Rules
      DefiniteAssignment();

      // Section 3: Default Values
      DefaultValues();

      // Section 4: Parameters Passing
      ParameterPassing();

    }

    static void StackAndHeap()
    {
      Console.WriteLine("=== STACK VS HEAP MEMORY ===");
      Console.WriteLine("Understanding where your data lives is crucial for performance and memory management\n");

      // stack example - local variables and method calls
      Console.WriteLine("--- Stack Memory (Local Variables) ---");
      Console.WriteLine("Each method call creates a stack frame. Watch how recursion builds up the stack:");

      int number = 5;
      int result = CalculateFactorial(number);
      Console.WriteLine($"Factorial of {number} = {result}");
      Console.WriteLine($"Notice: Each recursive call added a new 'x' variable to the stack\n");

      // heap example - reference types
      Console.WriteLine("--- Heap Memory (Reference Types) ---");
      Console.WriteLine("Objects created with 'new' go on the heap. Multiple variables can reference the same object:");

      // creating objects on the heap
      StringBuilder builder1 = new("First Object");
      Console.WriteLine($"builder1 content: {builder1}");

      StringBuilder builder2 = new("Second Object");
      StringBuilder builder3 = builder2;   // both reference the same heap object

      Console.WriteLine($"builder2 content: {builder2}");
      Console.WriteLine($"builder3 content: {builder3}");

      // modifying through one reference affects the other
      builder2.Append(" - modified!");
      Console.WriteLine($"After modifying builder2:");
      Console.WriteLine($"builder2 content: {builder2}");
      Console.WriteLine($"builder3 content: {builder3}"); // Same object!
      Console.WriteLine("Key point: builder2 and builder3 point to the same heap object\n");

    }

    // Recursive method to demonstrate stack usage
    // Each call creates a new stack frame with its own 'x' parameter
    static int CalculateFactorial(int x)
    {
      Console.WriteLine($"  Computing factorial for: {x} (new stack frame created)");

      if (x == 0 || x == 1)
      {
        Console.WriteLine($"  Base case reached, returning 1 (stack frame will be destroyed)");
        return 1;
      }
      return x * CalculateFactorial(x - 1);
    }
    static void DefiniteAssignment()
    {
      Console.WriteLine("=== DEFINITE ASSIGNMENT RULES ===");
      Console.WriteLine("C# ensures variables are initialized before use - this prevents nasty bugs!\n");

      // This would cause a compile error:
      // int uninitialized;
      // Console.WriteLine(uninitialized); // Error: Use of unassigned local variable

      Console.WriteLine("--- Local Variables Must Be Initialized ---");
      int localVar = 42; // Must initialize before use
      Console.WriteLine($"Local variable properly initialized: {localVar}");

      Console.WriteLine("\n--- Arrays and Fields Auto-Initialize ---");
      int[] numbers = new int[3];
      Console.WriteLine($"Array elements auto-initialize to default values:");
      for (int i = 0; i < numbers.Length; i++)
      {
        Console.WriteLine($"  numbers[{i}] = {numbers[i]} (auto-initialized to 0)");
      }

      // Demonstrating conditional initialization
      Console.WriteLine("\n--- Conditional Initialization Example ---");
      int conditionalValue;
      bool shouldInitialize = DateTime.Now.Second % 2 == 0;

      if (shouldInitialize)
      {
        conditionalValue = 100;
      }
      else
      {
        conditionalValue = 200;
      }

      // Now it's safe to use conditionalValue because both code paths initialize it
      Console.WriteLine($"Conditionally initialized value: {conditionalValue}");
      Console.WriteLine("Compiler ensures all code paths initialize the variable before use\n");
    }

    static void DefaultValues()
    {
      Console.WriteLine("=== DEFAULT VALUES FOR TYPES ===");
      Console.WriteLine("Every type in C# has a predictable default value. Know these by heart!\n");

      // Numeric types default to zero
      Console.WriteLine("--- Numeric Type Defaults ---");
      Console.WriteLine($"int default: {default(int)}");
      Console.WriteLine($"double default: {default(double)}");
      Console.WriteLine($"decimal default: {default(decimal)}");
      Console.WriteLine($"float default: {default(float)}");

      // Boolean defaults to false
      Console.WriteLine($"\nbool default: {default(bool)}");

      // Character defaults to null character
      Console.WriteLine($"char default: '{default(char)}' (null character)");

      // Reference types default to null
      Console.WriteLine($"\nstring default: {default(string) ?? "null"}");
      Console.WriteLine($"object default: {default(object) ?? "null"}");

      // Custom structs get all fields defaulted
      Console.WriteLine("\n--- Custom Struct Defaults ---");
      BigCalculationData defaultStruct = default(BigCalculationData);
      Console.WriteLine($"Custom struct Values array: {(defaultStruct.Values == null ? "null" : $"array with {defaultStruct.Values.Length} elements")}");
      Console.WriteLine($"Custom struct Description: {defaultStruct.Description ?? "null"}");
      Console.WriteLine($"Custom struct Timestamp: {defaultStruct.Timestamp}");

      Console.WriteLine("\nRemember: Default values are predictable and safe - use them to your advantage!\n");
    }

    static void ParameterPassing()
    {
      Console.WriteLine("=== PARAMETER PASSING MODES ===");
      Console.WriteLine("Master these concepts and you'll write more efficient, predictable code\n");

      // Pass by value (default behavior)
      Console.WriteLine("--- Pass by Value (Default) ---");
      int originalValue = 10;
      Console.WriteLine($"Before calling ModifyByValue: originalValue = {originalValue}");
      ModifyByValue(originalValue);
      Console.WriteLine($"After calling ModifyByValue: originalValue = {originalValue}");
      Console.WriteLine("Value unchanged - method worked with a copy\n");

      // Pass by reference with 'ref'
      Console.WriteLine("--- Pass by Reference (ref keyword) ---");
      int refValue = 10;
      Console.WriteLine($"Before calling ModifyByRef: refValue = {refValue}");
      ModifyByRef(ref refValue);
      Console.WriteLine($"After calling ModifyByRef: refValue = {refValue}");
      Console.WriteLine("Value changed - method worked with the original variable\n");

      // Output parameters with 'out'
      Console.WriteLine("--- Output Parameters (out keyword) ---");
      string fullName = "John Michael Smith";
      Console.WriteLine($"Original name: {fullName}");

      SplitName(fullName, out string firstName, out string lastName);
      Console.WriteLine($"First name: {firstName}");
      Console.WriteLine($"Last name: {lastName}");
      Console.WriteLine("'out' parameters don't need to be initialized before the call\n");

      // Input parameters with 'in' - for performance with large structs
      Console.WriteLine("--- Input Parameters (in keyword) ---");
      BigCalculationData bigData = new BigCalculationData(1000, "Performance Test Data");
      Console.WriteLine($"Original data description: {bigData.Description}");

      double sum = ProcessBigData(in bigData);
      Console.WriteLine($"Sum calculated: {sum:F2}");
      Console.WriteLine("'in' keyword prevents copying large structs - better performance!\n");
    }

    // Pass by value - method gets a copy
    static void ModifyByValue(int parameter)
    {
      Console.WriteLine($"  Inside ModifyByValue: received {parameter}");
      parameter = parameter * 2;
      Console.WriteLine($"  Inside ModifyByValue: modified to {parameter}");
      // Original variable remains unchanged
    }

    // Pass by reference - method works with original variable
    static void ModifyByRef(ref int parameter)
    {
      Console.WriteLine($"  Inside ModifyByRef: received {parameter}");
      parameter = parameter * 2;
      Console.WriteLine($"  Inside ModifyByRef: modified to {parameter}");
      // Original variable is changed
    }

    // Output parameters - method must assign values
    static void SplitName(string fullName, out string firstName, out string lastName)
    {
      int lastSpaceIndex = fullName.LastIndexOf(' ');
      if (lastSpaceIndex > 0)
      {
        firstName = fullName.Substring(0, lastSpaceIndex);
        lastName = fullName.Substring(lastSpaceIndex + 1);
      }
      else
      {
        firstName = fullName;
        lastName = "";
      }
    }

    // Input parameter - readonly reference for performance
    static double ProcessBigData(in BigCalculationData data)
    {
      Console.WriteLine($"  Processing: {data.Description}");
      Console.WriteLine($"  Array size: {data.Values?.Length ?? 0} elements");

      // We can read from the parameter but cannot modify it
      // data.Description = "Modified"; // This would cause a compiler error

      return data.Sum();
    }


  }
}