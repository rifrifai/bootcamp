namespace UnoGame;

public class DiscardPile : IDiscardPile
{
  private List<ICard> _cards = [];


  public List<ICard> GetCards() => _cards;
  public ICard GetCardAt(int index) => _cards[index];
  public void SetCards(List<ICard> cards) => _cards = cards;
  public void SetCardAt(int index, ICard card)
  {
    if (index >= 0 && index < _cards.Count)
    {
      _cards[index] = card;
    }
  }
  public void AddCard(ICard card)
  {
    _cards.Add(card);
  }
}