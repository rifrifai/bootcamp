// See https://aka.ms/new-console-template for more information
// parentChild constructor
class Component
{
  public string? brand;
  public string? color;
  public Component(string brand, string color)
  {
    this.brand = brand;
    this.color = color;
  }
}

class Bell : Component
{
  public int size;
  public Bell(int size, string brand, string color) : base(brand, color)
  {
    this.size = size;
  }
}

class Program
{
  static void Main()
  {
    Bell bell = new(30, "honda", "black");
    Console.WriteLine(bell.size);
    Console.WriteLine(bell.brand);
    Console.WriteLine(bell.color);
  }
}