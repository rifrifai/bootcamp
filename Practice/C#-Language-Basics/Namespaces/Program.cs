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
      NestedNamespaces();
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

    static void NestedNamespaces()
    {
      WriteLine("=== NESTED NAMESPACES ===");
      WriteLine("Creating hierarchical code organization\n");

      WriteLine("--- Multi-Level Namespace Structure ----");

      // working with deeply nested namespace
      var dbUser = new Company.Data.Models.DatabaseUser(1, "Bagus");
      var apiUser = new Company.Api.Controllers.UserController();
      var webComponent = new Company.Web.UI.Components.UserCard();

      WriteLine($"Database user: {dbUser.GetInfo()}");
      WriteLine($"API controller: {apiUser.GetControllerInfo()}");
      WriteLine($"Web component: {webComponent.GetComponentInfo()}");

      WriteLine($"\n--- Namespace Hierarchy Explanation---");
      WriteLine("Company.Data.Models.DatabaseUser");
      WriteLine("  └── Company (root namespace)");
      WriteLine("      └── Data (data layer)");
      WriteLine("          └── Models (data models)");
      WriteLine("              └── DatabaseUser (spesific class)");

      WriteLine("\n--- Benefits of Nested Organization ----");
      WriteLine("• Pemisahan kepentingan yang jelas");
      WriteLine("• Mudah untuk menemukan fungsionalitas yang terhubung");
      WriteLine("• Mencegah konflik penamaan antar lapisan");
      WriteLine("• Mencerminkan arsitektur proyek anda");

      WriteLine("\nNested namespace reflect your application's logical structure\n");
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

  // nested namespace example - company structure
  namespace Company
  {
    namespace Data
    {
      namespace Models
      {
        public class DatabaseUser
        {
          public int Id { get; set; }
          public string? Username { get; set; }
          public DatabaseUser(int id, string username)
          {
            Id = id;
            Username = username;
          }
          public string GetInfo() => $"DB User: {Username} (ID: {Id})";
        }
      }

      namespace Repositories
      {
        public class OrderRepository
        {
          public string GetRepositoryInfo() => $"Order repository - handles order data persistance";
        }
      }
    }

    namespace Api
    {
      namespace Controllers
      {
        public class UserController
        {
          public string GetControllerInfo() => $"User API Controller - handles HTTP Request";
        }
      }
    }

    namespace Web
    {
      namespace UI
      {
        namespace Components
        {
          public class UserCard
          {
            public string GetComponentInfo() => "User Card UI Component - displays user information";
          }
        }
      }

      namespace Models
      {
        public class OrderViewModel
        {
          public string GetModelInfo() => $"Order View Model - shapes data for web presentation";
        }
      }
    }

    namespace Services
    {
      namespace Business
      {
        public class OrderService
        {
          public string GetServiceInfo() => "Order Business Service - implements order processing logic";
        }
      }
    }
  }
}