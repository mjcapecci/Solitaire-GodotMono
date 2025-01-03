using Solitaire.Classes.Enums;

namespace Solitaire.Classes
{
  public enum Color
  {
    Red,
    Black
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
