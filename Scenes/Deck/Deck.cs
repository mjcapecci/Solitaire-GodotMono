using Godot;

using Solitaire.Classes.Enums;

using System.Collections.Generic;
using System.Linq;

public partial class Deck : Area2D
{
  private CollisionShape2D _collisionShape;
  private List<TableauZone> _tableauZones;

  public override void _Ready()
  {
    _collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");

    // Locate Tableau Zones
    _tableauZones = new List<TableauZone>();
    for (int i = 1; i <= 7; i++) // Assuming nodes are named TableauZone1, TableauZone2, etc.
    {
      var tableauZone = GetNode<TableauZone>($"../TableauZone{i}");
      _tableauZones.Add(tableauZone);
    }

    // Shuffle and populate the deck
    var shuffledDeck = DeckManager.ShuffleDeck();
    PopulateStockPile(shuffledDeck);
  }

  public override void _UnhandledInput(InputEvent @event)
  {
    if (@event is InputEventMouseButton mouseEvent)
    {
      if (mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
      {
        var mousePosition = mouseEvent.GlobalPosition;
        if (_collisionShape.GlobalPosition.DistanceTo(mousePosition) <= 100)
        {
          OnStockPileClicked();
        }
      }
    }
  }

  private void PopulateStockPile(Dictionary<CardDetails, CardPile> shuffledDeck)
  {
    foreach (var cardDetails in shuffledDeck.Keys)
    {
      var card = DeckManager.CreateCardInstance(cardDetails);
      AddChild(card);

      // Place all cards stacked at the StockPile position
      card.GlobalPosition = _collisionShape.GlobalPosition;
      card.FlipCard(false); // Start face-down
    }
  }

  private void OnStockPileClicked()
  {
    if (!DeckManager.HasCardsInPile(CardPile.Tableau))
    {
      DealToTableauZones();
    }
    else if (DeckManager.HasCardsInPile(CardPile.StockPile))
    {
      DrawFromStock();
    }
    else
    {
      ResetWasteToStock();
    }
  }

  private void DealToTableauZones()
  {
    int tableauIndex = 0;
    foreach (var tableauZone in _tableauZones)
    {
      int cardsToDeal = tableauIndex + 1;

      for (int i = 0; i < cardsToDeal; i++)
      {
        var stockCards = DeckManager.GetCardsInPile(CardPile.StockPile);
        if (stockCards.Count == 0)
          break;

        var card = stockCards.Last();
        DeckManager.UpdateCardPile(card, CardPile.Tableau);

        // Emit the action to move the card to the tableau zone
        ActionManager.EmitTableauPlayed(card, tableauZone);

        // Flip only the topmost card
        card.FlipCard(i == tableauIndex);

        // Make the top card draggable
        card.IsDraggable = i == tableauIndex;
      }

      tableauIndex++;
    }
  }

  private void DrawFromStock()
  {
    var stockCards = DeckManager.GetCardsInPile(CardPile.StockPile);
    if (stockCards.Count == 0)
      return;

    var card = stockCards[stockCards.Count - 1];
    ActionManager.EmitCardDrawn(card);
  }

  private void ResetWasteToStock()
  {
    ActionManager.EmitStockPileReset();
  }
}
