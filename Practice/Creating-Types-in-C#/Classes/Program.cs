namespace Classes
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("=== C# Classes: Building Blocks of Object-Oriented Programming ===\n");

      // Start with the fundamentals - what makes a class tick
      BasicClasses();

      // Fields - data containers
      Fields();

      // Constants - immutable values
      Constants();
    }

    /// <summary>
    /// Basic class creation and instantiation
    /// Shows us how classes work as blueprints for objects
    /// </summary>
    static void BasicClasses()
    {
      Console.WriteLine("1. Basic Classes - The Foundation:");

      // Create employee instances from our Employee class blueprint
      var employee = new Employee("Alice Johnson", 28);
      var manager = new Employee("Bob Smith", 35);

      Console.WriteLine($"  Employee: {employee.Name}, Age: {employee.Age}");
      Console.WriteLine($"  Manager: {manager.Name}, Age: {manager.Age}");

      // show how each object has its own date
      employee.CelebrateBirthday();
      Console.WriteLine($"  After birthday: {employee.Name} is now {employee.Age}");
      Console.WriteLine($"  Manager's age unchaged: {manager.Age}");

      Console.WriteLine($"✅ Each object has its own copy of instance data\n");
    }

    /// <summary>
    /// Field types and behaviors
    /// Covers instance fields, static fields, readonly, and initialization
    /// </summary>
    static void Fields()
    {
      Console.WriteLine("2. Fields - Data Containers:");

      // instance field - each object gets its own copy
      var octopus1 = new Octopus("Oscar");
      var octopus2 = new Octopus("Ollie");

      Console.WriteLine($"  Octopus 1: {octopus1.Name}, Age: {octopus1.Age}");
      Console.WriteLine($"  Octopus 2: {octopus2.Name}, Age: {octopus2.Age}");

      // static fields - shared across ALL instances
      Console.WriteLine($"  All octopuses have {Octopus.Legs} legs (static field)");
      Console.WriteLine($"  Total octopuses created: {Octopus.TotalCreated}");

      // Readonly fields
      Console.WriteLine($"  Octopus 1 ID (readonly): {octopus1.Id}");
      Console.WriteLine($"  Octopus 2 ID (readonly): {octopus2.Id}");

      Console.WriteLine("✅ Instance fields are per-object, static fields are per-type\n");
    }
    static void Constants() { }
  }
}