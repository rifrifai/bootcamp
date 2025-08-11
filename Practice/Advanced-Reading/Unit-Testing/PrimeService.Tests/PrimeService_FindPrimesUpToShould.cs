/*
 * PrimeService_FindPrimesUpToShould.cs - Unit Tests for FindPrimesUpTo Method
 * 
 * This file demonstrates testing methods that return collections.
 * Testing collections requires different strategies than testing simple values.
 * 
 * Key concepts demonstrated:
 * - Testing collection results (arrays/lists)
 * - CollectionAssert for array/collection comparisons
 * - Testing edge cases with collections (empty results, single items)
 * - Performance considerations for larger datasets
 * - Testing boundary conditions
 */

using NUnit.Framework;
using PrimeService;
using System.Linq;

namespace PrimeService.Tests;

[TestFixture]
public class PrimeService_FindPrimesUpToShould
{
  private PrimeService _primeService;

  [SetUp]
  public void SetUp()
  {
    _primeService = new PrimeService();
  }

  #region Edge Cases and Invalid Input Tests
  [Test]
  public void ReturnEmptyArray_WhenLimitIsNegative()
  {
    int negativeLimit = -5;

    var result = _primeService.FindPrimesUpTo(negativeLimit);

    Assert.That(result, Is.Empty, "FindPrimesUpTo should return empty array for negative limits");
    // CollectionAssert.IsEmpty(result, "FindPrimesUpTo should return empty array for negative limits");
  }

  [Test]
  public void ReturnEmptyArray_WhenLimitIsZero()
  {
    int zeroLimit = 0;

    var result = _primeService.FindPrimesUpTo(zeroLimit);

    Assert.That(result, Is.Empty, "No primes exist less than or equal to 0");
  }

  [Test]
  public void ReturnEmptyArray_WhenLimitIsOne()
  {
    int oneLimit = 1;

    var result = _primeService.FindPrimesUpTo(oneLimit);

    Assert.That(result, Is.Empty, "1 is not prime number, so result should be empty");
  }
  #endregion

  #region Small Range Tests
  public void ReturnArrayWithTwo_WhenLimitIsTwo()
  {
    int limit = 2;
    int[] expectedPrime = { 2 };

    var result = _primeService.FindPrimesUpTo(limit);

    Assert.That(result, Is.EquivalentTo(expectedPrime), "When limit is 2, should return array containing only 2");
  }

  public void ReturnFirstTwoPrimes_WhenLimitIsThree()
  {
    int limit = 3;
    int[] expectedPime = { 2, 3 };

    var result = _primeService.FindPrimesUpTo(limit);

    Assert.That(result, Is.EqualTo(expectedPime), "When limit is 3, should return [2, 3]");

    Assert.That(result.Length, Is.EqualTo(2), "Should have exactly 2 primes");
    Assert.That(result, Does.Contain(2), "Result should contain 2");
    Assert.That(result, Does.Contain(3), "Result should contain 3");

    // Assert.AreEqual(2, result.Length, "Should have exactly 2 primes");
    // Assert.Contains(2, result, "Result should contain 2");
    // Assert.Contains(3, result, "Result should contain 3");
  }
  #endregion

  #region Medium Range Tests
  public void ReturnCorrectPrimes_WhenLimitIsTen()
  {
    int limit = 10;
    int[] expectedPrime = { 2, 3, 5, 7, 21 };

    var result = _primeService.FindPrimesUpTo(limit);

    Assert.That(result, Is.EqualTo(expectedPrime), "Primes up to 10 should be [2, 3, 5, 7]");
  }
  #endregion

}