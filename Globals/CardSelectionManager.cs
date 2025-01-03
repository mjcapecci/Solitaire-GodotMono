using Godot;

using System.Collections.Generic;
using System.Linq;

public partial class CardSelectionManager : Node2D
{
  public static CardSelectionManager Instance;

  private Card _selectedCard = null;
  private List<Card> _selectedPile = new List<Card>();
  private Dictionary<Card, Vector2> _initialPositions = new Dictionary<Card, Vector2>();
  private Vector2 _mouseOffset;

  public override void _Ready()
  {
    Instance = this;
  }

  public override void _Input(InputEvent @event)
  {
    if (@event is InputEventMouseButton mouseEvent && mouseEvent.ButtonIndex == MouseButton.Left)
    {
      if (mouseEvent.Pressed)
      {
        HandleMouseClick(mouseEvent.GlobalPosition);
      }
    }

    if (@event is InputEventMouseButton rightClickEvent && rightClickEvent.ButtonIndex == MouseButton.Right)
    {
      if (rightClickEvent.IsReleased())
      {
        HandleRightClick(rightClickEvent.GlobalPosition);
      }
    }

    if (_selectedCard != null && @event is InputEventMouseMotion mouseMotionEvent)
    {
      MoveSelectedPileTo(mouseMotionEvent.GlobalPosition);
    }
  }

  public void HandleMouseClick(Vector2 mousePosition)
  {
    if (_selectedCard == null)
    {
      TrySelectCard(mousePosition);
    }
    else
    {
      TryDropOrDeselectCard(mousePosition);
    }
  }

  private void TrySelectCard(Vector2 mousePosition)
  {
    var spaceState = GetTree().Root.GetWorld2D().DirectSpaceState;

    var queryParameters = new PhysicsPointQueryParameters2D
    {
      CollideWithAreas = true,
      CollideWithBodies = false,
      Position = mousePosition
    };

    var results = spaceState.IntersectPoint(queryParameters);

    Card topCard = null;
    int topZIndex = int.MinValue;

    foreach (var result in results)
    {
      if (result["collider"].As<Area2D>() is Area2D area && area.GetParent() is Card card && card.IsDraggable)
      {
        // Prioritize the card with the highest ZIndex
        if (card.ZIndex > topZIndex)
        {
          topCard = card;
          topZIndex = card.ZIndex;
        }
      }
    }

    if (topCard != null)
    {
      SelectCard(topCard, mousePosition);
    }
  }


  private void SelectCard(Card card, Vector2 mousePosition)
  {
    _selectedCard = card;
    _selectedPile = (card.GetParent() as TableauZone)?.GetCardsStartingFrom(card) ?? new List<Card> { card };
    _mouseOffset = card.GlobalPosition - mousePosition;

    // Store initial positions for snapping back
    _initialPositions.Clear();
    foreach (var pileCard in _selectedPile)
    {
      _initialPositions[pileCard] = pileCard.GlobalPosition;
      pileCard.BringToFront();
    }
  }

  private void TryDropOrDeselectCard(Vector2 mousePosition)
  {
    // Check for valid drop zone
    var dropZone = FindValidDropZone(mousePosition);


    if (dropZone != null && dropZone.CanAcceptCard(_selectedCard))
    {
      PlaceCardsInZone(dropZone);
    }
    else
    {
      SnapBackToInitialPositions();
    }

    DeselectCard();
  }

  private void MoveSelectedPileTo(Vector2 targetPosition)
  {
    foreach (var card in _selectedPile)
    {
      card.GlobalPosition = targetPosition;
      targetPosition.Y += 35; // Adjust vertical offset for stacked cards
    }
  }

