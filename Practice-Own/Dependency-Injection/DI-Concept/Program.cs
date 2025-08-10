// interface and implementation
public interface ICoffeeMaker
{
  void Brew();
}

public class Espresso : ICoffeeMaker
{
  public void Brew()
  {
    Console.WriteLine("Membuat hot espresso...");
  }
}

public class Americano : ICoffeeMaker
{
  public void Brew()
  {
    Console.WriteLine("Membuat kopi americano...");
  }
}

public class MachineCoffee
{
  private readonly ICoffeeMaker? _coffeeMaker;

  // constructor DI
  public MachineCoffee(ICoffeeMaker coffeeMaker)
  {
    _coffeeMaker = coffeeMaker;
  }

  public void MakeCoffee()
  {
    _coffeeMaker!.Brew();
  }
}



class Program
{
  static void Main()
  {
    Espresso espresso = new();
    MachineCoffee machineCoffee = new(espresso);

    machineCoffee.MakeCoffee();

    Americano americano = new();
    MachineCoffee machineCoffee2 = new(americano);

    machineCoffee2.MakeCoffee();
  }
}