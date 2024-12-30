using Godot;

using System;

public partial class Card : Node2D
{

  public static int GlobalTopZIndex = 1; // shared among all Cards
  private Sprite2D _cardSprite;
  private bool _isFaceUp = false;
  private string _suit;
  private string _rank;

  public void Initialize(CardDetails details)
  {
    _suit = details.Suit;
    _rank = details.Rank;
  }

  public override void _Ready()
  {
    _cardSprite = GetNode<Sprite2D>("CardSprite");
    // Set default texture as face-down
    _cardSprite.Texture = DeckManager.GetSupportTexture("card_back");
  }

  public void FlipCard()
  {
    _isFaceUp = !_isFaceUp;
    _cardSprite.Texture = _isFaceUp
        ? DeckManager.GetCardTexture($"card_{_suit}_{_rank}")
        : DeckManager.GetSupportTexture("card_back");
  }
}
