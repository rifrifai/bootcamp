using System;
using System.Collections.Generic;

namespace Uno
{
  // Enumerations
  public enum Color { Red, Blue, Green, Yellow }
  public enum CardType { Number, Action, Wild }
  public enum ActionType { Skip, Reverse, DrawTwo }
  public enum WildType { Wild, WildDrawFour }
  public enum Number { Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine }

  // Interfaces
  public interface ICard
  {
    CardType GetCardType();
    Color? GetColor();
    Number? GetNumber();
    ActionType? GetActionType();
    WildType? GetWildType();
    void GetDisplayText();

    void SetCardType(CardType type);
    void SetColor(Color? color);
    void SetNumber(Number? number);
    void SetActionType(ActionType? actionType);
    void SetWildType(WildType? wildType);
  }

  public interface IPlayer
  {
    string GetName();
    void SetName(string name);
  }

  public interface IDeck
  {
    List<ICard> GetCards();
    ICard GetCardAt(int index);
    void SetCards(List<ICard> cards);
    void SetCardAt(int index, ICard card);
  }

  public interface IDiscardPile
  {
    List<ICard> GetCards();
    ICard GetCardAt(int index);
    void SetCards(List<ICard> cards);
    void SetCardAt(int index, ICard card);
  }
  public class Card : ICard
  {
    private CardType _type;
    private Color? _color;
    private Number? _number;
    private ActionType? _actionType;
    private WildType? _wildType;

    public CardType GetCardType() => _type;
    public Color? GetColor() => _color;
    public Number? GetNumber() => _number;
    public ActionType? GetActionType() => _actionType;
    public WildType? GetWildType() => _wildType;

    public void GetDisplayText()
    {
      Console.Write("Card: ");

      switch (_type)
      {
        case CardType.Number:
          Console.WriteLine($"{_color} {_number}");
          break;
        case CardType.Action:
          Console.WriteLine($"{_color} {_actionType}");
          break;
        case CardType.Wild:
          Console.WriteLine($"{_wildType}");
          break;
      }
    }

    public void SetCardType(CardType type) => _type = type;
    public void SetColor(Color? color) => _color = color;
    public void SetNumber(Number? number) => _number = number;
    public void SetActionType(ActionType? actionType) => _actionType = actionType;
    public void SetWildType(WildType? wildType) => _wildType = wildType;
  }
  public class Player : IPlayer
  {
    private string _name;

    public string GetName() => _name;
    public void SetName(string name) => _name = name;
  }
  public class Deck : IDeck
  {
    private List<ICard> _cards = new List<ICard>();

    public List<ICard> GetCards() => _cards;
    public ICard GetCardAt(int index) => _cards[index];
    public void SetCards(List<ICard> cards) => _cards = cards;
    public void SetCardAt(int index, ICard card) => _cards[index] = card;
  }
  public class DiscardPile : IDiscardPile
  {
    private List<ICard> _cards = new List<ICard>();

    public List<ICard> GetCards() => _cards;
    public ICard GetCardAt(int index) => _cards[index];
    public void SetCards(List<ICard> cards) => _cards = cards;
    public void SetCardAt(int index, ICard card) => _cards[index] = card;
  }


  public class GameController
  {
    // Fields
    private List<IPlayer> _players = new List<IPlayer>();
    private Dictionary<IPlayer, List<ICard>> _playerHands = new Dictionary<IPlayer, List<ICard>>();
    private IDeck _deck;
    private IDiscardPile _discardPile;
    private int _currentPlayerIndex = 0;
    private bool _isClockwise = true;
    private Color? _currentWildColor = null;

    // Events / Delegates
    public Action<IPlayer> OnPlayerTurnChanged;
    public Action<IPlayer, ICard> OnCardPlayed;
    public Action<IPlayer> OnUnoViolation;
    public Action<IPlayer> OnGameEnded;

    public Func<IPlayer, ICard, List<ICard>, ICard> CardChooser;
    public Func<Color> WildColorChooser;
    public Func<IPlayer, bool> UnoCallChecker;

    // Constructor (optional, tidak disebut di class diagram)
    public GameController(IDeck deck, IDiscardPile discardPile)
    {
      _deck = deck;
      _discardPile = discardPile;
    }

    // Methods
    public void AddPlayer(IPlayer player)
    {
      _players.Add(player);
      _playerHands[player] = new List<ICard>();
    }

    public void StartGame()
    {
      InitializeDeck();
      DealCardsToPlayers();
      GameLoop();
    }

