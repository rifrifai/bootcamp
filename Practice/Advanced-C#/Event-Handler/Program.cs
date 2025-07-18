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

      eventPublisher.SafeNotification += msg => Console.WriteLine($"  Event received: {msg}");
      delegatePublisher.UnsafeNotification += msg => Console.WriteLine($"  Delegate received: {msg}");

      Console.WriteLine("Both subscribed successfully");

      eventPublisher.TriggerEvent("Hello from event");
      delegatePublisher.TriggerEvent("Hello from delegate");

      Console.WriteLine("\nTesting safety differences: ");

      // try to do dangerous things - these will show the difference
      //eventPublisher.SafeNotification = null;     //compile error - can't assign to event
      // eventPublisher.SafeNotification("hack");      //compile error - can't invoke from outside

      // but with delegate, these dangerous operations are possible:
      Console.WriteLine("Delegate allows dangerous operations: ");
      delegatePublisher.UnsafeNotification = null;    //wipes out all subscribers!
      delegatePublisher.TriggerEvent("This won't be received by anyone");

      Console.WriteLine("Event safety prevents subscriber interference\n");
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
      public void TriggerEvent(string message)
      {
        Console.WriteLine($"Event publisher sending: {message}");
        UnsafeNotification?.Invoke(message);
      }
    }
  }
}