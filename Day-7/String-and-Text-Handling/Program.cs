using System.Globalization;

namespace StringAndTextHandling
{
  class Program
  {
    static void Main()
    {
      char a = 'a';
      char b = '5';
      Console.WriteLine("== learning char ==");
      Console.WriteLine($"a to upperInvariant= {char.ToUpperInvariant(a)}");
      Console.WriteLine($"is 'a' a digit = {char.IsDigit(a)}");
      Console.WriteLine($"is '5' a digit = {char.IsDigit(b)}");

      Console.WriteLine("== learning string ==");
      string aa = "Joko";
      // avoid make a many string because string is immutable
      // using stringBuilder for efficiency memory and performence
      Console.WriteLine($"Hello {aa.ToUpper()}");

      Console.WriteLine("== null vs empty ==");
      string ss = null;
      string bb = "Parjono";
      // Console.WriteLine(ss.Length);   // null reference exception
      Console.WriteLine($"is 'null' null or empty = {string.IsNullOrEmpty(ss)}");
      Console.WriteLine($"is 'parjono' null or empty = {string.IsNullOrEmpty(bb)}");

      Console.WriteLine("== akses dan iterasi");
      string kata = "xyz";
      char x = kata[1];
      Console.WriteLine($"char dari string kata[1] adalah {x}");

      foreach (char i in kata)
      {
        Console.WriteLine(i);
      }

      Console.WriteLine("=== formatting and parsing ===");
      // formatting = ubah nilai ke string 
      int age = 40;
      string umur = age.ToString();
      Console.WriteLine($"formatting age 40 to string = \"{umur}\"");

      // parsing = ubah string ke nilai 
      int number = int.Parse("123");
      Console.WriteLine($"parsing string \"123\" to number = {number}");

      // parse vs tryparse
      bool isOk = int.TryParse("abc", out int result);   //isOk = false, result = 0
      Console.WriteLine($"parsing abc to int using tryparse = {isOk}, the result {result}");

      // format provider 
      NumberFormatInfo rupiah = new()
      {
        CurrencySymbol = "Rp"
      };
      Console.WriteLine(50000.ToString("C", rupiah));

      // Math
      double v = 3.5;
      Console.WriteLine($"pembulatan 3.5 menggunakan Math.Round = {Math.Round(v)}");
      Console.WriteLine($"pembulatan 3.5 menggunakan Convert.ToInt32 = {Convert.ToInt32(v)}");
      double n = 4.5;
      Console.WriteLine($"pembulatan 3.5 menggunakan Convert.ToInt32 = {Convert.ToInt32(n)}");
    }
  }
}