// See https://aka.ms/new-console-template for more information
using static System.Console;

namespace Namespace
{
  class Program
  {
    static void Main(string[] args)
    {
      WriteLine("=== C# NAMESPACES MASTERCLASS ===");
      WriteLine("Your complete guide to organizing code like a professional\n");

      BasicNamespaces();
    }

    static void BasicNamespaces()
    {
      WriteLine("=== BASIC NAMESPACE CONCEPTS ===");
      WriteLine("Understanding the foundation of code organization\n");

      // working with classes from our custom namespaces
      WriteLine("--- Creating Objects from Different Namespaces ---");

      // fully qualified names - the explicit way
      var basicUser = new BasicTypes.User("Ahmad", "ahmad@mail.com");
      WriteLine($"Created user with fully qualified name: {basicUser.GetInfo()}");

      // using imported namespace (via using directive at top)
      var advancedUser = new AdvancedTypes.EnchancedUser("Tyas", "tyas@mail.com", "Premium");
      WriteLine($"Created enchanced user: {advancedUser.GetFullInfo()}");

      // demonstrating namespace hierarchy
      WriteLine($"\n--- Namespace Hierarchy ---");
      WriteLine($"BasicTypes.User lives in: BasicTypes namespace");
      WriteLine($"AdvancedTypes.EnchancedUser lives in: AdvancedTypes namespace");
      WriteLine($"Both are organized logically based on their complexity level");

      WriteLine("\nNamespaces prevent naming conflicts and provide logical organization\n");

    }


  }
  // basic namespace example
  namespace BasicTypes
  {
    public class User
    {
      public string Name { get; set; }
      public string Email { get; set; }
      public User(string name, string email)
      {
        Name = name;
        Email = email;
      }
      public string GetInfo() => $"{Name}, ({Email})";
    }

    public class Product
    {
      public string? Name { get; set; }
      public decimal Price { get; set; }
    }
  }

  // advanced namespace example
  namespace AdvancedTypes
  {
    public class EnchancedUser
    {
      public string Name { get; set; }
      public string Email { get; set; }
      public string MembershipLever { get; set; }
      public DateTime CreatedAt { get; set; }
      public EnchancedUser(string name, string email, string membershipLevel)
      {
        Name = name;
        Email = email;
        MembershipLever = membershipLevel;
        CreatedAt = DateTime.Now;
      }
      public string GetFullInfo() => $"{Name} ({Email}) - {MembershipLever} member since {CreatedAt: yyyy-MM-dd}";

    }
  }
}