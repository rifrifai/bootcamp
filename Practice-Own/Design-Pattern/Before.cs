using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeforeaFactoryMethod;
// Transportation classes without interface
    public class Truck
    {
        public void Deliver(string destination)
        {
            Console.WriteLine($"🚛 Truck delivering to {destination} by road");
        }
    }
    public class Ship
    {
        public void Deliver(string destination)
        {
            Console.WriteLine($"🚢 Ship delivering to {destination} by sea");
        }
    }
    // PROBLEM: Client code tightly coupled
    public class LogisticsManager
    {
        public void PlanDelivery(string transportType, string destination)
        {
        switch (transportType.ToLower())
        {
            case "truck":
                var truck = new Truck();
                truck.Deliver(destination);
                break;

            case "ship":
                var ship = new Ship();
                ship.Deliver(destination);
                break;

            case "airplane":
                var airplane = new Airplane();
                airplane.Deliver(destination);
                break;

            default:
                throw new ArgumentException("Invalid transport type");
            // ❌ Problems:
            // - Tight coupling
            // - Must modify this code to add new transport types
            // - Violates Open/Closed Principle
            }
        }
    }
    // ==========================================
    // DEMO PROGRAM
    // ==========================================
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("=== BEFORE: Without Factory Method Pattern ===");
            Console.WriteLine("❌ Problems: Tight coupling, hard to extend\n");

            var manager = new LogisticsManager();

            manager.PlanDelivery("truck", "Bandung");
            manager.PlanDelivery("ship", "Aceh");

            Console.WriteLine("⚠️ To add new transport type, we must modify LogisticsManager class!");
            Console.WriteLine("⚠️ This violates Open/Closed Principle\n");
        }
    }









            // manager.PlanDelivery("airplane", "Tokyo");
//     public class Airplane
    // {
    //     public void Deliver(string destination)
    //     {
    //         Console.WriteLine($"✈️ Airplane delivering to {destination} by air");
    //     }
    // }






// public class Truck
// {
//     public void Deliver(string cargo, string destination)
//     {
//         Console.WriteLine($"🚛 Truck delivering {cargo} to {destination} by road");
//         Console.WriteLine("   - Loading cargo into truck container");
//         Console.WriteLine("   - Driving on highways and roads");
//         Console.WriteLine("   - Estimated delivery: 2-3 days");
//     }
// }

// public class Ship
// {
//     public void Deliver(string cargo, string destination)
//     {
//         Console.WriteLine($"🚢 Ship delivering {cargo} to {destination} by sea");
//         Console.WriteLine("   - Loading cargo into ship containers");
//         Console.WriteLine("   - Sailing across oceans");
//         Console.WriteLine("   - Estimated delivery: 7-14 days");
//     }
// }

// public class Airplane
// {
//     public void Deliver(string cargo, string destination)
//     {
//         Console.WriteLine($"✈️ Airplane delivering {cargo} to {destination} by air");
//         Console.WriteLine("   - Loading cargo into aircraft hold");
//         Console.WriteLine("   - Flying at high altitude");
//         Console.WriteLine("   - Estimated delivery: 1-2 days");
//     }
// }

// // PROBLEMATIC: Logistics service with tight coupling
// public class LogisticsService
// {
//     public void PlanDelivery(string transportType, string cargo, string destination, double weight)
//     {
//         // Problems:
//         // 1. Tight coupling with concrete classes
//         // 2. Violates Open/Closed Principle
//         // 3. Complex conditional logic
//         // 4. Code duplication
//         // 5. Hard to test and maintain

//         switch (transportType.ToLower())
//         {
//             case "truck":
//             case "road":
//                 if (weight > 10000)
//                 {
//                     Console.WriteLine("⚠️ Cargo too heavy for standard truck!");
//                     return;
//                 }
//                 var truck = new Truck();
//                 truck.Deliver(cargo, destination);
//                 break;

//             case "ship":
//             case "sea":
//                 if (weight > 50000)
//                 {
//                     Console.WriteLine("⚠️ Cargo too heavy for standard ship!");
//                     return;
//                 }
//                 var ship = new Ship();
//                 ship.Deliver(cargo, destination);
//                 break;

//             case "airplane":
//             case "air":
//                 if (weight > 5000)
//                 {
//                     Console.WriteLine("⚠️ Cargo too heavy for airplane!");
//                     return;
//                 }
//                 var airplane = new Airplane();
//                 airplane.Deliver(cargo, destination);
//                 break;

//             default:
//                 throw new ArgumentException("Invalid transport type");
//         }
//     }

//     // Duplicated logic for express delivery
//     public void PlanExpressDelivery(string transportType, string cargo, string destination)
//     {
//         Console.WriteLine("🚨 EXPRESS DELIVERY");
        
//         // Same problematic switch statement!
//         switch (transportType.ToLower())
//         {
//             case "truck":
//                 var truck = new Truck();
//                 truck.Deliver($"EXPRESS: {cargo}", destination);
//                 break;
//             case "airplane":
//                 var airplane = new Airplane();
//                 airplane.Deliver($"EXPRESS: {cargo}", destination);
//                 break;
//             default:
//                 Console.WriteLine("Express delivery only available for truck and airplane");
//                 break;
//         }
//     }
// }

// public class BeforeExample
// {
//     public static void RunExample()
//     {
//         Console.WriteLine("=== BEFORE: Without Factory Method ===\n");
        
//         var logistics = new LogisticsService();
        
//         logistics.PlanDelivery("truck", "Electronics", "New York", 500);
//         Console.WriteLine();
        
//         logistics.PlanDelivery("ship", "Furniture", "London", 8000);
//         Console.WriteLine();
        
//         logistics.PlanExpressDelivery("airplane", "Medicine", "Tokyo");
        
//         // Adding new transport type requires modifying LogisticsService!
//     }
// }