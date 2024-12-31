using Godot;

using System.Collections.Generic;

public partial class TableauZone : Area2D, IZone
{
  private ZoneManager _zoneManager = new ZoneManager();
  private CollisionShape2D _collisionShape;

  public override void _Ready()
  {
    _collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
    AreaEntered += OnAreaEntered;
    AreaExited += OnAreaExited;

  }

  private void OnAreaEntered(Node area)
  {
    if (area.GetParent() is Card card)
    {
      _zoneManager.AddCard(card);
      UpdateActiveCollision();
    }
  }

  private void OnAreaExited(Node area)
  {
    if (area.GetParent() is Card card)
    {
      _zoneManager.RemoveCard(card);
      UpdateActiveCollision();
    }
  }

  public void AddCard(Card card)
  {
    _zoneManager.AddCard(card);
    UpdateActiveCollision();
  }

  public void RemoveCard(Card card)
  {
    _zoneManager.RemoveCard(card);
    UpdateActiveCollision();
  }

  private void UpdateActiveCollision()
  {
    if (_zoneManager.GetCardCount() > 0)
    {
      // Disable the zone's collision
      _collisionShape.Disabled = true;

      // Enable the last card's collision area
      var lastCard = _zoneManager.GetTopCard();
      if (lastCard != null)
      {
        lastCard.SetCollisionEnabled(true);
      }
    }
    else
    {
      // Enable the zone's collision
      _collisionShape.Disabled = false;
    }
  }

  public Card GetTopCard()
  {
    return _zoneManager.GetTopCard();
  }

  public List<Card> GetPileFromCard(Card startingCard)
  {
    return _zoneManager.GetPileFromCard(startingCard);
  }

  public bool CanAcceptCard(Card card)
  {
    var topCard = _zoneManager.GetTopCard();

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
    bool isAlternatingColor = cardToDrop.SuitDetails.Color == Solitaire.Classes.Color.Red
        ? targetCard.SuitDetails.Color == Solitaire.Classes.Color.Black
        : targetCard.SuitDetails.Color == Solitaire.Classes.Color.Red;

    bool isDescending = cardToDrop.Rank == targetCard.Rank - 1;

    return isAlternatingColor && isDescending;
  }
}
