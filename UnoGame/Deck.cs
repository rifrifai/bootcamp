namespace UnoGame;

public class Deck
{
  private List<ICard> _cards;


  public List<ICard> GetCards() => _cards;
  public void SetCards(List<ICard> cards) => SetCards(_cards);
  public ICard GetCardAt(int index) => GetCardAt(index);
  public void SetCardAt(int index, ICard card) => _cards = (List<ICard>)card;
}