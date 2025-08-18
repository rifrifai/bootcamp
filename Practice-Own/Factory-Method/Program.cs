public interface ITransportation
{
    void Deliver(string destination);
}
public class Truck : ITransportation
{
    public void Deliver(string destination)
    {
        Console.WriteLine($"🚚 Delivering by truck to {destination}");
    }
}
public class Ship : ITransportation
{
    public void Deliver(string destination)
    {
        Console.WriteLine($"🚢 Delivering by ship to {destination}");
    }
}
public class Airplane : ITransportation
{
    public void Deliver(string destination)
    {
        Console.WriteLine($"✈️  Delivering by airplane to {destination}");
    }
}
public abstract class LogisticsFactory
{
    public abstract ITransportation CreateTransport();

    public void PlanDelivery(string destination)
    {
        var transport = CreateTransport();

        Console.WriteLine("📦 Planning delivery...");
        transport.Deliver(destination);
    }
}
public class TruckFactory : LogisticsFactory
{
    public override ITransportation CreateTransport()
    {
        return new Truck();
    }
}
public class shipFactory : LogisticsFactory
{
    public override ITransportation CreateTransport()
    {
        return new Ship();
    }
}
public class airplaneFactory : LogisticsFactory {
    public override ITransportation CreateTransport()
    {
        return new Airplane();
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== AFTER: With Factory Method Pattern ===");

        var truckFactory = new TruckFactory();
        truckFactory.PlanDelivery("Bandung");

        var shipFactory = new shipFactory();
        shipFactory.PlanDelivery("Aceh");

        var airplaneFactory = new airplaneFactory();
        airplaneFactory.PlanDelivery("Tokyo");

    }
}