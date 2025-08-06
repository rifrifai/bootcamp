using UnoRevisi.Interfaces;

namespace UnoRevisi.Models;

public class DiscardPile : IDiscardPile
{
  private List<ICard> _cards;

  public DiscardPile()
  {
    _cards = new List<ICard>();
  }

  public List<ICard> GetCards() => _cards;
  public void SetCards(List<ICard> cards) => _cards = cards;
  public ICard GetCardAt(int index) => _cards[index];
  public void SetCardAt(int index, ICard card) => _cards[index] = card;
}
