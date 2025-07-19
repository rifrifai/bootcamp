// See https://aka.ms/new-console-template for more information
namespace Statements
{
  public class Player
  {
    public string Name { get; set; }
    public int Score { set; get; }
    public bool IsActive { get; set; }
    public Player(string name)
    {
      Name = name;
      Score = 0;
      IsActive = true;
    }
    public void AddScore(int points)
    {
      Score += points;
    }
    public void DisplayStatus()
    {
      Console.WriteLine($"Player: {Name}, Score: {Score}, Active: {IsActive}");
    }
  }

  public class GameSession
  {
    public List<Player> Players { get; set; }
    public int Round { get; set; }
    public GameSession()
    {
      Players = new List<Player>();
      Round = 1;
    }
    public void AddPlayer(string name)
    {
      Players.Add(new Player(name));
    }
    public void DisplayLeaderboard()
    {
      Console.WriteLine($"\n--- Round {Round} Leaderboard ---");
      foreach (var player in Players.OrderByDescending(p => p.Score))
      {
        player.DisplayStatus();
      }
    }
  }
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine($"=== C# STATEMENT MASTERCLASS ===");
      Console.WriteLine($"Building blocks of program control flow\n");

      DeclarationStatements();
    }

    static void DeclarationStatements()
    {
      Console.WriteLine("=== DECLARATION STATEMENTS ===");
      Console.WriteLine($"Creating variables and statements - the foundation of data storage\n");

      // basic variable declarations with initialization
      Console.WriteLine("--- Single Variable Declarations ---");
      string someWord = "rosebud";  // String variable with initial value
      int someNumber = 42;          // Integer variable with initial value
      double pi = 3.14159;          // Double precision floating point
      bool isLearning = true;       // Boolean variable

      Console.WriteLine($"someWord: '{someWord}'");
      Console.WriteLine($"someNumber: {someNumber}");
      Console.WriteLine($"pi: {pi}");
      Console.WriteLine($"isLearning: {isLearning}");

      // Multiple variable declarations of the same type
      Console.WriteLine("\n--- Multiple Variable Declarations ---");
      bool rich = true, famous = false;  // Two booleans in one statement
      int x = 10, y = 20, z = 30;       // Three integers in one statement

      Console.WriteLine($"rich: {rich}, famous: {famous}");
      Console.WriteLine($"x: {x}, y: {y}, z: {z}");

      // Constant declarations - values that never change
      Console.WriteLine("\n--- Constant Declarations ---");
      const double c = 2.99792458E08;  // Speed of light in m/s
      const string appName = "Statement Demo";
      const int maxRetries = 3;

      Console.WriteLine($"Speed of light: {c:E2} m/s");
      Console.WriteLine($"Application name: {appName}");
      Console.WriteLine($"Maximum retries allowed: {maxRetries}");

      // Demonstrating that constants cannot be modified
      Console.WriteLine("\n--- Constant Immutability ---");
      Console.WriteLine("Constants cannot be changed after declaration");
      Console.WriteLine("Uncommenting the next line would cause a compile error:");
      Console.WriteLine("// c += 10;  // ERROR: Cannot modify a constant value");

      // Variable scope demonstration
      Console.WriteLine("\n--- Variable Scope ---");
      int outerVariable = 100;
      Console.WriteLine($"Outer variable: {outerVariable}");

      {
        // This is a nested block - creates new scope
        int innerVariable = 200;
        Console.WriteLine($"Inner variable: {innerVariable}");
        Console.WriteLine($"Outer variable is still accessible: {outerVariable}");

        // This would cause an error if uncommented:
        // int outerVariable = 300;  // ERROR: Already defined in outer scope
      }

      // innerVariable is no longer accessible here
      Console.WriteLine("Inner variable is no longer accessible outside its block");
      Console.WriteLine("This demonstrates block-level scoping in C#\n");
    }
  }
}