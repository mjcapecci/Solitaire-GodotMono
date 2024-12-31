namespace Solitaire.Classes
{
  public enum Color
  {
    Red,
    Black
  }

  public enum Suit
  {
    Hearts = 1,
    Diamonds = 2,
    Clubs = 3,
    Spades = 4
  }

  public class SuitDetails
  {
    public Color Color { get; private set; }

    public SuitDetails(Suit title)
    {
      Color = (title == Suit.Hearts || title == Suit.Diamonds) ? Color.Red : Color.Black;
    }
  }
}
