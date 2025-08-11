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

[TestFixture]
public class PrimeService_IsPrimeShould
{
  private PrimeService _primeService;

  [SetUp]
  public void Setup()
  {
    _primeService = new PrimeService();
  }

  [TearDown]
  public void TearDown()
  {
  }

  #region Edge Cases and Invalid Inputs
  [Test]
  public void IsPrime_InputIs1_ReturnFalse()
  {
    // Arrange: set up the test
    int input = 1;

    // Act: execute the method we're testing
    var result = _primeService.IsPrime(input);

    // Assert: verify the result is what we expect
    Assert.That(result, Is.False, "1 should not be prime by mathematical definition");
  }

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

  [Test]
  public void IsPrime_LargeNegativeNumber_ReturnFalse()
  {
    int input = int.MinValue;

    var result = _primeService.IsPrime(input);

    Assert.That(result, Is.False, "Large negative numbers should not be prime!");
  }

  #endregion


  #region Known Prime Numbers

  [Test]
  public void IsPrime_InputIs2_ReturnTrue()
  {
    int input = 2;

    var result = _primeService.IsPrime(input);

    Assert.That(result, Is.True, "2 is the smallest prime number");
  }

  [TestCase(2)]
  [TestCase(3)]
  [TestCase(5)]
  [TestCase(7)]
  [TestCase(11)]
  [TestCase(13)]
  [TestCase(17)]
  [TestCase(19)]
  [TestCase(23)]
  [TestCase(97)]
  [TestCase(101)]
  public void IsPime_KnownPrimeNumbers_ReturnTrue(int primeNumbers)
  {
    var result = _primeService.IsPrime(primeNumbers);

    Assert.That(result, Is.True, $"{primeNumbers} should be identified as prime");
  }

  #endregion

  #region Known Composite Numbers
  [Test]
  public void IsPrime_InputIs4_ReturnFalse()
  {
    int input = 4;

    var result = _primeService.IsPrime(input);

    Assert.That(result, Is.False, "4 is composite (2 * 2) not prime");
  }

  [TestCase(4)]
  [TestCase(6)]
  [TestCase(8)]
  [TestCase(9)]
  [TestCase(12)]
  [TestCase(14)]
  [TestCase(15)]
  [TestCase(21)]
  [TestCase(25)]
  [TestCase(100)]
  public void IsPrime_KnownCompositeNumbers_ReturnFalse(int compositeNumber)
  {
    var result = _primeService.IsPrime(compositeNumber);

    Assert.That(result, Is.False, $"{compositeNumber} should be identified as composite (not prime)");
  }

  #endregion

  #region Performance and Large Numbers

  [TestCase(982451653)]
  [TestCase(982451654)]
  public void IsPrime_LargeNumbers_WorksCorrectly(int largeNumbers)
  {
    var stopWatch = System.Diagnostics.Stopwatch.StartNew();

    var result = _primeService.IsPrime(largeNumbers);

    stopWatch.Stop();

    if (largeNumbers == 982451653)
    {
      Assert.That(result, Is.True, "982451653 should be prime");
    }
    else
    {
      Assert.That(result, Is.False, "982451654 should be composite");
    }
  }

  #endregion

  #region Alternative Assert Syntax Examples
  [Test]
  public void IsPrime_DemonstrateAssertSyntax_InputIs3()
  {
    var result = _primeService.IsPrime(3);

    // Assert.IsTrue(result, "3 should be prime - Classic syntax");

    Assert.That(result, Is.True, "3 should be prime - Constraint syntax (Newer)");

    Assert.That(result, Is.EqualTo(true), "3 should be prime - Fluent syntax (Most modern)");

    Assert.That(result, "3 should be prime - Simple boolean check");
  }

  #endregion
}