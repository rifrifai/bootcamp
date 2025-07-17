// See https://aka.ms/new-console-template for more information
// inheritance compotition
class Car
{
  public Car(Engine engine)
  {
    this.engine = engine;
  }
  public Engine engine;
}
class Engine
{
  public int size;
  public string brand;
}
class ElectricEngine : Engine { }
class DieselEngine : Engine { }
class PistonEngine : Engine { }
class Program
{
  static void Main()
  {
    Engine engine = new();
    Car car = new(engine);

    ElectricEngine electricEngine = new();
    Car car2 = new(electricEngine);
  }
}