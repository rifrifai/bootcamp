namespace DemonstrationThreading;

class Program
{
  static void Main(string[] args)
  {
    // Demo 1: The Classic Thread Example - Main Thread vs New Thread
    Console.WriteLine("1. Classic Threading Example - Interleaved Execution");
    DemoClassicThreading();

    Console.WriteLine("\n" + new string('-', 60) + "\n");

    // Demo 2: Thread Properties and Lifecycle
    Console.WriteLine("2. Thread Properties and Lifecycle Management");
    DemoThreadProperties();

    Console.WriteLine("\n" + new string('-', 60) + "\n");

    // Demo 3: Thread Synchronization - Join and Sleep
    Console.WriteLine("3. Thread Synchronization - Join and Sleep");
    DemoThreadSynchronization();

  }

  static void DemoClassicThreading()
  {
    Console.WriteLine("Starting classic threading demo - you'll see interleaved x's and y's");

    // create new thread to run WriteY method
    Thread t = new Thread(WriteY);
    t.Start();  // start new thread

    // meanwhile, main thread does its own work
    for (int i = 0; i < 15; i++)
    {
      Console.WriteLine("X");
    }

    // wait for the other thread to complete
    t.Join();
    Console.WriteLine("\nDemo complete - notice how x's and y's were mixed together");
  }
  static void WriteY()
  {
    for (int i = 0; i < 15; i++)
    {
      Console.WriteLine("Y");
    }
  }


  static void DemoThreadProperties()
  {
    Console.WriteLine("Examining thread properties and lifecycle...");

    // create thread but dont start it yet
    Thread worker = new Thread(DoSomeWork);
    worker.Name = "WorkerThread";   // naming helps with debugging

    Console.WriteLine($"Before Start - IsAlive: {worker.IsAlive}");
    Console.WriteLine($"Thread Name: {worker.Name}");
    Console.WriteLine($"Is Background: {worker.IsBackground}");
    Console.WriteLine($"Thread State: {worker.ThreadState}");

    // after start
    worker.Start();
    Console.WriteLine($"After Start - IsAlive: {worker.IsAlive}");
    Console.WriteLine($"Thread State: {worker.ThreadState}");

    // check current thread info
    Console.WriteLine($"Current thread name: {Thread.CurrentThread.Name ?? "Main"}");
    Console.WriteLine($"Current thread ID: {Thread.CurrentThread.ManagedThreadId}");
    Console.WriteLine($"Is thread pool thread: {Thread.CurrentThread.IsThreadPoolThread}");

    // wait for completion
    worker.Join();
    Console.WriteLine($"After Join - IsAlive: {worker.IsAlive}");
    Console.WriteLine($"Final Thread State: {worker.ThreadState}");
  }
  static void DoSomeWork()
  {
    Console.WriteLine($"Worker thread starting on thread ID: {Thread.CurrentThread.ManagedThreadId}");
    Thread.Sleep(2000);   // simulate some work
    Console.WriteLine("Worker thread finishing");
  }


  static void DemoThreadSynchronization()
  {
    Console.WriteLine("Demonstrating Join and Sleep methods...");

    // start thread and immediately join it
    Thread quickWorker = new Thread(() =>
    {
      Console.WriteLine($"Quick worker starting");
      Thread.Sleep(1000);
      Console.WriteLine("Quick worker finishing");
    });

    quickWorker.Start();
    Console.WriteLine("Main thread waiting for quick worker...");
    quickWorker.Join();  // Main thread blocks until quickWorker completes
    Console.WriteLine("Quick worker finished, main thread continues");

    // demonstrate Join with timeout
    Thread slowWorker = new Thread(() =>
    {
      Console.WriteLine("Slow worker starting");
      Thread.Sleep(3000);
      Console.WriteLine("Slow worker done");
    });

    slowWorker.Start();
    Console.WriteLine("Main thread waiting for slow worker (max 1 second)...");
    bool finished = slowWorker.Join(1000);  // wait max 1 second

    if (finished)
      Console.WriteLine("Slow worker finished in time");
    else
      Console.WriteLine("Slow worker didn't finish in time, continuing anyway");

    // Demonstrate different Sleep scenarios
    Console.WriteLine("\nDemonstrating Thread.Sleep variations:");
    Console.WriteLine("Sleeping for 500ms...");
    Thread.Sleep(500);

    Console.WriteLine("Sleeping with TimeSpan...");
    Thread.Sleep(TimeSpan.FromMilliseconds(500));

    Console.WriteLine("Yielding time slice (Sleep(0))...");
    Thread.Sleep(0);  // Immediately yield to other threads

    Console.WriteLine("Thread.Yield() - yield to threads on same processor");
    Thread.Yield();   // Similar to Sleep(0) but more specific

    slowWorker.Join();  // Make sure slow worker finishes
  }
}