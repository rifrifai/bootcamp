// See https://aka.ms/new-console-template for more information
using System.Drawing;

class Murid
{
  // camelCase => attribute/variable
  // isMurid

  // PascalCase => function/ class
  // GetUser

  // variable/attribute
  public string? nama;
  public string? alamat;
  public bool isMurid;

  // function/behavior
  public void Learn()
  {
    Console.WriteLine("learn");
  }
  public void Hobby()
  {
    Console.WriteLine("hobby");
  }
}
class Program
{
  static void Main()
  {
    Murid rafi = new Murid();
    rafi.nama = "rafi";
    rafi.alamat = "salatiga";
    rafi.isMurid = true;
    Console.WriteLine(rafi.nama);
    Console.WriteLine(rafi.alamat);
    Console.WriteLine(rafi.isMurid);

    Murid ahmad = new Murid();
    ahmad.nama = "ahmad";
    ahmad.alamat = "kebumen";
    ahmad.isMurid = false;
    Console.WriteLine(ahmad.nama);
    Console.WriteLine(ahmad.alamat);
    Console.WriteLine(ahmad.isMurid);

    ahmad.Learn();
  }
}
