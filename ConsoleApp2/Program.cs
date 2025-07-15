// See https://aka.ms/new-console-template for more information
// latihan foobar
Console.WriteLine("Enter a valid number : ");
int answer = Convert.ToInt32(Console.ReadLine());
if (answer > 0)
{
  for (int i = 1; i <= answer; i++)
  {
    if (i % 3 == 0 && i % 5 == 0)
    {
      Console.Write("foobar");
    }
    else if (i % 3 == 0)
    {
      Console.Write("foo");
    }
    else if (i % 5 == 0)
    {
      Console.Write("bar");
    }
    else
    {
      Console.Write(i);
    }
    if (i < answer) Console.Write(",");
  }
}
else
{
  Console.WriteLine("Unvalid number");
}