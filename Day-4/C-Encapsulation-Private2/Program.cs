// See https://aka.ms/new-console-template for more information
// private2
public class Santri
{
  private int idSantri = 1100;
  public int CheckedId(string password)
  {
    if (password == "p@ssw0rd")
    {
      return idSantri;
    }
    return 0;
  }
  public void SetId(int idBaru)
  {
    if (!(idBaru < 0))
    {
      idSantri = idBaru;
    }
  }
}

class Program
{
  static void Main()
  {
    Santri santri = new();
    Console.WriteLine(santri.CheckedId("p@ssw0rd"));
    santri.SetId(2222);
    Console.WriteLine(santri.CheckedId("p@ssw0rd"));
  }
}