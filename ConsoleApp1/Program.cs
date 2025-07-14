// See https://aka.ms/new-console-template for more information
// type basic
string pesan = "Tahun Lahir";
string finalPesan = pesan.ToUpper();
int tahunLahir = 2001;
string tahunLahirr = tahunLahir.ToString();
Console.WriteLine(finalPesan + " " + tahunLahirr);

int satuKM = 900;
bool isItAKM = satuKM >= 1000;
if (isItAKM)
{
  Console.WriteLine("Ini 1KM atau bahkan lebih");
}

// Division
// int num = 2 / 3;
//int b = 0;
//int c = 5 / b; //Throws DevideByZeroExpection at run time;
//int d = 5 / 0; //Compile-time error
// Console.WriteLine(c);

// Overflow
int num = int.MinValue;
num--;
Console.WriteLine(num == int.MaxValue);
int num2 = int.MaxValue;
num2++;
Console.WriteLine(num2 == int.MinValue);