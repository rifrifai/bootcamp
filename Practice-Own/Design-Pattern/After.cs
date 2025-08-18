using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AfterFactoryMethod;
    public interface ITransportation    // 1. Product Interface
    {
        void Deliver(string destination);
    }
    public class Truck : ITransportation    // 2. Concrete Products
    {
        public void Deliver(string destination)
        {
            Console.WriteLine($"🚛 Truck delivering to {destination} by road");
        }
    }
    public class Ship : ITransportation
    {
        public void Deliver(string destination)
        {
            Console.WriteLine($"🚢 Ship delivering to {destination} by sea");
        }
    }
    public abstract class LogisticsFactory      // 3. Abstract Creator (Factory)
    {
        public abstract ITransportation CreateTransportation();     // Factory Method
        public void PlanDelivery(string destination)    // Template method
        {
            var transport = CreateTransportation();     // Create the transport using factory method
            
            Console.WriteLine("Planning delivery...");      // Use the transport
            transport.Deliver(destination);
            Console.WriteLine("Delivery planned successfully!\n");
        }
    }
    public class TruckLogistics : LogisticsFactory       // 4. Concrete Creators (Factories)
    {
        public override ITransportation CreateTransportation()
        {
            return new Truck();
        }
    }
    public class ShipLogistics : LogisticsFactory
    {
        public override ITransportation CreateTransportation()
        {
            return new Ship();
        }
    }
    public class LogisticsManager   // 5. Client Code - Clean and Extensible
    {
        public void PlanDelivery(LogisticsFactory factory, string destination)
        {
            factory.PlanDelivery(destination);
            // ✅ Benefits:
            // - No tight coupling
            // - Easy to extend (just add new factory)
            // - Follows Open/Closed Principle
        }
    }
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("\n=== AFTER: Factory Method Pattern ===");

            var newManager = new LogisticsManager();

            // Using different factories
            var truckFactory = new TruckLogistics();
            var shipFactory = new ShipLogistics();
            var airFactory = new AirLogistics();

            newManager.PlanDelivery(truckFactory, "Bandung");
            newManager.PlanDelivery(shipFactory, "Aceh");
            newManager.PlanDelivery(airFactory, "Jakarta");

            Console.WriteLine("🎉 Demo completed!");
        }
    }

    
            
            












// public class Airplane : ITransportation
// {
//     public void Deliver(string destination)
//     {
//         Console.WriteLine($"✈️ Airplane delivering to {destination} by air");
//     }
// }