    private void InitializeDeck()
    {
      // Placeholder: isi deck secara manual nanti
      // sesuai peraturan UNO jika dibutuhkan
    }

    public void GameLoop()
    {
      while (!IsGameOver())
      {
        IPlayer currentPlayer = _players[_currentPlayerIndex];
        OnPlayerTurnChanged?.Invoke(currentPlayer);

        List<ICard> playerHand = _playerHands[currentPlayer];
        ICard topCard = GetTopDiscardCard();
        List<ICard> playableCards = GetPlayableCardsFromPlayer(currentPlayer, topCard);

        ICard chosenCard = ChooseCard(currentPlayer, topCard, playableCards);
        if (chosenCard != null && PlayCard(currentPlayer, chosenCard))
        {
          OnCardPlayed?.Invoke(currentPlayer, chosenCard);
        }
        else
        {
          ICard drawn = DrawCardFromDeck(currentPlayer);
          if (CanPlayCard(drawn, topCard))
            PlayCard(currentPlayer, drawn);
        }

        CheckUnoViolation(currentPlayer);
        NextPlayer();
      }

      OnGameEnded?.Invoke(GetWinner());
    }

    public void NextPlayer()
    {
      if (_isClockwise)
        _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;
      else
        _currentPlayerIndex = (_currentPlayerIndex - 1 + _players.Count) % _players.Count;
    }

    public bool IsGameOver()
    {
      foreach (var hand in _playerHands.Values)
      {
        if (hand.Count == 0)
          return true;
      }
      return false;
    }

    public IPlayer? GetWinner()
    {
      foreach (var entry in _playerHands)
      {
        if (entry.Value.Count == 0)
          return entry.Key;
      }
      return null;
    }

    public void AddCardToPlayer(IPlayer player, ICard card)
    {
      if (_playerHands.ContainsKey(player))
        _playerHands[player].Add(card);
    }

    public bool RemoveCardFromPlayer(IPlayer player, ICard card)
    {
      if (_playerHands.ContainsKey(player))
        return _playerHands[player].Remove(card);
      return false;
    }

    public int GetPlayerHandSize(IPlayer player)
    {
      if (_playerHands.ContainsKey(player))
        return _playerHands[player].Count;
      return 0;
    }

    public bool PlayerHasCard(IPlayer player, ICard card)
    {
      if (_playerHands.ContainsKey(player))
        return _playerHands[player].Contains(card);
      return false;
    }

    public List<ICard> GetPlayerHand(IPlayer player)
    {
      if (_playerHands.ContainsKey(player))
        return _playerHands[player];
      return new List<ICard>();
    }

    public void ClearPlayerHand(IPlayer player)
    {
      if (_playerHands.ContainsKey(player))
        _playerHands[player].Clear();
    }

    public bool PlayCard(IPlayer player, ICard card)
    {
      if (!PlayerHasCard(player, card))
        return false;

      ICard topCard = GetTopDiscardCard();

      if (!CanPlayCard(card, topCard))
        return false;

      RemoveCardFromPlayer(player, card);
      AddCardToDiscardPile(card);
      ExecuteCardEffect(card);

      return true;
    }

    public ICard DrawCardFromDeck(IPlayer player)
    {
      if (IsDeckEmpty())
        RecycleDiscardPile();

      var card = _deck.GetCardAt(0);
      _deck.GetCards().RemoveAt(0);
      AddCardToPlayer(player, card);
      return card;
    }

    public void AddCardToDeck(ICard card)
    {
      _deck.GetCards().Add(card);
    }

    public int GetDeckCardCount()
    {
      return _deck.GetCards().Count;
    }

    public bool IsDeckEmpty()
    {
      return _deck.GetCards().Count == 0;
    }

    public void ShuffleDeck()
    {
      var cards = _deck.GetCards();
      var rng = new Random();
      int n = cards.Count;
      while (n > 1)
      {
        n--;
        int k = rng.Next(n + 1);
        var value = cards[k];
        cards[k] = cards[n];
        cards[n] = value;
      }
    }

    public void AddCardToDiscardPile(ICard card)
    {
      _discardPile.GetCards().Add(card);
    }

    public ICard? GetTopDiscardCard()
    {
      var cards = _discardPile.GetCards();
      if (cards.Count == 0)
        return null;
      return cards[^1]; // cards[cards.Count - 1]
    }

    public int GetDiscardPileCardCount()
    {
      return _discardPile.GetCards().Count;
    }

    public bool IsDiscardPileEmpty()
    {
      return _discardPile.GetCards().Count == 0;
    }

