﻿// logic exercise
Console.WriteLine("Enter a valid number please!");
int.TryParse(Console.ReadLine(), out int answer);
if (answer > 0)
{
  for (int i = 1; i <= answer; i++)
  {
    string hasil = (i % 3 == 0 ? "foo" : "") +
      (i % 5 == 0 ? "bar" : "") +
      (i % 7 == 0 ? "jazz" : "") +
      (i % 4 == 0 ? "baz" : "") +
      (i % 9 == 0 ? "huzz" : "");

    Console.Write(string.IsNullOrEmpty(hasil) ? i.ToString() : hasil);
    if (i < answer) Console.Write(", ");
  }
}
else
{
  Console.WriteLine("Invalid number!");
}