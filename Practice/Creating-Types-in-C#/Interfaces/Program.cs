namespace Interfaces
{
  /// <summary>
  /// Welcome to the complete C# Interfaces masterclass!
  /// 
  /// An interface is a contract that defines WHAT a type can do, not HOW it does it.
  /// Think of it like a job description - it lists the requirements,
  /// but doesn't tell you exactly how to fulfill them.
  /// 
  /// We'll cover everything from the basics to the most advanced features.
  /// </summary>

  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
      Console.WriteLine("║               C# INTERFACES MASTERCLASS                     ║");
      Console.WriteLine("║          From Basic Contracts to Advanced Features          ║");
      Console.WriteLine("╚══════════════════════════════════════════════════════════════╝\n");

      // Let's walk through each concept step by step
      BasicConcepts();
    }

    static void SectionHeader(string title)
    {
      Console.WriteLine($"\n┌─ {title} ".PadRight(65, '─') + "┐");
    }

    static void SectionFooter()
    {
      Console.WriteLine("└".PadRight(65, '─') + "┘\n");
    }

    /// <summary>
    /// 1. Basic Interface Concepts
    /// Understanding what interfaces are and how they work
    /// </summary>
    static void BasicConcepts()
    {
      SectionHeader("1. BASIC INTERFACE CONCEPTS");

      Console.WriteLine("An interface is a contract. Let's see this with IEnumerator:");

      // using our Countdown class that implements IEnumeratorDemo
      IEnumeratorDemo countdown = new Countdown();

      Console.Write("Countdown: ");
      while (countdown.MoveNext())
      {
        Console.Write(countdown.Current + " ");
      }
      Console.WriteLine("\n");

      Console.WriteLine("Key points:");
      Console.WriteLine("✓ Interface defines WHAT to do, not HOW");
      Console.WriteLine("✓ Classes provide the actual implementation");
      Console.WriteLine("✓ You can treat objects as their interface type (polymorphism)");

      // Multiple interfaces example
      Console.WriteLine("\nMultiple Interfaces Demo:");
      var phone = new SmartDevice("iPhone 15");

      // Use as communication device
      ICommunicationDevice comm = phone;
      comm.SendMessage("Hello Interface World!");

      // Use as entertainment device
      IEntertainmentDevice entertainment = phone;
      entertainment.PlayMusic("Bohemian Rhapsody");

      Console.WriteLine("\n✓ One class can implement multiple interfaces");
      Console.WriteLine("✓ This is impossible with class inheritance (single inheritance rule)");

      SectionFooter();
    }
  }
}