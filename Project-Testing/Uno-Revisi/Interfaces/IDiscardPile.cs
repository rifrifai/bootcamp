namespace UnoRevisi.Interfaces;

public interface IDiscardPile
{
  public List<ICard> GetCards();
  public ICard GetCardAt(int index);
  public void SetCards(List<ICard> cards);
  public void SetCardAt(int index, ICard card);
}