  private IZone FindValidDropZone(Vector2 mousePosition)
  {
    foreach (var node in GetTree().GetNodesInGroup("DropZones"))
    {
      if (node is TableauZone tableauZone)
      {
        // Ignore the initial drop zone of the selected card
        if (_selectedCard != null && tableauZone == _selectedCard.GetParent())
        {
          continue;
        }

        var topCard = tableauZone.GetTopCard();
        if (topCard != null && mousePosition.DistanceTo(topCard.GlobalPosition) < 50)
        {
          return tableauZone;
        }

        if (topCard == null && mousePosition.DistanceTo(tableauZone.GlobalPosition) < 50)
        {
          return tableauZone;
        }
      }
      else if (node is FoundationZone foundationZone)
      {
        // Ignore the initial drop zone of the selected card
        if (_selectedCard != null && foundationZone == _selectedCard.GetParent())
        {
          continue;
        }

        // Foundation zones can only accept one card at a time
        if (_selectedPile.Count == 1)
        {
          var topCard = foundationZone.GetTopCard();
          if (topCard != null && mousePosition.DistanceTo(topCard.GlobalPosition) < 50)
          {
            return foundationZone;
          }

          if (topCard == null && mousePosition.DistanceTo(foundationZone.GlobalPosition) < 50)
          {
            return foundationZone;
          }
        }
      }
    }

    return null;
  }

  private void PlaceCardsInZone(IZone dropZone)
  {
    if (dropZone is TableauZone)
    {
      foreach (var card in _selectedPile)
      {
        ActionManager.EmitTableauPlayed(card, (TableauZone) dropZone);
      }
    }

    if (dropZone is FoundationZone)
    {
      foreach (var card in _selectedPile)
      {
        ActionManager.EmitFoundationPlayed(card, (FoundationZone) dropZone);
      }
    }
  }

  private void SnapBackToInitialPositions()
  {
    foreach (var card in _selectedPile)
    {
      if (_initialPositions.TryGetValue(card, out var initialPosition))
      {
        var tween = CreateTween();
        tween.TweenProperty(card, "global_position", initialPosition, 0.1f).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.InOut);
        tween.Finished += () =>
        {
          // Re-increment the ZIndices of all child cards in the initial pile
          if (_selectedCard?.GetParent() is TableauZone tableauZone)
          {
            var cards = tableauZone.GetChildren().OfType<Card>().ToList();

            for (int i = 0; i < cards.Count; i++)
            {
              cards[i].ZIndex = i;
            }
          }

          if (_selectedCard?.GetParent() is FoundationZone foundationZone)
          {
            var cards = foundationZone.GetChildren().OfType<Card>().ToList();

            for (int i = 0; i < cards.Count; i++)
            {
              cards[i].ZIndex = i;
            }
          }
        };
      }
    }
  }

  private void HandleRightClick(Vector2 mousePosition)
  {
    var spaceState = GetTree().Root.GetWorld2D().DirectSpaceState;

    var queryParameters = new PhysicsPointQueryParameters2D
    {
      CollideWithAreas = true,
      CollideWithBodies = false,
      Position = mousePosition
    };

    var results = spaceState.IntersectPoint(queryParameters);

    Card topCard = null;
    int topZIndex = int.MinValue;


    foreach (var result in results)
    {
      if (result["collider"].As<Area2D>() is Area2D area && area.GetParent() is Card card && card.IsDraggable)
      {

        if (card.ZIndex > topZIndex)
        {
          topCard = card;
          topZIndex = card.ZIndex;
        }

        if (topCard.GetParent() is TableauZone tableauZone && tableauZone.GetTopCard() != topCard)
        {
          // If the card is in a tableau zone and is not the top card, do nothing
          return;
        }

        if (TryAutoMoveToFoundation(topCard))
        {
          return;
        }
      }
    }
  }

  private bool TryAutoMoveToFoundation(Card card)
  {
    foreach (var node in GetTree().GetNodesInGroup("FoundationZones"))
    {
      if (node is FoundationZone foundationZone && foundationZone.CanAcceptCard(card))
      {
        ActionManager.EmitFoundationPlayed(card, foundationZone);
        return true;
      }
    }

    return false;
  }

  private void DeselectCard()
  {
    ActionManager.EmitCardPositionReset(_selectedCard);
    _selectedCard = null;
    _selectedPile.Clear();
  }
}
