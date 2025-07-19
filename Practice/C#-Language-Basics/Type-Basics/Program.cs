// ============================================================================
// COMPREHENSIVE C# TYPE BASICS DEMONSTRATION
// ============================================================================
// This project demonstrates all fundamental C# type concepts including:
// - Predefined types (int, string, bool) and their characteristics
// - Custom types (classes and structs) with constructors and members
// - Instance vs Static members and their different behaviors
// - Value types vs Reference types and memory implications
// - Type conversions (implicit and explicit)
// - Constructors and object instantiation
// - Null references and their behavior
// - Storage overhead and type taxonomy
// - Program entry points (Main method and top-level statements)
// - Namespace organization and type visibility
// ============================================================================

using System.Security.AccessControl;

namespace TypeBasics
{
  /// <summary>
  /// Main program class demonstrating C# type system fundamentals.
  /// This class shows how types work as blueprints for values.
  /// </summary>
  public class Program
  {
    // MAIN ENTRY POINT
    static void Main()
    {
      Console.WriteLine("============================================================================");
      Console.WriteLine("C# TYPE BASICS COMPREHENSIVE DEMONSTRATION");
      Console.WriteLine("============================================================================");
      Console.WriteLine();

      PredefinedTypes();
      CustomTypes();
      InstanceVsStaticMembers();
      ValueVsReferenceTypes();
    }
    static void PredefinedTypes()
    {
      Console.WriteLine("1. PREDEFINED TYPES (BUILT-IN TYPES)");
      Console.WriteLine("====================================");
      Console.WriteLine();
    }
    static void CustomTypes()
    {
      Console.WriteLine("2. CUSTOM TYPES (USER-DEFINED)");
      Console.WriteLine("==============================");
      Console.WriteLine();

      // UNITCONVERTER CLASS DEMONSTRATION

      Console.WriteLine("--- UnitConverter Class ---");

      // Creating instances with different conversion ratios
      UnitConverter feetToInchesConverter = new(12);
      UnitConverter milesToFeetConverter = new(5280);

      Console.WriteLine($"Converting 30 feet to inches: {feetToInchesConverter.Convert(30)}");
      Console.WriteLine($"Converting 100 feet to inches: {feetToInchesConverter.Convert(100)}");

      // Chaining conversions - demonstrates object interaction
      int oneMileInInches = feetToInchesConverter.Convert(milesToFeetConverter.Convert(1));
      Console.WriteLine($"1 mile in inches: {oneMileInInches}");

      // CUSTOM TYPE SYMMETRY WITH PREDEFINED TYPES

      Console.WriteLine("\n--- Symmetry Between Predefined and Custom Types ---");

      // Both predefined and custom types have data and functions
      Console.WriteLine("Predefined int type:");
      int number = 42;  // Data: 32 bits
      string numberAsString = number.ToString();  // Function: ToString()
      Console.WriteLine($"  Data: {number}, Function result: '{numberAsString}'");

      Console.WriteLine("Custom UnitConverter type:");
      UnitConverter converter = new UnitConverter(100);  // Data: ratio field
      int converted = converter.Convert(5);  // Function: Convert method
      Console.WriteLine($"  Data: ratio=100, Function result: {converted}");
      Console.WriteLine();
    }

    static void InstanceVsStaticMembers()
    {
      Console.WriteLine("3. INSTANCE VS STATIC MEMBERS");
      Console.WriteLine("=============================");
      Console.WriteLine();

      // INSTANCE MEMBERS - belong to spesific objects

      Console.WriteLine("--- Instance Members ---");

      // each panda object has its own name (instance member)
      Panda p1 = new("Po");
      Panda p2 = new("Pah");

      // instance members accessed through object references
      Console.WriteLine($"Panda 1 name: {p1.Name}");  // instance field access
      Console.WriteLine($"Panda 2 name: {p2.Name}"); // instance field access

      // instance method called on specific objects
      p1.DisplayInfo();   // instance method call
      p2.DisplayInfo();

      // STATIC MEMBERS - belong to the type itself

      Console.WriteLine("\n--- Static Members ---");

      // static members accessed through type name, not object instances
      Console.WriteLine($"Total panda population: {Panda.Population}");   //static field access

      // static method example (Console.WriteLine is static)
      Console.WriteLine("Console.WriteLine is a static method - called on Console type, not instance");

      // demonstrating why you can't access static members through instances
      // this would cause a compile error: p1.Population
      Console.WriteLine("Note: You cannot access static members through instance variables");
      Console.WriteLine();
    }

