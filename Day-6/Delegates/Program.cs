namespace Delegates
{

  // 1. membuat delegate
  delegate int Transformers(int x);
  delegate int MyDelegate(int my);
  delegate int MathDelegate(int y);

  class Calculator
  {
    public int Multiply3(int x) => x * 3;
  }

  class Program
  {
    // 2. membuat fungsi/method
    static int Square(int x) => x * x;
    static int Cube(int x) => x * x * x;

    static int DoubleIt(int my) => my * my;

    // delegate sebagai parameter
    static void Transformer(int[] values, Func<int, int> func)
    {
      for (int i = 0; i < values.Length; i++)
      {
        values[i] = func(values[i]);
      }
    }
    static void Apply(int[] data, Func<int, int> func)
    {
      for (int i = 0; i < data.Length; i++)
      {
        data[i] = func(data[i]);
      }
    }
    static void Greet() => Console.WriteLine("Hello");
    static void Bye() => Console.WriteLine("Bye");

    // func n action
    static int Times4(int f) => f * 4;
    static void PrintPesan(string pesan) => Console.WriteLine(pesan);

    static void Main()
    {
      Transformers t = Square;
      Transformers c = Cube;
      Console.WriteLine($"hasil square = {t(3)}");
      Console.WriteLine($"hasil cube = {c(3)}");

      MyDelegate my = DoubleIt;
      Console.WriteLine($"Double myDelegate is {my(5)}");

      // delegate sebagai parameter
      int[] nums = { 1, 2, 3 };
      Transformer(nums, Square);

      int[] numbers = { 1, 2, 3 };
      Apply(numbers, Cube);

      Console.WriteLine("delegate sebagai parameter,  hasilnya = ");
      foreach (int i in nums)
      {
        Console.WriteLine(i);
      }
      foreach (int i in numbers)
      {
        Console.WriteLine(i);
      }

      // multicast delegate
      Action greetings = Greet;
      greetings += Bye;
      greetings();

      // delegate instance method
      Calculator calc = new();
      MathDelegate d = calc.Multiply3;
      Console.WriteLine($"delegate instance method result is {d(5)}");

      // func n action
      Func<int, int> f = Times4;
      Console.WriteLine($"func, result 8 * 4 = {f(8)}");

      Action<string> a = PrintPesan;
      a("action, selamat belajar n never give up!");
    }
  }
}