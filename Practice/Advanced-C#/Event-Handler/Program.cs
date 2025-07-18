// See https://aka.ms/new-console-template for more information
namespace EventHandlerDemo
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("=== Events in C# - Complete Demonstration ===");

      // run all demonstrations to show event concept in action
      BasicEventDeclarationDemo();
      EventVsDelegateDemo();
    }

    static void BasicEventDeclarationDemo()
    {
      Console.WriteLine("1. Basic Event Declaration - Broadcaster/Subscriber Pattern");
      Console.WriteLine("===========================================================");
      var priceMonitor = new BasicPriceMotor("AAPL");

      void Trader1Handler(decimal oldPrice, decimal newPrice) => Console.WriteLine($"  Trader 1: Price changed from ${oldPrice} to ${newPrice}");

      void Trader2Handler(decimal oldPrice, decimal newPrice) => Console.WriteLine($"  Trader 2: Price changed from ${oldPrice} to ${newPrice}");

      priceMonitor.PriceChanged += Trader1Handler;
      priceMonitor.PriceChanged += Trader2Handler;

      Console.WriteLine("Subscribed two traders to price changes");
      Console.WriteLine("Triggering price changes...\n");

      priceMonitor.UpdatePrice(150.00m);
      priceMonitor.UpdatePrice(155.50m);

      priceMonitor.PriceChanged -= Trader1Handler;
      Console.WriteLine("\nTrader 1 unsubscribed. Only Trader 2 should receive this update: ");
      priceMonitor.UpdatePrice(152.75m);

      Console.WriteLine();
    }
    public delegate void PriceChangeHandler(decimal oldPrice, decimal newPrice);

    public class BasicPriceMotor
    {
      private decimal _currentPrice;
      public string Symbol { get; }
      public event PriceChangeHandler? PriceChanged;
      public BasicPriceMotor(string symbol)
      {
        Symbol = symbol;
        _currentPrice = 0;
      }
      public void UpdatePrice(decimal newPrice)
      {
        if (_currentPrice != newPrice)
        {
          decimal oldPrice = _currentPrice;
          _currentPrice = newPrice;
          PriceChanged?.Invoke(oldPrice, newPrice);
        }
      }
    }

    static void EventVsDelegateDemo()
    {
      Console.WriteLine("2. Event Vs Delegate - Safety Compariso");
      Console.WriteLine("=======================================");

      var eventPublisher = new EventPublisher();
      var delegatePublisher = new DelegatePublisher();


    }
    public class EventPublisher
    {
      public event Action<string>? SafeNotification;
      public void TriggerEvent(string message)
      {
        Console.WriteLine($"Event publisher sending: {message}");
        SafeNotification?.Invoke(message);
      }
    }
    public class DelegatePublisher
    {
      public Action<string>? UnsafeNotification;
      public void TraggerEvent(string message)
      {
        Console.WriteLine($"Event publisher sending: {message}");
        UnsafeNotification?.Invoke(message);
      }
    }
  }
}