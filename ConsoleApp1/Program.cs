// See https://aka.ms/new-console-template for more information
// Task 1
Console.Write("Masukkan no : ");
string? answer = Console.ReadLine();
if (int.TryParse(answer, out int num) && num > 0)
{
  for (int i = 1; i <= num; i++)
  {
    if (i % 3 == 0 && i % 5 == 0)
    {
      Console.Write("foobar");
    }
    else if (i % 3 == 0)
    {
      Console.Write("foo");
    }
    else if (i % 5 == 0)
    {
      Console.Write("bar");
    }
    else
    {
      Console.Write(i);
    }
    if (i < num) Console.Write(",");
  }
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

