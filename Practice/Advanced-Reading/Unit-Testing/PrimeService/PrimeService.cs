/*
 * PrimeService - A simple Prime Number Service

 * This class demonstrate basic functionality that we'll test using NUnit.
 * The pupose is to show how to write testable code and then create comprehensive unit test for it.
 * In real development, you'd implement the logic first, but for learning purposes, we'll write the tests first.
 * Test-Driven Development (TDD), we start with a failing implementation and let the tests drive our design.
*/

namespace PrimeService;

/// <summary>
/// Service class for prime number operations
/// This is our "System Under Test" (SUT) - the thing we're going to test
/// </summary>

public class PrimeService
{
  /// <summary>
  /// Determine if a give number is prime
  /// 
  /// A prime number is a natural number greater than 1 that has no positive
  /// divisors other than 1 and itself. The first few prime numbers are
  /// 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47...
  /// </summary>        /// <param name="candidate"> The number to test for primality</param>
  /// <returns>True if the number is prime, false otherwise</returns>

  public bool IsPrime(int candidate)
  {
    // handle edge cases - nembers less than 2 are not prime
    if (candidate < 2) return false;

    // 2 is the only even prime number
    if (candidate == 2) return true;

    // all other even numbers are not prime
    if (candidate % 2 == 0) return false;

    // small prime optimizations
    if (candidate == 3) return true;
    if (candidate % 3 == 0) return false;

    // check for factors from 5 to sqrt(candidate)
    // use 6k±1 optimization: all prime > 3 are of the from 6k±1
    int limit = (int)Math.Sqrt(candidate);
    for (int divisor = 5; divisor <= limit; divisor += 6)
    {
      if (candidate % divisor == 0 || candidate % (divisor + 2) == 0) return false;
    }

    return true;
  }

  /// <summary>
  /// Finds all prime numbers up to a given limit using the Sieve of Eratosthenes
  /// This gives us another method to test and demonstrates testing methods that return collections
  /// </summary>
  /// <param name="limit">Find all primes up to this number (inclusive)</param>
  /// <returns>Array of prime numbers up to the limit<returns>
  public int[] FindPrimesUpTo(int limit)
  {
    if (limit < 2) return new int[0];   // return empty array for invalid input

    // sieve of eratosthenes algorithm
    bool[] isPrime = new bool[limit + 1];

    // initialize all numbers as potentially prime
    for (int i = 2; i <= limit; i++)
    {
      isPrime[i] = true;
    }

    // mark non-primes
    for (int i = 2; i * i <= limit; i++)
    {
      if (isPrime[i])
      {
        // mark all multiples of i as non-prime
        for (int j = i * i; j < limit; j += i)
        {
          isPrime[j] = false;
        }
      }
    }

    // count primes to determine array size
    int primeCount = 0;
    for (int i = 2; i <= limit; i++)
    {
      if (isPrime[i]) primeCount++;
    }

    // create result array
    int[] primes = new int[primeCount];
    int index = 0;
    for (int i = 2; i <= limit; i++)
    {
      if (isPrime[i])
      {
        primes[index++] = i;
      }
    }

    return primes;
  }

  /// <summary>
  /// Gets the next prime number after the given number
  /// Demonstrate testing methods with more complex logic
  /// </summary>
  /// <param name="number">Starting number</param>
  /// <returns>The next prime number after the input</returns>
  public int GetNextPrime(int number)
  {
    // handle edge cases
    if (number < 1) return 2;
    if (number == 1) return 2;

    // start searching from the next number
    int candidate = number + 1;

    // keep searching until we find a prime
    while (!IsPrime(candidate))
    {
      candidate++;

      // prevent infinite loops for very large numbers
      if (candidate < 0) throw new OverflowException("No next prime number found due to integer overflow");
    }

    return candidate;
  }
}