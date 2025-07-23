Console.WriteLine("Enter a valid number!");
if (int.TryParse(Console.ReadLine(), out int result))
{
  for (int i = 1; i <= result; i++)
  {
    if (i % 5 == 0 && i % 3 == 0)
    {
      if (i % 7 == 0)
      {
        Console.Write("foobarjazz, ");
        continue;
      }
      Console.Write("foobar");
    }
    else if (i % 5 == 0)
    {
      if (i % 7 == 0)
      {
        Console.Write("foojazz, ");
        continue;
      }
      Console.Write("foo");
    }
    else if (i % 3 == 0)
    {
      if (i % 7 == 0)
      {
        Console.Write("barjazz, ");
        continue;
      }
      Console.Write("bar");
    }
    else
    {
      Console.Write(i);
    }
    if (i < result)
      Console.Write(", ");
  }
}
else
{
  Console.WriteLine("Invalid Number");
}