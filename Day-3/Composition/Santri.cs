using SantriComponents;

namespace Person;

public class Santri
{
  public Biodata biodata;
  public Teacher teacher;
  public Santri(Biodata biodata, Teacher teacher)
  {
    this.biodata = biodata;
    this.teacher = teacher;

  }
}