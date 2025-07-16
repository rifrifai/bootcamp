// See https://aka.ms/new-console-template for more information
class Component
{
  public string? brand;
  public string? color;
  public int price;
  public Component(string brand, string color, int price)
  {
    this.brand = brand;
    this.color = color;
    this.price = price;
  }
}

class Bell : Component
{
  public int size;
  public string? model;
  public string? material;

  // below error because requirement from parent
  public Bell(int size, string model, string material)
  // bellow correct
  // public Bell(string brand, string color, int price, int size, string model, string material) : base(brand, color, size)
  {
    this.size = size;
    this.model = model;
    this.material = material;
  }
}