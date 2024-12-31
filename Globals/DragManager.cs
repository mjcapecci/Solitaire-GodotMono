using Godot;

using System.Collections.Generic;

public class DragState
{
  public Card DraggedCard { get; set; }
  public List<Card> DraggedPile { get; set; } = new List<Card>();
  public Vector2 DragOffset { get; set; }
  public Dictionary<Card, Vector2> InitialPositions { get; set; } = new Dictionary<Card, Vector2>();
  public Node OriginalParent { get; set; }
}


public partial class DragManager : Node
{
  public static DragManager Instance;

  private DragState _currentDrag = null;

  public override void _Ready()
  {
    Instance = this;
  }

  public static void StartDrag(Card card, Node originalParent, Vector2 dragOffset)
  {
    Instance._currentDrag = new DragState
    {
      DraggedCard = card,
      OriginalParent = originalParent,
      DragOffset = dragOffset,
      DraggedPile = originalParent is TableauZone tableauZone ? tableauZone.GetCardsStartingFrom(card) : new List<Card> { card }
    };

    foreach (var draggedCard in Instance._currentDrag.DraggedPile)
    {
      Instance._currentDrag.InitialPositions[draggedCard] = draggedCard.Position;
      draggedCard.BringToFront();
    }
  }

  public static void UpdateDrag(Vector2 mouseMotion)
  {
    if (Instance._currentDrag == null) return;

    foreach (var card in Instance._currentDrag.DraggedPile)
    {
      card.Position += mouseMotion;
    }
  }

  public static void EndDrag(Vector2 mousePosition)
  {
    if (Instance._currentDrag == null) return;

    var dropZone = Instance.FindValidDropZone(Instance._currentDrag.DraggedCard, mousePosition);

    if (dropZone != null && dropZone.CanAcceptCard(Instance._currentDrag.DraggedCard))
    {
      Instance.PlaceCardsInZone(dropZone);
    }
    else
    {
      Instance.ResetDraggedCardsToInitialPosition();
    }

    Instance.ResetDrag();
  }

  private TableauZone FindValidDropZone(Card card, Vector2 mousePosition)
  {
    TableauZone bestZone = null;

    // Check for overlapping cards first
    foreach (var node in GetTree().GetNodesInGroup("TableauZones"))
    {
      if (node is TableauZone tableauZone)
      {
        var topCard = tableauZone.GetTopCard();
        if (topCard != null && mousePosition.DistanceTo(topCard.GlobalPosition) < 100)
        {
          bestZone = tableauZone;
          break; // Prioritize the card's collider
        }
      }
    }

    // If no overlapping card is found, check for an empty zone
    if (bestZone == null)
    {
      foreach (var node in GetTree().GetNodesInGroup("TableauZones"))
      {
        if (node is TableauZone tableauZone)
        {
          var topCard = tableauZone.GetTopCard();
          if (topCard == null && mousePosition.DistanceTo(tableauZone.GlobalPosition) < 100)
          {
            bestZone = tableauZone;
            break;
          }
        }
      }
    }

    return bestZone;
  }

  private void PlaceCardsInZone(TableauZone dropZone)
  {
    foreach (var card in _currentDrag.DraggedPile)
    {
      GD.Print("Placing card in zone");
      ActionManager.EmitTableauPlayed(card, dropZone);
    }
  }

  private void ResetDraggedCardsToInitialPosition()
  {
    foreach (var card in _currentDrag.DraggedPile)
    {
      if (_currentDrag.InitialPositions.TryGetValue(card, out var initialPosition))
      {
        card.Position = initialPosition;
      }
    }
  }


  private void ResetDrag()
  {
    foreach (var card in _currentDrag.DraggedPile)
    {
      card.ZIndex = 1;
    }
    _currentDrag = null;
  }
}
