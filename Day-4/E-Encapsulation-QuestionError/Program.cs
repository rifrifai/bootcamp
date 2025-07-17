// See https://aka.ms/new-console-template for more information
// it'll errror
class Engine
{
  private void Run()
  {
    Console.WriteLine("Engine Run");
  }
}
class ElectricalEngine : Engine
{
  public void Off()
  {
    Console.WriteLine("Engine Off");
  }
}

class Program
{
  static void Main()
  {
    Engine engine = new();
    engine.Run();
    engine.Off();

    ElectricalEngine ee = new();
    ee.Run();
    ee.Off();
  }
}