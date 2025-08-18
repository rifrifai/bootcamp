public interface IProduct
{
    public string Name { get; }
    string Use();
}
public class ConcreteProductA : IProduct
{
    public string Name => "Concrete Product A";

    public string Use() => "Memproses data dengan Algoritma A";
}
public class ConcreteProductB : IProduct
{
    public string Name => "Concrete Product B";

    public string Use() => "Memproses data dengan Algoritma B";
}
public class ConcreteProductC : IProduct
{
    public string Name => "Concrete Product C";

    public string Use() => "Memproses data dengan Algoritma C";
}
public class ConcreteProductD : IProduct
{
    public string Name => "Concrete Product D";
    public string Use() => "Memproses data dengan Algoritma D";
}


public abstract class Creator
{
    public abstract IProduct CreateProduct();
    public string DoWork()
    {
        IProduct product = CreateProduct();
        var result = product.Use();
        return $"[{product.Name}, {result}]";
    }
}
public sealed class ConcreteCreatorA : Creator
{
    public override IProduct CreateProduct() => new ConcreteProductA();
}
public sealed class ConcreteCreatorB : Creator
{
    public override IProduct CreateProduct() => new ConcreteProductB();
}
public sealed class ConcreteCreatorC : Creator
{
    public override IProduct CreateProduct() => new ConcreteProductC();
}
public sealed class ConcreteCreatorD : Creator
{
    public override IProduct CreateProduct() => new ConcreteProductD();
}

class Program
{
    static void Main(string[] args)
    {
        // Creator creatorA = new ConcreteCreatorA();
        // creatorA.DoWork();

        // Creator creatorB = new ConcreteCreatorB();
        // creatorB.DoWork();

        // Creator creatorC = new ConcreteCreatorC();
        // creatorC.DoWork();


        // Contoh pemilihan creator runtime dari argumen/konfigurasi
        // Jalankan: dotnet run A  (atau B/C)
        // var code = (args.Length > 0 ? args[0] : "A").ToUpperInvariant();

        // Creator creator = code switch
        // {
        //     "A" => new ConcreteCreatorA(),
        //     "B" => new ConcreteCreatorB(),
        //     "C" => new ConcreteCreatorC(),
        //     _ => new ConcreteCreatorA()
        // };

        // var output = creator.DoWork();
        // Console.WriteLine(output);


        // Jalankan semua creator
        Creator[] creators = [
            new ConcreteCreatorA(),
            new ConcreteCreatorB(),
            new ConcreteCreatorC(),
            new ConcreteCreatorD()
        ];

        foreach (var creator in creators)
        {
            string result = creator.DoWork();
            Console.WriteLine(result);
        }
    }
}