    public List<ICard> GetPlayableCardsFromPlayer(IPlayer player, ICard topCard)
    {
      var playable = new List<ICard>();
      foreach (var card in GetPlayerHand(player))
      {
        if (CanPlayCard(card, topCard))
          playable.Add(card);
      }
      return playable;
    }

    public ICard ChooseCard(IPlayer player, ICard topCard, List<ICard> playableCards)
    {
      if (CardChooser != null)
        return CardChooser(player, topCard, playableCards);
      return null;
    }

    public void ExecuteCardEffect(ICard card)
    {
      switch (card.GetCardType())
      {
        case CardType.Action:
          switch (card.GetActionType())
          {
            case ActionType.Skip:
              NextPlayer();
              break;
            case ActionType.Reverse:
              ReverseDirection();
              break;
            case ActionType.DrawTwo:
              NextPlayer();
              DrawCardFromDeck(_players[_currentPlayerIndex]);
              DrawCardFromDeck(_players[_currentPlayerIndex]);
              break;
          }
          break;
        case CardType.Wild:
          _currentWildColor = ChooseWildColor();
          if (card.GetWildType() == WildType.WildDrawFour)
          {
            NextPlayer();
            DrawCardFromDeck(_players[_currentPlayerIndex]);
            DrawCardFromDeck(_players[_currentPlayerIndex]);
            DrawCardFromDeck(_players[_currentPlayerIndex]);
            DrawCardFromDeck(_players[_currentPlayerIndex]);
          }
          break;
      }
    }

    public bool CallUno(IPlayer player)
    {
      return UnoCallChecker != null && UnoCallChecker(player);
    }

    public bool CheckUnoViolation(IPlayer player)
    {
      if (GetPlayerHandSize(player) == 1 && !CallUno(player))
      {
        OnUnoViolation?.Invoke(player);
        PenalizePlayer(player);
        return true;
      }
      return false;
    }

    public void PenalizePlayer(IPlayer player)
    {
      DrawCardFromDeck(player);
      DrawCardFromDeck(player);
    }

    public bool ValidateCard(ICard card)
    {
      return card != null && card.GetCardType() != null;
    }

    public bool CanPlayCard(ICard card, ICard topCard)
    {
      if (!ValidateCard(card) || !ValidateCard(topCard))
        return false;

      if (card.GetCardType() == CardType.Wild)
        return true;

      if (card.GetColor() == topCard.GetColor())
        return true;

      if (card.GetCardType() == CardType.Number && card.GetNumber() == topCard.GetNumber())
        return true;

      if (card.GetCardType() == CardType.Action && card.GetActionType() == topCard.GetActionType())
        return true;

      return false;
    }

    public void DealCardsToPlayers()
    {
      const int initialCards = 7;
      for (int i = 0; i < initialCards; i++)
      {
        foreach (var player in _players)
        {
          DrawCardFromDeck(player);
        }
      }

      // Letakkan satu kartu awal ke discard pile
      if (!IsDeckEmpty())
      {
        var firstCard = _deck.GetCardAt(0);
        _deck.GetCards().RemoveAt(0);
        AddCardToDiscardPile(firstCard);
      }
    }

    public void ReverseDirection()
    {
      _isClockwise = !_isClockwise;
    }

    public Color ChooseWildColor()
    {
      if (WildColorChooser != null)
        return WildColorChooser();
      return Color.Red; // default fallback
    }

    public void RecycleDiscardPile()
    {
      var cards = _discardPile.GetCards();
      if (cards.Count <= 1) return;

      var top = cards[^1];
      cards.RemoveAt(cards.Count - 1);

      _deck.GetCards().AddRange(cards);
      ShuffleDeck();

      _discardPile.SetCards(new List<ICard> { top });
    }

    public IPlayer GetCurrentPlayer()
    {
      return _players[_currentPlayerIndex];
    }

    public List<Color> GetValidColors()
    {
      return new List<Color> { Color.Red, Color.Blue, Color.Green, Color.Yellow };
    }

    public IPlayer? GetPlayerByName(string name)
    {
      foreach (var player in _players)
      {
        if (player.GetName() == name)
          return player;
      }
      return null;
    }

    public List<int> GetPlayerHandSizes()
    {
      var result = new List<int>();
      foreach (var hand in _playerHands.Values)
      {
        result.Add(hand.Count);
      }
      return result;
    }

    public List<IPlayer> GetAllPlayers()
    {
      return _players;
    }


  }
}


