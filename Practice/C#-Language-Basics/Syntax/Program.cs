// See https://aka.ms/new-console-template for more information
namespace Syntax
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Console.WriteLine("============================================================================");
      Console.WriteLine("C# SYNTAX FUNDAMENTALS DEMONSTRATION");
      Console.WriteLine("============================================================================");
      Console.WriteLine();

      // Demonstrate each syntax category with clear explanations
      RealWorldExample();
    }

    private static void RealWorldExample()
    {
      Console.WriteLine("7. REAL-WORLD EXAMPLE: STUDENT GRADE CALCULATOR");
      Console.WriteLine("===============================================");
      Console.WriteLine();

      // REALISTIC SCENARIO - STUDENT GRADE SYSTEM

      // Identifiers using proper naming conventions
      string studentName = "Iskandar Dzulqarnain";        // camelCase local variable
      int studentId = 112212;                       // camelCase local variable

      // Array literal with punctuators
      double[] examScores = { 85.5, 92.0, 78.5, 95.0, 88.0 };  // Array initialization

      // Operators for calculations
      double totalScore = 0.0;                     // Assignment operator

      // Loop with various punctuators and operators
      for (int i = 0; i < examScores.Length; i++)  // Semicolons, comparison, increment
      {
        totalScore += examScores[i];             // Compound assignment operator
        Console.WriteLine($"Exam {i + 1}: {examScores[i]:F1} points");  // String interpolation
      }

      // More calculations with operators
      double averageScore = totalScore / examScores.Length;  // Division operator

      // Conditional logic with comparison operators
      string letterGrade;                          // Declaration
      if (averageScore >= 90.0)                    // Comparison operator
      {
        letterGrade = "A";                       // Assignment in block
      }
      else if (averageScore >= 80.0)               // else if keywords
      {
        letterGrade = "B";
      }
      else if (averageScore >= 70.0)
      {
        letterGrade = "C";
      }
      else if (averageScore >= 60.0)
      {
        letterGrade = "D";
      }
      else
      {
        letterGrade = "F";
      }

      // Ternary operator example
      string status = averageScore >= 75.0 ? "PASS" : "FAIL";  // Conditional operator

      // Member access operator with method calls
      Console.WriteLine($"\n--- Student Report ---");
      Console.WriteLine($"Student: {studentName.ToUpper()}");     // Member access + method call
      Console.WriteLine($"ID: {studentId}");
      Console.WriteLine($"Total Points: {totalScore:F1}");
      Console.WriteLine($"Average Score: {averageScore:F1}%");
      Console.WriteLine($"Letter Grade: {letterGrade}");
      Console.WriteLine($"Status: {status}");

      // Logical operators in complex condition
      bool isHonorRoll = averageScore >= 85.0 && letterGrade != "F";  // AND operator
      bool needsHelp = averageScore < 70.0 || letterGrade == "F";     // OR operator

      Console.WriteLine($"Honor Roll: {isHonorRoll}");
      Console.WriteLine($"Needs Academic Support: {needsHelp}");

      // Creating and using custom class instance
      var gradeCalculator = new SimpleCalculator();
      int bonusPoints = gradeCalculator.AddNumbers(5, 3);  // Method call with parentheses
      Console.WriteLine($"Bonus points available: {bonusPoints}");

      Console.WriteLine();
    }
  }

  public class SimpleCalculator()
  {
    // Private field using camelCase with underscore prefix
    private int _operationCount;

    /// <summary>
    /// Public property using PascalCase naming convention
    /// </summary>
    public int OperationCount
    {
      get { return _operationCount; } // getter with return keyword
      private set { _operationCount = value; } // private setter with value keyword
    }

    /// <summary>
    /// Adds two numbers and increments operation counter.
    /// Demonstrates method syntax with parameters and return value.
    /// </summary>
    /// <param name="first">First number to add</param>
    /// <param name="second">Second number to add</param>
    /// <returns>Sum of the two numbers</returns>
    public int AddNumbers(int first, int second)   //method with parameters
    {
      _operationCount++;
      return first + second;   // return statements with expression
    }
  }
}