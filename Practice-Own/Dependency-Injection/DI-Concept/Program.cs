namespace DependencyInjectionConcept;

public class Hammer
{
  public void Use()
  {
    Console.WriteLine("Hammering nails!");
  }
}

public class Saw
{
  public void Use()
  {
    Console.WriteLine("Sawing woods!");
  }
}

public class ElectricDrill
{
  public void Use()
  {
    Console.WriteLine("Drilling the wall!");
  }
}

public class Builder
{
  // it's called setter D I.
  public Hammer Hammer { get; set; } = new Hammer();
  public Saw Saw { get; set; } = new Saw();
  public ElectricDrill ElectricDrill { get; set; } = new ElectricDrill();

  // private Hammer _hammer;
  // private Saw _saw;
  // private ElectricDrill _electricDrill;


  // it's called Contructor DI
  // public Builder(Hammer hammer, Saw saw, ElectricDrill electricDrill)
  // {
  //   _hammer = hammer;
  //   _saw = saw;
  //   _electricDrill = electricDrill;
  // }

  // public Builder()
  // {
  // it's called dependencies bellow
  //   _hammer = new Hammer();
  //   _saw = new Saw();
  // }

  public void BuildHouse()
  {
    // constructor DI
    // _hammer.Use();
    // _saw.Use();
    // _electricDrill.Use();

    // setter DI
    Hammer.Use();
    Saw.Use();
    ElectricDrill.Use();
  }
}



internal class Program
{
  static void Main()
  {
    Hammer hammer = new();  // create the dependencies outside
    ElectricHammer electricHammer = new();
    Saw saw = new();
    ElectricDrill electricDrill = new();
    Builder builder = new();

    // setter DI
    builder.Hammer = hammer;  // inject dependencies via setter
    builder.Saw = saw;
    builder.ElectricDrill = electricDrill;

    builder.BuildHouse();
  }
}