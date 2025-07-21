namespace LogicExercise
{
  class Program
  {
    static void Main()
    {
      Console.WriteLine("Enter a number: ");
      string? hasil = Console.ReadLine();
      if (int.TryParse(hasil, out int nums) && nums > 0)
      {
        for (int i = 1; i <= nums; i++)
        {
          if (i % 3 == 0 && i % 5 == 0)
          {
            if (i % 3 == 0 && i % 5 == 0 && i % 7 == 0)
            {
              Console.Write("foobarjaz, ");
              continue;
            }
            Console.Write("foobar");
          }
          else if (i % 3 == 0)
          {
            if (i % 3 == 0 && i % 7 == 0)
            {
              Console.Write("foojaz, ");
              continue;
            }
            Console.Write("foo");
          }
          else if (i % 5 == 0)
          {
            if (i % 5 == 0 && i % 7 == 0)
            {
              Console.Write("barjaz, ");
              continue;
            }
            Console.Write("bar");
          }
          else
          {
            Console.Write(i);
          }
          if (nums > 0)
          {
            Console.Write(", ");
          }
        }
      }
    }
  }
}