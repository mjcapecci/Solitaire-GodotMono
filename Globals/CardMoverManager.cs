using Godot;

using Solitaire.Classes.Enums;

using System;
using System.Linq;

public partial class CardMoverManager : Node
{

  public static CardMoverManager Instance;

  private Area2D _stockPile;
  private Area2D _wastePile;

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    Instance = this;
    _stockPile = GetNode<Area2D>("/root/Table/Deck/StockPile");
    _wastePile = GetNode<Area2D>("/root/Table/Deck/WastePile");
    ActionManager.Instance.OnTablueaPlayed += OnTableauPlayed;
    ActionManager.Instance.OnFoundationPlayed += OnFoundationPlayed;
    ActionManager.Instance.OnCardDrawn += OnCardDrawn;
    ActionManager.Instance.OnStockPileReset += OnStockpileReset;
    ActionManager.Instance.OnCardPositionReset += OnCardPositionReset;
  }

  public override void _ExitTree()
  {
    ActionManager.Instance.OnTablueaPlayed -= OnTableauPlayed;
    ActionManager.Instance.OnFoundationPlayed -= OnFoundationPlayed;
    ActionManager.Instance.OnCardDrawn -= OnCardDrawn;
    ActionManager.Instance.OnStockPileReset -= OnStockpileReset;
    ActionManager.Instance.OnCardPositionReset -= OnCardPositionReset;
  }

  private void OnCardDrawn(Card card)
  {
    card.FlipCard(true);
    card.GetParent()?.RemoveChild(card);
    _wastePile.AddChild(card);

    // Ensure only three cards are visible in the waste pile
    var visibleCards = _wastePile.GetChildren().OfType<Card>().Reverse().Take(3).ToList();
    for (int i = 0; i < visibleCards.Count; i++)
    {
      visibleCards[i].Restore();
      visibleCards[i].Position = new Vector2(0, 35 * i);
      visibleCards[i].IsDraggable = (i == 0);
      visibleCards[i].ZIndex = visibleCards.Count - i;
    }

    var allCards = _wastePile.GetChildren().OfType<Card>().ToList();
    foreach (var c in allCards)
    {
      if (!visibleCards.Contains(c))
      {
        c.Discard();
      }
    }
  }

  private void OnStockpileReset()
  {
    foreach (var child in _wastePile.GetChildren())
    {
      if (child is Card card)
      {
        card.GetParent()?.RemoveChild(card);
        _stockPile.AddChild(card);
        card.Position = Vector2.Zero;
        card.Restore();
        card.FlipCard(false);
        card.IsDraggable = false;
      }
    }

    DeckManager.FlipStockpile(_stockPile);
  }

  private void OnTableauPlayed(Card card, TableauZone zone)
  {
    MoveCardToZone(card, zone);
    var visibleCards = _wastePile.GetChildren().OfType<Card>().Reverse().Take(3).ToList();
    for (int i = 0; i < visibleCards.Count; i++)
    {
      visibleCards[i].Restore();
      visibleCards[i].Position = new Vector2(0, 35 * i);
      visibleCards[i].IsDraggable = (i == 0);
      visibleCards[i].ZIndex = visibleCards.Count - i;
    }
    var allCards = _wastePile.GetChildren().OfType<Card>().ToList();
    foreach (var c in allCards)
    {
      if (!visibleCards.Contains(c))
      {
        c.Discard();
      }
    }
  }

  private void OnFoundationPlayed(Card card, FoundationZone zone)
  {
    MoveCardToZone(card, zone);
    var visibleCards = _wastePile.GetChildren().OfType<Card>().Reverse().Take(3).ToList();
    for (int i = 0; i < visibleCards.Count; i++)
    {
      visibleCards[i].Restore();
      visibleCards[i].Position = new Vector2(0, 35 * i);
      visibleCards[i].IsDraggable = (i == 0);
      visibleCards[i].ZIndex = visibleCards.Count - i;
    }
    var allCards = _wastePile.GetChildren().OfType<Card>().ToList();
    foreach (var c in allCards)
    {
      if (!visibleCards.Contains(c))
      {
        c.Discard();
      }
    }
  }

  private void MoveCardToZone(Card card, Node zone)
  {
    var previousZone = card.GetParent();

    if (card.GetParent() != zone)
    {
      card.GetParent()?.RemoveChild(card);
      zone.AddChild(card);
    }

    card.Position = (zone as IZone).GetNextCardPosition();

    if (previousZone is TableauZone tableauZone)
    {
      var topCard = tableauZone.GetTopCard();
      if (topCard != null && !topCard.IsFaceUp)
      {
        topCard.FlipCard(true);
        topCard.IsDraggable = true;
      }
    }

    // Re-increment the z-indices of all children cards in the zone
    for (int i = 1; i < zone.GetChildCount(); i++)
    {

      var childCard = zone.GetChild<Card>(i);
      childCard.ZIndex = i;
    }
  }

  private void OnCardPositionReset(Card card)
  {
    var zone = card.GetParent();
    for (int i = 1; i < zone.GetChildCount(); i++)
    {
      var childCard = zone.GetChild<Card>(i);
      childCard.ZIndex = i;
    }
  }
}
