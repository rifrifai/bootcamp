namespace SantriComponents;

public class Biodata
{
  public string? name;
  public string? address;
  public string? hobby;
  public int age;
  public Biodata(string name, string address, string hobby, int age)
  {
    this.name = name;
    this.address = address;
    this.hobby = hobby;
    this.age = age;
  }
}