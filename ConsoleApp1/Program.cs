// See https://aka.ms/new-console-template for more information
// Tugas
Console.Write("Enter a number: ");
string input = Console.ReadLine();
if (int.TryParse(input, out int n) && n > 0)
{
  for (int x = 1; x <= n; x++)
  {
    if (x % 3 == 0 && x % 5 == 0)
    {
      Console.Write("foobar");
    }
    else if (x % 3 == 0)
    {
      Console.Write("foo");
    }
    else if (x % 5 == 0)
    {
      Console.Write("bar");
    }
    else
    {
      Console.Write(x);
    }
    // tambah comma
    if (x < n) { Console.Write(","); }
  }
  Console.WriteLine();
}
else
{
  Console.WriteLine("Masukkan nomor yang valid");
}

// type basic
// string pesan = "Tahun Lahir";
// string finalPesan = pesan.ToUpper();
// int tahunLahir = 2001;
// string tahunLahirr = tahunLahir.ToString();
// Console.WriteLine(finalPesan + " " + tahunLahirr);

// int satuKM = 900;
// bool isItAKM = satuKM >= 1000;
// if (isItAKM)
// {
//   Console.WriteLine("Ini 1KM atau bahkan lebih");
// }

// Division
// int num = 2 / 3;
//int b = 0;
//int c = 5 / b; //Throws DevideByZeroExpection at run time;
//int d = 5 / 0; //Compile-time error
// Console.WriteLine(c);

// Overflow
// int num = int.MinValue;
// num--;
// Console.WriteLine(num == int.MaxValue);
// int num2 = int.MaxValue;
// num2++;
// Console.WriteLine(num2 == int.MinValue);

// int num3 = 1000000;
// int num4 = 1000000;
// Console.WriteLine(checked(num3 * num4));

// String Concatenation
// string x = "b" + 2;
// Console.WriteLine(x);

