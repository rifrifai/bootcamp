using SOLID_and_KISS_Principles.BadExamples;

namespace SOLID_and_Kiss_Principles;

/// <summary>
/// Welcome to the SOLID & KISS Principles Demo!
/// This program shows you the difference between good and bad code design
/// 
/// We'll demonstrate each principle with real examples that you can run and see
/// Pay attention to the comments - they explain what's happening and why
/// </summary>
class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("🚀 SOLID & KISS Principles Demo");
    Console.WriteLine("=====================================");
    Console.WriteLine();

    // Let's see each principle in action
    DemonstrateSOLIDPrinciples();
  }

  static void DemonstrateSOLIDPrinciples()
  {
    Console.WriteLine("📚 SOLID PRINCIPLES DEMONSTRATION");
    Console.WriteLine("==================================");

    // 1. Single Responsibility Principle
    DemonstrateSRP();
  }

  static void DemonstrateSRP()
  {
    Console.WriteLine("📚 SINGLE RESPONSIBILITY PRINCIPLE DEMONSTRATION");
    Console.WriteLine("================================================");

    Console.WriteLine("\n🎯 1. SINGLE RESPONSIBILITY PRINCIPLE (SRP)");
    Console.WriteLine("Each class should have only ONE reason to change");
    Console.WriteLine("------------------------------------------------");

    Console.WriteLine("\n❌ BAD: One class doing everything");
    try
    {
      var badService = new BadUserService();
      badService.RegisterUser("john@example.com", "password123");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error: {ex.Message}");
    }
  }
}