// public class AirLogistics : LogisticsFactory
// {
//     public override ITransportation CreateTransportation()
//     {
//         return new Airplane();
//     }
// }


 // Console.WriteLine("=== BEFORE: Tightly Coupled ===");

            // var oldManager = new BeforeFactoryMethod.LogisticsManager();
            // oldManager.PlanDelivery("truck", "New York");
            // oldManager.PlanDelivery("ship", "London");



    // // Product interface
    // public interface ITransportation
    // {
    //     void Deliver(string cargo, string destination);
    //     double GetMaxWeight();
    //     int GetEstimatedDays();
    //     string GetTransportMode();
    // }

    // // Concrete Products
    // public class Truck : ITransportation
    // {
    //     public void Deliver(string cargo, string destination)
    //     {
    //         Console.WriteLine($"🚛 Truck delivering {cargo} to {destination} by road");
    //         Console.WriteLine("   - Loading cargo into truck container");
    //         Console.WriteLine("   - Driving on highways and roads");
    //         Console.WriteLine($"   - Estimated delivery: {GetEstimatedDays()} days");
    //     }

    //     public double GetMaxWeight() => 10000; // 10 tons
    //     public int GetEstimatedDays() => 3;
    //     public string GetTransportMode() => "Road";
    // }

    // public class Ship : ITransportation
    // {
    //     public void Deliver(string cargo, string destination)
    //     {
    //         Console.WriteLine($"🚢 Ship delivering {cargo} to {destination} by sea");
    //         Console.WriteLine("   - Loading cargo into ship containers");
    //         Console.WriteLine("   - Sailing across oceans");
    //         Console.WriteLine($"   - Estimated delivery: {GetEstimatedDays()} days");
    //     }

    //     public double GetMaxWeight() => 50000; // 50 tons
    //     public int GetEstimatedDays() => 10;
    //     public string GetTransportMode() => "Sea";
    // }

    // public class Airplane : ITransportation
    // {
    //     public void Deliver(string cargo, string destination)
    //     {
    //         Console.WriteLine($"✈️ Airplane delivering {cargo} to {destination} by air");
    //         Console.WriteLine("   - Loading cargo into aircraft hold");
    //         Console.WriteLine("   - Flying at high altitude");
    //         Console.WriteLine($"   - Estimated delivery: {GetEstimatedDays()} days");
    //     }

    //     public double GetMaxWeight() => 5000; // 5 tons
    //     public int GetEstimatedDays() => 1;
    //     public string GetTransportMode() => "Air";
    // }

    // // New transportation types can be added easily
    // public class Train : ITransportation
    // {
    //     public void Deliver(string cargo, string destination)
    //     {
    //         Console.WriteLine($"🚆 Train delivering {cargo} to {destination} by rail");
    //         Console.WriteLine("   - Loading cargo into train cars");
    //         Console.WriteLine("   - Traveling on railway network");
    //         Console.WriteLine($"   - Estimated delivery: {GetEstimatedDays()} days");
    //     }

    //     public double GetMaxWeight() => 20000; // 20 tons
    //     public int GetEstimatedDays() => 2;
    //     public string GetTransportMode() => "Rail";
    // }

    // public class Drone : ITransportation
    // {
    //     public void Deliver(string cargo, string destination)
    //     {
    //         Console.WriteLine($"🚁 Drone delivering {cargo} to {destination} by air");
    //         Console.WriteLine("   - Attaching cargo to drone");
    //         Console.WriteLine("   - Flying directly to destination");
    //         Console.WriteLine($"   - Estimated delivery: {GetEstimatedDays()} days");
    //     }

    //     public double GetMaxWeight() => 50; // 50 kg
    //     public int GetEstimatedDays() => 1;
    //     public string GetTransportMode() => "Drone";
    // }

    // // Abstract Creator (Factory)
    // public abstract class LogisticsFactory
    // {
    //     // Factory method - implemented by concrete creators
    //     public abstract ITransportation CreateTransportation();

    //     // Template method using the factory method
    //     public virtual void PlanDelivery(string cargo, string destination, double weight)
    //     {
    //         var transport = CreateTransportation();
            
    //         // Validate weight capacity
    //         if (weight > transport.GetMaxWeight())
    //         {
    //             Console.WriteLine($"⚠️ Cargo weight ({weight}kg) exceeds {transport.GetTransportMode()} " +
    //                             $"maximum capacity ({transport.GetMaxWeight()}kg)!");
    //             return;
    //         }

    //         Console.WriteLine($"📦 Planning delivery using {transport.GetTransportMode()} transport");
    //         Console.WriteLine($"   Weight: {weight}kg (Max: {transport.GetMaxWeight()}kg)");
    //         transport.Deliver(cargo, destination);
    //     }

    //     public virtual void PlanExpressDelivery(string cargo, string destination, double weight)
    //     {
    //         var transport = CreateTransportation();
            
    //         if (transport.GetEstimatedDays() > 2)
    //         {
    //             Console.WriteLine($"⚠️ {transport.GetTransportMode()} transport is not suitable for express delivery!");
    //             return;
    //         }

    //         if (weight > transport.GetMaxWeight())
    //         {
    //             Console.WriteLine($"⚠️ Cargo too heavy for {transport.GetTransportMode()} express delivery!");
    //             return;
    //         }

    //         Console.WriteLine("🚨 EXPRESS DELIVERY SERVICE");
    //         Console.WriteLine($"📦 Express delivery using {transport.GetTransportMode()} transport");
    //         transport.Deliver($"EXPRESS: {cargo}", destination);
    //     }

    //     public virtual void GetQuote(string cargo, string destination, double weight)
    //     {
    //         var transport = CreateTransportation();
    //         var basePrice = weight * 2; // $2 per kg base price
    //         var multiplier = transport.GetTransportMode() switch
    //         {
    //             "Air" => 5.0,
    //             "Drone" => 8.0,
    //             "Road" => 1.0,
    //             "Rail" => 0.8,
    //             "Sea" => 0.5,
    //             _ => 1.0
    //         };

    //         var totalPrice = basePrice * multiplier;
    //         Console.WriteLine($"💰 Quote for {cargo} ({weight}kg) to {destination}:");
    //         Console.WriteLine($"   Transport: {transport.GetTransportMode()}");
    //         Console.WriteLine($"   Delivery time: {transport.GetEstimatedDays()} days");
    //         Console.WriteLine($"   Total cost: ${totalPrice:F2}");
    //     }
    // }

    // // Concrete Creators (Factories)
    // public class RoadLogisticsFactory : LogisticsFactory
    // {
    //     public override ITransportation CreateTransportation()
    //     {
    //         return new Truck();
    //     }
    // }

    // public class SeaLogisticsFactory : LogisticsFactory
    // {
    //     public override ITransportation CreateTransportation()
    //     {
    //         return new Ship();
    //     }
    // }

    // public class AirLogisticsFactory : LogisticsFactory
    // {
    //     public override ITransportation CreateTransportation()
    //     {
    //         return new Airplane();
    //     }
    // }

    // public class RailLogisticsFactory : LogisticsFactory
    // {
    //     public override ITransportation CreateTransportation()
    //     {
    //         return new Train();
    //     }
    // }

    // public class DroneLogisticsFactory : LogisticsFactory
    // {
    //     public override ITransportation CreateTransportation()
    //     {
    //         return new Drone();
    //     }

    //     // Override for drone-specific express delivery
    //     public override void PlanExpressDelivery(string cargo, string destination, double weight)
    //     {
    //         var transport = CreateTransportation();
            
    //         if (weight > transport.GetMaxWeight())
    //         {
    //             Console.WriteLine($"⚠️ Cargo too heavy for drone delivery!");
    //             return;
    //         }

    //         Console.WriteLine("🚨 ULTRA-FAST DRONE EXPRESS DELIVERY");
    //         Console.WriteLine("📦 Same-day delivery using drone technology");
    //         transport.Deliver($"ULTRA-EXPRESS: {cargo}", destination);
    //     }
    // }

    // // Client service - CLEAN and EXTENSIBLE
    // public class LogisticsService
    // {
    //     private readonly Dictionary<string, LogisticsFactory> _factories;

    //     public LogisticsService()
    //     {
    //         _factories = new Dictionary<string, LogisticsFactory>
    //         {
    //             { "truck", new RoadLogisticsFactory() },
    //             { "road", new RoadLogisticsFactory() },
    //             { "ship", new SeaLogisticsFactory() },
    //             { "sea", new SeaLogisticsFactory() },
    //             { "airplane", new AirLogisticsFactory() },
    //             { "air", new AirLogisticsFactory() },
    //             { "train", new RailLogisticsFactory() },
    //             { "rail", new RailLogisticsFactory() },
    //             { "drone", new DroneLogisticsFactory() }
    //         };
    //     }

    //     public void PlanDelivery(string transportType, string cargo, string destination, double weight)
    //     {
    //         if (_factories.TryGetValue(transportType.ToLower(), out var factory))
    //         {
    //             factory.PlanDelivery(cargo, destination, weight);
    //         }
    //         else
    //         {
    //             throw new ArgumentException($"Unsupported transport type: {transportType}");
    //         }
    //     }

    //     public void PlanExpressDelivery(string transportType, string cargo, string destination, double weight)
    //     {
    //         if (_factories.TryGetValue(transportType.ToLower(), out var factory))
    //         {
    //             factory.PlanExpressDelivery(cargo, destination, weight);
    //         }
    //         else
    //         {
    //             throw new ArgumentException($"Unsupported transport type: {transportType}");
    //         }
    //     }

    //     public void GetQuote(string transportType, string cargo, string destination, double weight)
    //     {
    //         if (_factories.TryGetValue(transportType.ToLower(), out var factory))
    //         {
    //             factory.GetQuote(cargo, destination, weight);
    //         }
    //         else
    //         {
    //             throw new ArgumentException($"Unsupported transport type: {transportType}");
    //         }
    //     }

    //     public void CompareOptions(string cargo, string destination, double weight)
    //     {
    //         Console.WriteLine($"🔍 Comparing transport options for {cargo} ({weight}kg) to {destination}:\n");
            
    //         foreach (var kvp in _factories)
    //         {
    //             if (kvp.Key.Contains("road") || kvp.Key.Contains("sea")) continue; // Skip aliases
                
    //             Console.WriteLine($"--- {kvp.Key.ToUpper()} ---");
    //             kvp.Value.GetQuote(cargo, destination, weight);
    //             Console.WriteLine();
    //         }
    //     }

    //     public List<string> GetAvailableTransports()
    //     {
    //         return _factories.Keys.Where(k => !k.Contains("road") && !k.Contains("sea")).ToList();
    //     }
    // }

    // // Usage example
    // public class AfterExample
    // {
    //     public static void RunExample()
    //     {
    //         Console.WriteLine("=== AFTER: Using Factory Method Pattern ===\n");
            
    //         var logistics = new LogisticsService();
            
    //         // Regular deliveries
    //         logistics.PlanDelivery("truck", "Electronics", "New York", 500);
    //         Console.WriteLine();
            
    //         logistics.PlanDelivery("ship", "Furniture", "London", 8000);
    //         Console.WriteLine();
            
    //         logistics.PlanDelivery("train", "Raw Materials", "Chicago", 15000);
    //         Console.WriteLine();

    //         // Express deliveries
    //         logistics.PlanExpressDelivery("airplane", "Medicine", "Tokyo", 200);
    //         Console.WriteLine();
            
    //         logistics.PlanExpressDelivery("drone", "Documents", "Local Office", 2);
    //         Console.WriteLine();

    //         // Weight limit testing
    //         logistics.PlanDelivery("drone", "Heavy Package", "Downtown", 100); // Too heavy
    //         Console.WriteLine();

    //         // Quotes comparison
    //         logistics.CompareOptions("Computer Parts", "Berlin", 300);

    //         // Show available transports
    //         Console.WriteLine("Available transport types:");
    //         foreach (var transport in logistics.GetAvailableTransports())
    //         {
    //             Console.WriteLine($"- {transport}");
    //         }
    //     }
    // }