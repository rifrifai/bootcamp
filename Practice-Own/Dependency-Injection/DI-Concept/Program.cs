namespace DependencyInjectionConcept;

public interface IToolUser
{
  void SetHammer(Hammer hammer);
  void SetSaw(Saw saw);
  void SetElectricalDrill(ElectricDrill electricDrill);
}

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

public class Builder : IToolUser
{
  // it's called setter D I.
  // public Hammer Hammer { get; set; } = new Hammer();
  // public Saw Saw { get; set; } = new Saw();
  // public ElectricDrill ElectricDrill { get; set; } = new ElectricDrill();

  // it's called Contructor DI and interface DI
  private Hammer _hammer = new();
  private Saw _saw = new();
  private ElectricDrill _electricDrill = new();


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
    // constructor DI and interface DI
    _hammer.Use();
    _saw.Use();
    _electricDrill.Use();

    // setter DI
    // Hammer.Use();
    // Saw.Use();
    // ElectricDrill.Use();
  }

  // they are interface DI below
  public void SetElectricalDrill(ElectricDrill electricDrill)
  {
    _electricDrill = electricDrill;
  }

  public void SetHammer(Hammer hammer)
  {
    _hammer = hammer;
  }

  public void SetSaw(Saw saw)
  {
    _saw = saw;
  }
  // public void SetSaw(Saw saw) => _saw = saw;
}



internal class Program
{
  static void Main()
  {
    Hammer hammer = new();  // create the dependencies outside
    Saw saw = new();
    ElectricDrill electricDrill = new();
    Builder builder = new();

    // interface DI
    builder.SetHammer(hammer);
    builder.SetSaw(saw);
    builder.SetElectricalDrill(electricDrill);

    // setter DI
    // builder.Hammer = hammer;  // inject dependencies via setter
    // builder.Saw = saw;
    // builder.ElectricDrill = electricDrill;

    builder.BuildHouse();
  }
}