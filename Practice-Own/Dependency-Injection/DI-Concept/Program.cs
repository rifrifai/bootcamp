// interface and implementation
public interface IAnimal
{
  void MakeNoise();
}
public class Dog : IAnimal
{
  public void MakeNoise()
  {
    Console.WriteLine("Dog Barks!");
  }
}

public class Cat : IAnimal
{
  public void MakeNoise()
  {
    Console.WriteLine("Cat meoww!");
  }
}

public class Animal
{
  private IAnimal? _animal;
  // constructor DI
  public Animal(IAnimal animal)
  {
    _animal = animal;
  }

  public void AnimalNoise()
  {
    _animal!.MakeNoise();
  }

}



class Program
{
  static void Main()
  {
    Dog dog = new();
    Animal animal = new(dog);

    Cat cat = new();
    Animal animal2 = new(cat);

    animal.AnimalNoise();
    animal2.AnimalNoise();

    // Espresso espresso = new();
    // MachineCoffee machineCoffee = new(espresso);

    // machineCoffee.MakeCoffee();

    // Americano americano = new();
    // MachineCoffee machineCoffee2 = new(americano);

    // machineCoffee2.MakeCoffee();
  }
}