﻿// See https://aka.ms/new-console-template for more information
// constructor
class Calculator
{
  // no constructor?
  // parameterless constructor
  public Calculator()
  {
    Console.WriteLine("Calculator instance created");
  }
}

class Program
{
  static void Main()
  {
    for (int i = 1; i <= 10; i++)
    {
      Calculator calculator = new();
    }
  }
}
