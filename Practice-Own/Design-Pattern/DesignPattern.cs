// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// Design Pattern Example: Factory Method Pattern
// Product Interface and Concrete Products
interface ITransport
{
    void Deliver();
}
public class Truck : ITransport
{
    public void Deliver()
    {
        Console.WriteLine("Deliver by land in a truck");
    }
}
public class Ship : ITransport
{
    public void Deliver()
    {
        Console.WriteLine("Deliver by sea in a ship");
    }
}

// Creator Class and Concrete Creators
public abstract class Logistics
{
    public abstract ITransport CrateTransport();

    public void PlanDelivery()
    {
        console.WriteLine("Planning delivery...");

        ITransport transport = CrateTransport();
        transport.Deliver();

        Console.WriteLine("Delivery Completed!.");
    }
}

public class RoadLogistics : Logistics
{
    public override ITransport CrateTransport()
    {
        return new Truck();
    }
}

public class SeaLogistics : Logistics
{
    public override ITransport CrateTransport()
    {
        return new Ship();
    }
}

// Client Code
class Program
{
    static void Main()
    {
        Console.WriteLine("=== Factory Method Pattern Demo ===");

        Logistics logistics;

        string transportMethod = GetTransportMethodFromUser();

        switch (transportMethod.ToLower())
        {
            case "road":
                logistics = new RoadLogistics();
                break;
            case "sea":
                logistics = new SeaLogistics();
                break;
            default:
                Console.WriteLine("Invalid transport method. Defaulting to Road Logistics.");
                logistics = new RoadLogistics();
                break;
        }
        
        static string GetTransportMethodFromUser()
        {
            Console.WriteLine("Enter transport method (road/sea): ");
            return Console.ReadLine();
        }
    }
} 
