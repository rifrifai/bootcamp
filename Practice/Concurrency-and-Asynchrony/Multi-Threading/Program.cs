namespace MultiThreading;

class Proram
{
  static void Main(string[] args)
  {
    Console.WriteLine("=== Multithreading Masterclass ===");
    Console.WriteLine("This demo covers all essential threading concepts in C#\n");

    RunDemo("1. Sequential vs Concurrent Execution", DemoSequentialVsConcurrent);
    RunDemo("2. Thread Creation and Naming", DemoThreadCreationAndNaming);
  }


  // helper method to run demonstrations with clear separation
  static void RunDemo(string title, Action demoMethod)
  {
    Console.WriteLine($"\n{title}");
    Console.WriteLine(new string('=', title.Length));

    try
    {
      demoMethod();
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Demo error: {ex.Message}");
    }
    Console.WriteLine($"\nPress Enter to continue to next demo...");
    Console.ReadLine();
  }


  // Demo 1: Shows the difference between sequential and concurrent execution
  static void DemoSequentialVsConcurrent()
  {
    Console.WriteLine("First, let's see sequential execution:");
    var startTime = DateTime.Now;

    // sequential execution - one method after another
    Method1();
    Method2();
    Method3();

    var sequentialTime = DateTime.Now - startTime;
    Console.WriteLine($"Sequential execution took: {sequentialTime.TotalSeconds:F2} seconds");

    Console.WriteLine("\nNow let's see concurrent execution:");
    startTime = DateTime.Now;

    // concurrent execution - all methods run simultaneously
    Thread t1 = new Thread(Method1);
    Thread t2 = new Thread(Method2);
    Thread t3 = new Thread(Method3);

    t1.Start();
    t2.Start();
    t3.Start();

    var concurrentTime = DateTime.Now - startTime;
    Console.WriteLine($"Concurrent execution took: {concurrentTime.TotalSeconds:F2} seconds");
    Console.WriteLine("Notice how concurrent execution is faster when methods can run in parallel!");
  }
  // these methods simulate different types of work
  static void Method1()
  {
    Console.WriteLine($"Method1 started on {GetThreadInfo()}");
    for (int i = 1; i < 3; i++)
    {
      Console.WriteLine($"Method1: Step{i}");
      Thread.Sleep(500);
    }
    Console.WriteLine($"Method1 completed on {GetThreadInfo()}");
  }
  static void Method2()
  {
    Console.WriteLine($"Method2 startd on {GetThreadInfo()}");
    for (int i = 1; i < 3; i++)
    {
      Console.WriteLine($"Method2: Step{i}");
      if (i == 2)
      {
        Console.WriteLine($"Method2: Simulation database operation...");
        Thread.Sleep(2000);
        Console.WriteLine($"Method2: Datebase simulation complete.");
      }
      else
      {
        Thread.Sleep(300);
      }
      Console.WriteLine($"Method2 completed on {GetThreadInfo()}");
    }
  }
  static void Method3()
  {
    Console.WriteLine($"Method3 started on {GetThreadInfo()}");
    for (int i = 1; i <= 4; i++)
    {
      Console.WriteLine($"Method3: Processing item {i}");
      Thread.Sleep(400); // Simulate processing
    }
    Console.WriteLine($"Method3 completed on {GetThreadInfo()}");
  }


  static void DemoThreadCreationAndNaming()
  {
    Console.WriteLine("Creating threads with meaningful names helps during debugging:");

    // Create threads with descriptive names
    Thread uiThread = new Thread(SimulateUIWork) { Name = "UI-UpdateThread" };
    Thread dataThread = new Thread(SimulateDataProcessing) { Name = "DataProcessing-Thread" };
    Thread logThread = new Thread(SimulateLogging) { Name = "Logging-Thread" };

    Console.WriteLine("Starting named threads...");
    uiThread.Start();
    dataThread.Start();
    logThread.Start();

    // Show main thread info
    Console.WriteLine($"Main thread: {Thread.CurrentThread.Name ?? "Main"}");

    // Wait for all to complete
    uiThread.Join();
    dataThread.Join();
    logThread.Join();

    Console.WriteLine("All named threads completed!");

  }
  static void SimulateUIWork()
  {
    for (int i = 1; i <= 3; i++)
    {
      Console.WriteLine($"[{Thread.CurrentThread.Name}] Updating UI element {i}");
      Thread.Sleep(600);
    }
  }

  static void SimulateDataProcessing()
  {
    for (int i = 1; i <= 4; i++)
    {
      Console.WriteLine($"[{Thread.CurrentThread.Name}] Processing data chunk {i}");
      Thread.Sleep(400);
    }
  }

  static void SimulateLogging()
  {
    for (int i = 1; i <= 5; i++)
    {
      Console.WriteLine($"[{Thread.CurrentThread.Name}] Writing log entry {i}");
      Thread.Sleep(300);
    }
  }


  // helper method to get thread information
  static string GetThreadInfo()
  {
    Thread current = Thread.CurrentThread;
    return $"Thread {current.ManagedThreadId} ({current.Name ?? "Unnamed"})";
  }
}