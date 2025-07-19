namespace VariablesAndParameters
{
  public struct BigCalculationData { }
  class Program
  {
    // Static field to demonstrate ref returns
    // In production code, be careful with global state like this
    private static string globalMessage = "Initial Global Value";
    static void Main(string[] args)
    {
      Console.WriteLine("=== VARIABLES AND PARAMETERS IN C# ===");
      Console.WriteLine("This demo covers everything from basic variables to advanced parameter techniques\n");

      // Section 1: Stack vs Heap Demonstration
      StackAndHeap();

      // Section 2: Definite Assignment Rules
      DefiniteAssignment();

    }

    static void StackAndHeap() { }
    static void DefiniteAssignment() { }
  }
}