using Godot;

using System.Collections.Generic;
using System.Linq;

public partial class Deck : Node2D
{

  private Area2D _stockpile;
  private Area2D _wastePile;
  private CollisionShape2D _stockpileCollisionShape;
  private CollisionShape2D _wastePileCollisionShape;
  private List<TableauZone> _tableauZones;

  public override void _Ready()
  {
    _stockpile = GetNode<Area2D>("StockPile");
    _wastePile = GetNode<Area2D>("WastePile");
    _stockpileCollisionShape = GetNode<CollisionShape2D>("StockPile/CollisionShape2D");
    _wastePileCollisionShape = GetNode<CollisionShape2D>("WastePile/CollisionShape2D");

    // Locate Tableau Zones
    _tableauZones = new List<TableauZone>();
    for (int i = 1; i <= 7; i++)
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
        if (_stockpileCollisionShape.GlobalPosition.DistanceTo(mousePosition) <= 50)
        {
          OnStockPileClicked();
        }
      }
    }
  }

  private void PopulateStockPile(List<CardDetails> shuffledDeck)
  {
    foreach (var cardDetails in shuffledDeck)
    {
      var card = DeckManager.CreateCardInstance(cardDetails);
      _stockpile.AddChild(card);

      // Place all cards stacked at the StockPile position
      card.GlobalPosition = _stockpileCollisionShape.GlobalPosition;
      card.FlipCard(false);
    }
  }

  private void OnStockPileClicked()
  {
    if (_tableauZones.All(zone => zone.GetChildren().OfType<Card>().Count() <= 0))
    {
      DealToTableauZones();
    }
    else if (_stockpile.GetChildren().OfType<Card>().Any())
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
        var stockCards = _stockpile.GetChildren().OfType<Card>().ToList();
        if (stockCards.Count == 0)
          break;

        var card = stockCards.Last();
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
    var card = _stockpile.GetChildren().OfType<Card>().Last();
    ActionManager.EmitCardDrawn(card);
  }

  private void ResetWasteToStock()
  {
    ActionManager.EmitStockPileReset();
  }
}
