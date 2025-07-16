// See https://aka.ms/new-console-template for more information
class People
{
  public string? name;
  public string? address;
  public string? gender;
  public int age;
  public void Hobby() { }
  public void Job() { }
}
class Santri : People
{
  public int idSantri;
}
class Teacher : People
{
  public int idTeacher;
  public string? mapel;
  public void Tools() { }
}
class Cleaner : People
{
  public int idCleaner;
  public void Tools() { }
}
