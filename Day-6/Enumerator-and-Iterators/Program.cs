using System;
using System.Collections;
using System.Collections.Generic;

namespace EnumeratosAndIterators
{
  class Program
  {
    static void Main()
    {
      Console.WriteLine("=== ENUMERATION AND ITERATORS DEMONSTRATION ===\n");

      // 1. Basic enumeration with foreach
      DemonstrateBasicEnumeration();

      // 2. Manual enumeration (what foreach does under the hood)
      DemonstrateManualEnumeration();

      // 3. Custom enumerable and enumerator
      DemonstrateCustomEnumerable();
    }

    static void DemonstrateBasicEnumeration()
    {
      Console.WriteLine("--- 1. Basic Enumeration with foreach ---");

      // The foreach statement is the high-level way to iterate
      // It works with any type that implements IEnumerable<T> or has GetEnumerator() method
      string word = "panda";
      Console.WriteLine($"Iterating through the string '{word}':");

      foreach (var i in word)
      {
        Console.WriteLine($"  Character: {i}");
      }
      Console.WriteLine();
    }

    static void DemonstrateManualEnumeration()
    {
      Console.WriteLine("--- 2. Manual Enumeration (what foreach does behind the scenes) ---");

      string word = "panda";
      Console.WriteLine($"Manually iterating through '{word}' using GetEnumerator():");

      // This is what the compiler generates for foreach statements
      using (var enumerator = word.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          var element = enumerator.Current;
          Console.WriteLine($"  Character: {element}");
        }
      }   // Dispose is called automatically due to 'using'
      Console.WriteLine();
    }

    static void DemonstrateCustomEnumerable()
    {
      Console.WriteLine("--- 3. Custom Enumerable and Enumerator ---");

      var countDown = new CountdownSequence(5);
      Console.WriteLine("custom countdown sequence from 5 to 1: ");

      foreach (var num in countDown)
      {
        Console.WriteLine($"   Count: {num}");
      }
      Console.WriteLine();

    }

    // Custom enumerable class that demonstrates the enumeration pattern
    public class CountdownSequence : IEnumerable<int>
    {
      private readonly int _start;

      public CountdownSequence(int start)
      {
        _start = start;
      }

      // GetEnumerator() returns an enumerator for this sequence
      public IEnumerator<int> GetEnumerator()
      {
        return new CountdownEnumerator(_start);
      }

      // Non-generic version required by IEnumerable
      IEnumerator IEnumerable.GetEnumerator()
      {
        return GetEnumerator();
      }
    }

    // Custom enumerator that implements the cursor logic
    public class CountdownEnumerator : IEnumerator<int>
    {
      private readonly int _start;
      private int _current;
      private bool _started = false;

      public CountdownEnumerator(int start)
      {
        _start = start;
        _current = start + 1; // Start one above so first MoveNext() gives correct value
      }

      // Current element at cursor position
      public int Current { get; private set; }

      // Non-generic version
      object IEnumerator.Current => Current;

      // Move cursor to next position, return true if successful
      public bool MoveNext()
      {
        if (!_started)
        {
          _started = true;
          _current = _start;
        }
        else
        {
          _current--;
        }

        if (_current >= 1)
        {
          Current = _current;
          return true;
        }

        return false; // End of sequence reached
      }

      // Reset cursor to beginning (optional for most scenarios)
      public void Reset()
      {
        _current = _start + 1;
        _started = false;
      }

      // Clean up resources when enumeration is done
      public void Dispose()
      {
        // In this simple example, no cleanup needed
        // But this is where you'd release resources like file handles, database connections, etc.
      }
    }
  }
}