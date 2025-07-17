// See https://aka.ms/new-console-template for more information
// private
class Property
{
  private int price = 15;
  public string? name;
  public string? place;
  public void Start()
  {
    Console.WriteLine("It's start");
  }
  public void CheckedPrice()
  {
    Console.WriteLine(price);
  }
  public void AddPrice(int add)
  {
    price += add;
  }
}

class Program
{
  static void Main()
  {
    Property property = new();
    property.name = "scbd";
    property.place = "jakarta";
    // error because price is private
    // property.price = 300;
    property.AddPrice(30);
    property.CheckedPrice();
  }
}