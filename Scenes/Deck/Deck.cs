using Godot;

using Solitaire.Classes;
using Solitaire.Classes.Enums;

using System.Collections.Generic;

public partial class Deck : Node2D
{
  private CollisionShape2D _collisionShape;
  private List<TableauZone> _tableauZones;

  public override void _Ready()
  {
    // Locate StockPile and WastePile nodes

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

  private void PopulateStockPile(Dictionary<CardDetails, CardPile> shuffledDeck)
  {
    foreach (var cardDetails in shuffledDeck.Keys)
    {
      var card = DeckManager.CreateCardInstance(cardDetails);
      // add to roote node called table
      AddChild(card);

      // Place all cards stacked at the StockPile position
      card.GlobalPosition = _collisionShape.GlobalPosition;
      card.FlipCard(false); // Start face-down
    }
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


  public void OnStockPileClicked()
  {
    if (DeckManager.CanDealToTableauZones())
    {
      DeckManager.DealToTableauZones(_tableauZones);
    }
    else if (DeckManager.CanDrawFromStock())
    {
      DeckManager.DrawFromStock();
    }
    else
    {
      DeckManager.ResetWasteToStock();
    }
  }
}
