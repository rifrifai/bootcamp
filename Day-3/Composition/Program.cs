// See https://aka.ms/new-console-template for more information
using SantriComponents;
using Person;
class Program
{
  static void Main()
  {
    Biodata biodata = new("ahmad", "kebumen", "game", 30);
    Teacher teacher = new(2213, "mr mukhtarom", "quran-hadist");
    Santri santri = new(biodata, teacher);

    Console.WriteLine(santri.biodata.name);
    Console.WriteLine(santri.biodata.address);
    Console.WriteLine(santri.biodata.age);
    Console.WriteLine(santri.biodata.hobby);
    Console.WriteLine(santri.teacher.name);
    Console.WriteLine(santri.teacher.mapel);
  }
}
