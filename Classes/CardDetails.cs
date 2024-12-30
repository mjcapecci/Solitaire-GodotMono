using System;

public partial class CardDetails
{
  public string Suit { get; set; }
  public string Rank { get; set; }
  public string Color { get; set; }
  public string TexturePath { get; set; }

  public CardDetails(string suit, string rank, string color, string texturePath)
  {
    Suit = suit;
    Rank = rank;
    Color = color;
    TexturePath = texturePath;
  }

  public override string ToString()
  {
    return $"{Rank} of {Suit}";
  }

  public override bool Equals(object obj)
  {
    if (obj == null || GetType() != obj.GetType())
    {
      return false;
    }

    var other = (CardDetails) obj;
    return Suit == other.Suit && Rank == other.Rank;
  }

  public override int GetHashCode()
  {
    return HashCode.Combine(Suit, Rank);
  }
}
