namespace Inheritance
{
  // This is our base class - the foundation that other classes will build upon

  class Program
  {
    static void Main()
    {
      Console.WriteLine("=== COMPREHENSIVE C# INHERITANCE CONCEPTS DEMONSTRATION ===");
      Console.WriteLine("This program demonstrates all major inheritance concepts in C#");
      Console.WriteLine("Based on professional training material and real-world examples\n");

      try
      {
        // 1. Basic Inheritance - Foundation concepts
        Console.WriteLine("Press any key to start with BASIC INHERITANCE...");
        Console.ReadKey();
        Console.Clear();
        BasicInheritance.RunDemo();

        Console.WriteLine("\nPress any key to continue to POLYMORPHISM...");
        Console.ReadKey();
        Console.Clear();

        // 2. Polymorphism - Many forms, one interface
        PolymorphismClass.RunDemo();

      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occured during demonstration: {ex.Message}");
        Console.WriteLine("This might be due to missing dependencies or runtime issues.");
      }
    }


  }
}