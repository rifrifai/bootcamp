namespace EventHandler
{
  // buat delegate (format event handler)
  public delegate void Notivy();
  public delegate void NotifikasiHandler();

  // buat publisher/broadcaster
  class Broadcaster
  {
    public event Notivy OnNotivy;   // event = aman, hanya bisa += dan -= dari luar

    public void Message()
    {
      Console.WriteLine("mengirim pesan");
      OnNotivy?.Invoke();   // panggil semua subs
    }
  }
  class Pintu
  {
    public event NotifikasiHandler Terbuka;
    public void Buka()
    {
      Console.WriteLine("pintu terbuka ...");
      Terbuka?.Invoke();
    }
  }

  class Program
  {
    // buat subscriber method
    static void DisplayAlert() => Console.WriteLine("notifikasi diterima!");

    static void TerdengarSuaraAlam() => Console.WriteLine("alarm berbunyi wiw wiw");

    static void Main()
    {
      Broadcaster b = new();
      b.OnNotivy += DisplayAlert;   //mendaftar ke event
      b.Message();    //output: mengirim pesan, notifikasi di terima

      Pintu p = new();
      p.Terbuka += TerdengarSuaraAlam;
      p.Buka();
    }
  }
}