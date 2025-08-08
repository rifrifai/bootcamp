using NUnit.Framework;
using UnoRevisi.Controller;
using UnoRevisi.Interfaces;

namespace UnoRevisi.Tests
{
  [TestFixture]
  public class GameController_BeforeStart
  {
    private GameController _gameController = null;

    [SetUp]
    public void Setup()
    {
      // Arrange
      var players = new List<IPlayer>();
      _gameController = new GameController(players);
    }

    [Test]
    public void IsGameStarted_BeforeStart_ReturnFalse()
    {
      // Arrange
      // var players = new List<IPlayer>();
      // var gameController = new GameController(players);

      // Act
      var result = _gameController.IsGameStarted();

      // Assert
      Assert.IsFalse(result);
    }

    [Test]
    public void StartGame_With1Player_ReturnFalse()
    {
      var result = _gameController.StartGame();

      Assert.IsFalse(result);
    }

    [Test]
    public void IsDeckEmpty_Default_ReturnFalse()
    {
      var result = _gameController.IsDeckEmpty();

      Assert.IsTrue(result);
    }
  }
}