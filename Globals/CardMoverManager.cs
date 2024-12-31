using Godot;

using Solitaire.Classes.Enums;

using System;

public partial class CardMoverManager : Node
{

  public static CardMoverManager Instance;

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    Instance = this;
    ActionManager.Instance.OnTablueaPlayed += OnTableauPlayed;
    ActionManager.Instance.OnFoundationPlayed += OnFoundationPlayed;
    ActionManager.Instance.OnCardDrawn += OnCardDrawn;
  }

  public override void _ExitTree()
  {
    ActionManager.Instance.OnTablueaPlayed -= OnTableauPlayed;
    ActionManager.Instance.OnFoundationPlayed -= OnFoundationPlayed;
  }

  private void OnCardDrawn(Card card)
  {
    var wasteCards = DeckManager.GetCardsInPile(CardPile.WastePile);
    card.GlobalPosition = new Vector2(187, 240 + wasteCards.Count * 35);
    card.FlipCard(true); // Make the card face-up
    card.IsDraggable = true;
    DeckManager.UpdateCardPile(card, CardPile.WastePile);
  }

  private void OnStockpileReset()
  {
    var wasteCards = DeckManager.GetCardsInPile(CardPile.WastePile);
    foreach (var card in wasteCards)
    {
      card.GlobalPosition = GetParent().GetNode<CollisionShape2D>("CollisionShape2D").GlobalPosition;
      card.FlipCard(false); // Face-down
      card.IsDraggable = false;

      DeckManager.ResetWasteToStock();
    }
  }

  private void OnTableauPlayed(Card card, TableauZone zone)
  {
    var previousZone = card.GetParent() as TableauZone;

    if (card.GetParent() != zone)
    {
      card.GetParent()?.RemoveChild(card);
      zone.AddChild(card);
    }

    // Update card position
    card.Position = zone.GetNextCardPosition();


    if (previousZone != null)
    {
      var topCard = previousZone.GetTopCard();
      if (topCard != null && !topCard.IsFaceUp)
      {
        topCard.FlipCard(true); // Flip the card face-up
        topCard.IsDraggable = true;
      }
    }

    DeckManager.UpdateCardPile(card, CardPile.Tableau);
  }

  private void OnFoundationPlayed(Card card, FoundationZone zone)
  {
    card.GetParent().RemoveChild(card);
    zone.AddChild(card);

    DeckManager.UpdateCardPile(card, CardPile.Foundation);
  }
}
