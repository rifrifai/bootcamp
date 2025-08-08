namespace UnoRevisi.Interfaces;

public interface IDeck
{
  List<ICard> GetCards();
  ICard GetCardAt(int index);
  void SetCards(List<ICard> cards);
  void SetCardAt(int index, ICard card);
}