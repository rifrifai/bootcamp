// See https://aka.ms/new-console-template for more information
class Car
{
  // sifat public di akses disemua class
  // public int class
  // modifier private yang diakses hanya didalam kelas saja
  private int _price;
  public Car(int price)
  {
    this._price = price;
  }

  // function untuk menampilkan data dengan variable protected/ private
  public int CheckedVariable(string validasi)
  {
    if (validasi.Equals("password")) return _price;
    return 0;
  }

  // function override 
  public virtual void Greeting()
  {
    Console.WriteLine("kalau mau nge override, harus nge virtual dulu sebelumnya");
  }

  protected void SetPrice(int price)
  {
    if (!(price < 0))
    {
      this._price = price;
    }
  }
}

class CarAmphibi : Car
{
  public string? skill;
  public CarAmphibi(int _price, string skill) : base(_price)
  {
    this.skill = skill;
  }

  // modifier public access protected access private
  public void SetPricePublic(int price)
  {
    SetPrice(price);
  }

  // override
  public override void Greeting()
  {
    base.Greeting();
    Console.WriteLine("Siap, tercatat!");
  }
}

class Program
{
  static void Main()
  {
    Car car = new(1999);
    CarAmphibi carAmphibi = new(599, "menyelam");
    Console.WriteLine(carAmphibi.CheckedVariable("password"));

    // access protected
    carAmphibi.SetPricePublic(799);
    Console.WriteLine(carAmphibi.CheckedVariable("password"));

    // override
    carAmphibi.Greeting();
  }
}