    static void ValueVsReferenceTypes()
    {
      Console.WriteLine("4. VALUE TYPES VS REFERENCE TYPES");
      Console.WriteLine("=================================");
      Console.WriteLine();

      // VALUE TYPES - store data directly

      Console.WriteLine("--- Value Types (struct) ---");

      // value type assignment copies the entire instance
      Point p1 = new();
      p1.X = 20;
      Point p2 = p1;    // copies all data from p1 to p2

      Console.WriteLine($"After assignment - p1.X: {p1.X}, p2.X: {p2.X}");

      // modifying one doesn't affect the other (independent storage)
      p1.X = 9;
      Console.WriteLine($"After p1.X change - p1.X: {p1.X}, p2.X: {p2.X}");
      Console.WriteLine($"Value types have independent storage locations");

      // REFERENCE TYPES - store reference to objects //

      Console.WriteLine("\n--- Reference Types (class) ---");

      // Reference type assignment copies the reference, not the object
      PointClass pc1 = new();
      pc1.X = 7;
      PointClass pc2 = pc1;  // Copies reference to same object

      Console.WriteLine($"After assignment - pc1.X: {pc1.X}, pc2.X: {pc2.X}");

      // Modifying through one reference affects the other (same object)
      pc1.X = 9;
      Console.WriteLine($"After pc1.X change - pc1.X: {pc1.X}, pc2.X: {pc2.X}");
      Console.WriteLine("Reference types share the same object in memory");

      // MEMORY BEHAVIOR COMPARISON//

      Console.WriteLine("\n--- Memory Behavior Summary ---");
      Console.WriteLine("Value types: Assignment copies data (independent objects)");
      Console.WriteLine("Reference types: Assignment copies reference (shared objects)");
      Console.WriteLine("Value types stored on stack, reference types on heap");

      Console.WriteLine();
    }

    public class UnitConverter
    {
      // Private field - encapsulates internal state
      int ratio;

      /// Constructor - initializes object state
      public UnitConverter(int unitRatio)
      {
        ratio = unitRatio;
      }

      /// Instance method - operates on specific object instance
      public int Convert(int unit) => unit * ratio;

    }

    // Panda class demonstrating instance vs static members
    public class Panda
    {
      // instance field
      public string Name;

      // static field
      public static int Population;

      // constructor - initializes new panda and updates pupulation
      public Panda(string n)
      {
        Name = n;           // set instance field
        Population++;       // increment static field
      }

      // instance method - displays info for this spesific panda
      public void DisplayInfo()
      {
        Console.WriteLine($"Panda: {Name}, Total Population: {Population}");
      }
    }

    // point struct - demonstrate value type behavior
    public struct Point
    {
      public int X, Y;
      public Point(int x, int y)
      {
        X = x;
        Y = y;
      }
      public void DisplayPoint()
      {
        Console.WriteLine($"Point: ({X}, {Y})");
      }
    }

    // point class - demonstrate reference type behavior
    public class PointClass
    {
      public int X, Y;
      public PointClass()
      {
        X = 0;
        Y = 0;
      }
      public PointClass(int x, int y)
      {
        X = x;
        Y = y;
      }
      public void DisplayInfo()
      {
        Console.WriteLine($"Point Class: ({X}, {Y})");
      }
    }
  }
}