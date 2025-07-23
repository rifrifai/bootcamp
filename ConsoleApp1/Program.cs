namespace Delegate
{
  class Program
  {
    static void Main()
    {
      Console.WriteLine("*** We'll learn Delegate on my own from scratch ***");

      BasicDelegate();
      ParameterMethodDelegate();
      FibonacciDelegate();
    }

    public delegate int Operation(int num1, int num2);
    static void BasicDelegate()
    {
      static int Tambah(int a, int b) => a + b;
      static int Kali(int a, int b) => a * b;
      static int Bagi(int a, int b) => a / b;
      Console.WriteLine("\n==Basic Delegate==");

      Operation op;
      op = Tambah;
      Console.WriteLine($"menggunakan delegate hasil dari 5 + 5 = {op(5, 5)}");
      op = Kali;
      Console.WriteLine($"menggunakan delegate hasil dari 5 * 5 = {op(5, 5)}");
      op = Bagi;
      Console.WriteLine($"menggunakan delegate hasil dari 5 : 5 = {op(5, 5)} \n");
    }

    // parameter and method delegate
    public delegate bool Filter(int numbers);
    static void ShowNumbers(int[] numbers, Filter filter)
    {
      foreach (int num in numbers)
      {
        if (filter(num))
          Console.Write($"{num} ");
      }
    }
    static bool IsGanjil(int numbers) => numbers % 2 == 1;
    static bool IsGenap(int numbers) => numbers % 2 == 0;

    static void ParameterMethodDelegate()
    {
      Console.WriteLine("==Parameter Method Delegate==");
      int[] numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20];

      Console.Write("berikut filter angka genap menggunakan delegate: ");
      ShowNumbers(numbers, IsGenap);
      Console.Write("\nberikut filter angka ganjil menggunakan delegate: ");
      ShowNumbers(numbers, IsGanjil);
      Console.WriteLine();
    }

    // fibonacci delegate
    public delegate int FibonacciDel(int numbers);

    // calc fibonacci
    static int CalcFibo(int numbers)
    {
      if (numbers <= 1) return numbers;
      return CalcFibo(numbers - 1) + CalcFibo(numbers - 2);
    }

    // show fibonacci until n
    static void ShowFibo(int numbers, FibonacciDel fibonacciDel)
    {
      for (int i = 0; i < numbers; i++)
      {
        Console.Write($"{fibonacciDel(i)} ");
      }
      Console.WriteLine();
    }

    static void FibonacciDelegate()
    {
      Console.WriteLine("\n==Fibonacci Delegate==");
      // int.TryParse(Console.ReadLine(), out int result);

      Console.WriteLine("Deret fibonacci: ");
      ShowFibo(15, CalcFibo);

    }
  }
}