namespace SantriComponents;

public class Teacher
{
  public int idTeacher;
  public string? name;
  public string? mapel;
  public Teacher(int idTeacher, string name, string mapel)
  {
    this.idTeacher = idTeacher;
    this.name = name;
    this.mapel = mapel;
  }
}