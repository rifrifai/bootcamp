using NUnit.Framework;
using UnoRevisi.Controller;
using UnoRevisi.Interfaces;

namespace UnoRevisi.Tests
{
  [TestFixture]
  public class Tests
  {
    private List<IPlayer> _players = new();
    private GameController _gameController = new GameController(_players);
    // private Deck _deck;

    [SetUp]
    public void Setup()
    {
      // Arrange
      _players = new List<IPlayer>();
      _gameController = new GameController(_players);

      if (_players.Count < 2)
      {
        return false;
      }
      _gameController.StartGame();
      // _gameController.ShuffleDeck();
      // _gameController.InitializePlayerHands(_players);
      // _gameController.DealCardsToPlayers();
    }

    [Test]
    public void GetCurrentPlayerIndex_GettingCurrentPlayerIndex_ReturnCurrentPlayerIndex()
    {
      int expected = 0;
      int actual = _gameController.GetCurrentPlayer();

      Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void IsGameStarted_BeforeStart_ReturnFalse()
    {
      // Arrange
      var players = new List<IPlayer>();
      var gameController = new GameController(players);

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