// See https://aka.ms/new-console-template for more information
class Dog
{
  public bool isManja;
  public string? name;
  public string? gender;
  public void Name(string name)
  {
    Console.WriteLine($"Your dog's name is {name}");
  }

  public void Eat(string jenisMakanan)
  {
    Console.WriteLine($"Dog eat {jenisMakanan}");
  }
  public void JenisDog(string tipeDog)
  {
    Console.WriteLine($"This dog is {tipeDog}");
  }
}

class Program
{
  static void Main()
  {
    Dog jack = new Dog();
    Console.WriteLine("What's your dog's name?");
    string? name = Console.ReadLine();
    Console.WriteLine("What's your dog's food?");
    string? food = Console.ReadLine();
    Console.WriteLine("What's your dog's type?");
    string? jenisDog = Console.ReadLine();
    // jack.isManja = false;
    // jack.name = "jack";
    // jack.gender = "male";
    // Console.WriteLine(jack.isManja);
    // Console.WriteLine(jack.name);
    // Console.WriteLine(jack.gender);
    jack.Name(name ?? "Noname");
    jack.Eat(food ?? "Dog's fasting");
    jack.JenisDog(jenisDog ?? "Local");
  }

}