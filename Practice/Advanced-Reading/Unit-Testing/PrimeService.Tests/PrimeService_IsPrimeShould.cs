/*
 * PrimeService_IsPrimeShould.cs - Unit Tests for IsPrime Method
 *
 * This file demonstrate comprehensive unit testing practices using NUnit.
 * We follow the naming convention: [className]_[methodName]Should
 *
 * Key concepts demonstrated:
 * 1. Test-Driven Development (TDD) approach
 * 2. Arrange, Act, Assert pattern
 * 3. Test case parameterization
 * 4. Test setup and teardown
 * 5. Descriptive test names that explain the scenarion
 *
 * Remember: Good tests are like documentation - they should clearly explain what the code is supposed to do in different scenarios.
*/

using NUnit.Framework;
using PrimeService;

namespace PrimeService.Tests;

/// <summary>
/// Test fixture for testing the IsPrime method of PrimeService
/// 
/// [TestFixture] tells NUnit that this class contains tests.
/// We organize tests by the method we're testing to keep things clean.
/// </summary>

[TestFixture]
public class PrimeService_IsPrimeShould
{
  // this is our "System Under Test" (SUT)
  // we create a fresh instance of each test to ensure test isolation
  private PrimeService _primeService;

  /// <summary>
  /// SetUp method runs before each individual test
  /// This ensures each test starts with a clean state
  /// Think of it as "preparing the stage" for each test
  /// </summary>
  [SetUp]
  public void Setup()
  {
    // create a new instance of each test
    // this prevents tests from affecting each other
    _primeService = new PrimeService();
  }

  /// <summary>
  /// TearDown runs after each test (if you need cleanup)
  /// For this simple example, we don't need it, but it's good to know about
  /// </summary>
  [TearDown]
  public void TearDown()
  {
    // In more complex scenarios, you might need to:
    // - Close database connections
    // - Delete temporary files
    // - Reset static state
    // For our simple service, no cleanup needed
  }

  #region Edge Cases and Invalid Inputs
  /// <summary>
  /// Test that demonstrate TDD - this will fail initially
  /// We start with the simplest case: 1 is not prime
  /// 
  /// Test naming convention: [MethodName]_[Scenario]_[ExpectedResult]
  /// This make it crystal clear what we're testing and what we expect
  /// </summary>
  [Test]
  public void IsPrime_InputIs1_ReturnFalse()
  {
    // Arrange: set up the test
    int input = 1;

    // Act: execute the method we're testing
    // note: this will initially throw NotImplementedException
    var result = _primeService.IsPrime(input);

    // Assert: verify the result is what we expect
    Assert.That(result, Is.False, "1 should not be prime by mathematical definition");
  }

  ///<summary>
  /// Using TestCase attribute to test multiple similiar scenarios
  /// This is more efficient than writing seperate test for each value
  /// All values less than 2 should return false
  ///</summary>
  [TestCase(-1)]
  [TestCase(0)]
  [TestCase(1)]
  public void IsPrime_ValuesLessThan2_ReturnFalse(int value)
  {
    // Act
    var result = _primeService.IsPrime(value);

    // Assert
    Assert.That(result, Is.False, $"{value} should not be prime (values less than 2 are not prime)");
  }

  /// <summary>
  /// Test with exremely large negative numbers
  /// Good tests cover edge cases that might break your code
  /// </summary>
  [Test]
  public void IsPrime_LargeNegativeNumber_ReturnFalse()
  {
    int input = int.MinValue;

    var result = _primeService.IsPrime(input);

    Assert.That(result, Is.False, "Large negative numbers should not be prime!");
  }

  #endregion


  #region Known Prime Numbers

  /// <summary>
  /// Test the smallest prime number
  /// 2 is the first and only even prime number
  /// </summary>
  [Test]
  public void IsPrime_InputIs2_ReturnTrue()
  {
    int input = 2;

    var result = _primeService.IsPrime(input);

    Assert.That(result, Is.True, "2 is the smallest prime number");
  }

  /// <summary>
  /// Test several known prime numbers using TestCase
  /// This ensures our algorithm works correctly for various primes
  /// </summary>
  [TestCase(2)]
  [TestCase(3)]
  [TestCase(5)]
  [TestCase(7)]
  [TestCase(11)]
  [TestCase(13)]
  [TestCase(17)]
  [TestCase(19)]
  [TestCase(23)]
  [TestCase(97)]  // larger 2 digit prime
  [TestCase(101)] // 3 digits prime
  public void IsPime_KnownPrimeNumbers_ReturnTrue(int primeNumbers)
  {
    var result = _primeService.IsPrime(primeNumbers);

    Assert.That(result, Is.True, $"{primeNumbers} should be identified as prime");
  }

  #endregion




}