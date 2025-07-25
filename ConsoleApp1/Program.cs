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
      DelegateStatistic();
      // DelegateFinal();
      FuncAction();
      Kalkulator();
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

      Console.WriteLine();

    }

    // delegate bertipe fungsi statistik
    public delegate int StatisticDelegate(int[] data);

    static int HitungRataRata(int[] data)
    {
      int total = 0;
      foreach (int i in data)
      {
        total += i;
      }
      return total / data.Length;
    }

    static int HitungMax(int[] data)
    {
      int max = data[0];
      foreach (int i in data)
      {
        if (i > max) max = i;
      }
      return max;
    }

    static int HitungMin(int[] data)
    {
      int min = data[0];
      foreach (int i in data)
      {
        if (i < min) min = i;
      }
      return min;
    }

    static int HitungJumlah(int[] data)
    {
      int total = 0;
      foreach (int i in data)
      {
        total += i;
      }
      return total;
    }

    static void ShowStatistic(string name, StatisticDelegate statisticDelegate, int[] data)
    {
      Console.WriteLine($"{name}: {statisticDelegate(data)}");
    }

    static void DelegateStatistic()
    {
      Console.WriteLine("\n=== Delegate Statistik ===");

      int[] angka = { 10, 20, 15, 5, 30 };

      ShowStatistic("Rata-rata", HitungRataRata, angka);
      ShowStatistic("Nilai Max", HitungMax, angka);
      ShowStatistic("Nilai Min", HitungMin, angka);
      ShowStatistic("Jumlah Total", HitungJumlah, angka);

    }

    // delegate final
    static void DelegateFinal()
    {
      List<double> numbers = new List<double>();
      string input;

      Console.WriteLine("=== Program Statistik Angka ===");
      Console.WriteLine("Masukkan angka satu per satu. Ketik 'selesai' untuk menghitung statistik.");

      // input data dari user
      while (true)
      {
        Console.WriteLine("Angka: ");
        input = Console.ReadLine();

        if (input.ToLower() == "selesai") break;

        if (double.TryParse(input, out double number))
        {
          numbers.Add(number);
        }
        else
        {
          Console.WriteLine("Invalid Input, Enter a number or 'Exit'");
        }
      }

      // check if there is no number
      if (numbers.Count == 0)
      {
        Console.WriteLine("There is no number inputed");
        return;
      }

      // delegate dalam bentuk angka
      Action<List<double>> ShowStatistic = data =>
      {
        Console.WriteLine("\n=== Statistic ===");
        Console.WriteLine($"Jumlah data: {data.Count}");
        Console.WriteLine($"Rata - rata: {data.Average()}");
        Console.WriteLine($"Nilai min: {data.Min()}");
        Console.WriteLine($"Nilai max: {data.Max()}");
        Console.WriteLine($"Median: {HitungMedian(data)}");


      };

      // show result
      ShowStatistic(numbers);
    }

    // fungsi untuk menghitung median
    static double HitungMedian(List<double> data)
    {
      var sorted = data.OrderBy(x => x).ToList();
      int count = sorted.Count;
      if (count % 2 == 1)
      {
        return sorted[count / 2];   //ganjil
      }
      else
      {
        return (sorted[count / 2 - 1] + sorted[count / 2]) / 2.0;   //genap
      }
    }

    // func and action pada delegate

    static void FuncAction()
    {
      Action<string> sapa = nama =>
      {
        Console.WriteLine($"Halo {nama}, Selamat belajar c#!");
      };
      sapa("Budi");
      Console.WriteLine();

      Action<int, int> tambah = (a, b) =>
      {
        Console.WriteLine($"Hasil: {a + b}");
      };
      tambah(10, 13);
      Console.WriteLine();

      Action<int, int> kurang = (a, b) =>
      {
        Console.WriteLine($"Hasil a - b = {a - b}");
      };
      kurang(10, 20);
      Console.WriteLine();

      Func<string, int> hitungHuruf = teks => teks.Length;
      int panjang = hitungHuruf("cristianoRonaldo");
      Console.WriteLine($"jumlah huruf pada 'cristianoRonaldo' = {panjang}");
    }

    static void Kalkulator()
    {
      // delegate for math operations
      Func<double, double, double> tambah = (a, b) => a + b;
      Func<double, double, double> kurang = (a, b) => a - b;
      Func<double, double, double> kali = (a, b) => a * b;
      Func<double, double, double> bagi = (a, b) =>
      {
        if (b == 0)
        {
          Console.WriteLine("tidak bisa dibagi dengan 0");
          return 0;
        }
        return a / b;
      };

      // action to print result
      Action<string> showHasil = hasil => Console.WriteLine($"hasil = {hasil}");

      while (true)
      {
        Console.WriteLine("\n=== Kalkulator Sederhana ===");
        Console.Write("Masukkan angka pertama: ");
        double angka1 = double.Parse(Console.ReadLine());

        Console.Write("Masukkan operator (+, *, /, -): ");
        string? op = Console.ReadLine();

        Console.Write("Masukkan angka kedua: ");
        double angka2 = double.Parse(Console.ReadLine());

        double hasil = 0;
        bool operasiValid = true;

        switch (op)
        {
          case "+":
            hasil = tambah(angka1, angka2);
            break;
          case "-":
            hasil = kurang(angka1, angka2);
            break;
          case "*":
            hasil = kali(angka1, angka2);
            break;
          case "/":
            hasil = bagi(angka1, angka2);
            break;
          default:
            Console.WriteLine("Invalid Operator");
            operasiValid = false;
            break;
        }

        if (operasiValid) showHasil(hasil.ToString());

        Console.Write("\nHitung lagi? (y/n): ");
        string ulang = Console.ReadLine().ToLower();
        if (ulang != "y") break;
      }
    }

  }
}