// See https://aka.ms/new-console-template for more information

public class Property
{
  private int _price;
  public Property(int price)
  {
    _price = price;
  }
  public void Print()
  {
    Console.WriteLine($"the price is {_price}");
  }
}

class Program
{
  static void Main()
  {
    Property property = new(2001);
    property.Print();
  }
}