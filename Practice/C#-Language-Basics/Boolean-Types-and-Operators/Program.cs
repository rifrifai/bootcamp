// See https://aka.ms/new-console-template for more information
using System.Collections;

namespace BooleanDemo
{
  // Class custom untuk menunjukkan perbandingan kesetaraan tipe referensi
  // Kelas ini membantu menggambarkan bagaimana operator == bekerja dengan tipe referensi
  public class Person
  {
    // public field untuk menyimpan nama person
    // Ini akan digunakan untuk menunjukkan bahwa objek dengan konten yang sama tetap merupakan instansi yang berbeda.
    public string Name;

    // constructor untuk menginisialisasi object Person dengan nama
    public Person(string name)
    {
      Name = name;
    }

    // Override metode ToString untuk memberikan representasi string yang bermakna
    // Ini memudahkan untuk menampilkan objek Person dalam output console
    public override string ToString() => $"Person: {Name}";
  }

  class Program
  {
    static void Main()
    {
      Console.WriteLine("=== C# BOOLEAN TYPES AND OPERATORS COMPREHENSIVE DEMONSTRATION ===\n");
      BooleanBasics();
    }
    static void BooleanBasics()
    {
      Console.WriteLine("1. BOOLEAN TYPE BASICS");
      // Variabel boolean hanya dapat menyimpan dua nilai: benar atau salah
      // Tipe bool adalah alias untuk System.Boolean
      // Meskipun hanya membutuhkan 1 bit, runtime menggunakan 1 byte untuk efisiensi

      bool isRaining = true;
      bool isStudent = false;

      Console.WriteLine($"isRaining: {isRaining}");
      Console.WriteLine($"isStudent: {isStudent}");

      if (isRaining)
      {
        Console.WriteLine("Take an Umbrella");
      }
      else
      {
        Console.WriteLine("No umbrella needed");
      }

      // Mendemonstrasikan optimasi memori dengan BitArray untuk beberapa nilai Boolean
      // BitArray menyimpan setiap Boolean sebagai satu bit, menghemat memori untuk koleksi besar
      Console.WriteLine("\nMemory optimization with BitArray: ");
      BitArray weatherConditions = new BitArray(8); //8 bit for different weather conditions
      weatherConditions[0] = true; //rainy
      weatherConditions[1] = false; //sunny
      weatherConditions[3] = true; //windy
      weatherConditions[4] = false; //snowy

      Console.WriteLine($"Weather conditions (BitArray): Rainy = {weatherConditions[0]}, " + $"Sunny = {weatherConditions[1]}, Windy = {weatherConditions[2]}, Snowy = {weatherConditions[3]}");
    }
  }
}