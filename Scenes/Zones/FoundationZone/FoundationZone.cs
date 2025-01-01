using Godot;

using System.Collections.Generic;

public partial class FoundationZone : Area2D, IZone
{
  private CollisionShape2D _collisionShape;

  public override void _Ready()
  {
    _collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
  }

  public Card GetTopCard()
  {
    var children = GetChildren();
    for (int i = children.Count - 1; i >= 0; i--)
    {
      if (children[i] is Card card)
      {
        return card; // Return the topmost card
      }
    }
    return null; // No cards in this zone
  }

  public Vector2 GetNextCardPosition()
  {
    return new Vector2(0, 0);
  }


  public bool CanAcceptCard(Card card)
  {
    var topCard = GetTopCard();

    if (topCard == null)
    {
      // Only Aces can be placed in an empty foundation
      return card.Rank == Solitaire.Classes.Enums.Rank.Ace;
    }

    // Validate Solitaire foundation rules
    return IsValidDrop(card, topCard);
  }

  private bool IsValidDrop(Card cardToDrop, Card targetCard)
  {
    bool isSameSuit = cardToDrop.Suit == targetCard.Suit;
    bool isAscending = cardToDrop.Rank == targetCard.Rank + 1;

    return isSameSuit && isAscending;
  }
}
