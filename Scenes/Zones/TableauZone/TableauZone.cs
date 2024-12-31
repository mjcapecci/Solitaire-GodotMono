using Godot;

using System.Collections.Generic;

public partial class TableauZone : Area2D
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
    int cardCount = 0;
    foreach (var child in GetChildren())
    {
      if (child is Card)
      {
        cardCount++;
      }
    }
    return new Vector2(0, (cardCount - 1) * 35);
  }

  public List<Card> GetCardsStartingFrom(Card startingCard)
  {
    var cards = new List<Card>();
    bool collect = false;

    foreach (var child in GetChildren())
    {
      if (child == startingCard)
      {
        collect = true;
      }

      if (collect && child is Card card)
      {
        cards.Add(card);
      }
    }

    return cards;
  }


  public bool CanAcceptCard(Card card)
  {
    var topCard = GetTopCard();

    if (topCard == null)
    {
      // Only Kings can be placed in an empty tableau
      return card.Rank == Solitaire.Classes.Enums.Rank.King;
    }

    // Validate Solitaire rules
    return IsValidDrop(card, topCard);
  }

  private bool IsValidDrop(Card cardToDrop, Card targetCard)
  {
    bool isAlternatingColor = cardToDrop.SuitDetails.Color != targetCard.SuitDetails.Color;
    bool isDescending = cardToDrop.Rank == targetCard.Rank - 1;

    return isAlternatingColor && isDescending;
  }
}
