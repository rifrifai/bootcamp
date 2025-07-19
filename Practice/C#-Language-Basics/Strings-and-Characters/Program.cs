// See https://aka.ms/new-console-template for more information
namespace StringsAndCharacters
{
  class Program
  {
    static void Main()
    {
      Console.WriteLine("=== STRINGS AND CHARACTERS IN C# - COMPLETE GUIDE ===");

      // character fundamentals - single unicode characters
      CharacterBasics();

      // escape sequences - handling special characters
      EscapeSequences();
    }

    static void CharacterBasics()
    {
      Console.WriteLine("1. CHARACTER TYPE BASICS");
      Console.WriteLine("========================");

      // basic character declaration - always use single quotes
      char letter = 'A';
      char digit = '7';
      char symbol = '$';
      char space = ' ';

      Console.WriteLine($"Letter: '{letter}'");
      Console.WriteLine($"Digit: '{digit}'");
      Console.WriteLine($"Symbol: '{symbol}'");
      Console.WriteLine($"Space: '{space}' (invisible but there!)");

      // unicode characters - C# supports the full unicode range
      char heart = '♥';           // direct Unicode input
      char smiley = '☺';          // another Unicode symbol
      char chinese = '中';         // chinese character
      char arabic = 'ع';          // arabic letter

      Console.WriteLine($"Heart: {heart}");
      Console.WriteLine($"Smiley: {smiley}");
      Console.WriteLine($"Chinese: {chinese}");
      Console.WriteLine($"Arabic: {arabic}");

      // character storage details
      Console.WriteLine($"\nCharacter storage info:");
      Console.WriteLine($"Size of char: {sizeof(char)} bytes");
      Console.WriteLine($"Size of int: {sizeof(int)} bytes");
      Console.WriteLine($"Size of decimal: {sizeof(decimal)} bytes");
      Console.WriteLine($"Min char value: {(int)char.MinValue}");
      Console.WriteLine($"Max char value: {(int)char.MaxValue}");

      Console.WriteLine();
    }

    // Escape Sequences - mewakili karakter khusus
    // Ketika Anda membutuhkan karakter yang sulit untuk diketik atau memiliki makna khusus

    static void EscapeSequences()
    {
      Console.WriteLine("2. ESCAPE SEQUENCES");
      Console.WriteLine("===================");

      // Common escape sequences - memorize these, you'll use them often
      char singleQuote = '\'';    // Escape single quote inside char
      char doubleQuote = '\"';    // Escape double quote
      char backslash = '\\';      // Escape backslash itself
      char nullChar = '\0';       // Null character (string terminator in C)
      char newLine = '\n';        // New line (line break)
      char tab = '\t';            // Horizontal tab (indentation)

      Console.WriteLine("Common escape sequences:");
      Console.WriteLine($"Single quote: {singleQuote}");
      Console.WriteLine($"Double quote: {doubleQuote}");
      Console.WriteLine($"Backslash: {backslash}");
      Console.WriteLine($"Null character: '{nullChar}' (usually invisible)");
      Console.WriteLine($"After this comes a new line:{newLine}See? New line worked!");
      Console.WriteLine($"Tab example:{tab}This text is tabbed over");

      // unicode escape sequences - specify characters by their unicode code point
      // format: \uXXXX where XXXX is the 4-digit hex unicode value

      char copyrightSymbol = '\u00A9';  // © symbol
      char omegaSymbol = '\u03A9';      // Ω (Greek letter Omega)
      char euroSymbol = '\u20AC';       // € symbol
      char checkMark = '\u2713';        // ✓ symbol

      Console.WriteLine("\nUnicode escape sequences:");
      Console.WriteLine($"Copyright: {copyrightSymbol} (\\u00A9)");
      Console.WriteLine($"Omega: {omegaSymbol} (\\u03A9)");
      Console.WriteLine($"Euro: {euroSymbol} (\\u20AC)");
      Console.WriteLine($"Check mark: {checkMark} (\\u2713)");

      Console.WriteLine();
    }
  }
}