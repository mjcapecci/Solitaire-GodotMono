using Godot;

using System;
using System.Collections.Generic;
using System.Linq;

public partial class InputManager : Node2D
{
  private Card _draggedCard = null;
  private Vector2 _dragOffset;
  private List<Card> _draggedPile = new List<Card>();

  public override void _UnhandledInput(InputEvent @event)
  {
    if (@event is InputEventMouseButton mouseEvent)
    {
      HandleMouseButtonEvent(mouseEvent);
    }
    else if (@event is InputEventMouseMotion motionEvent && _draggedCard != null)
    {
      HandleCardDrag(motionEvent);
    }
  }

  private void HandleMouseButtonEvent(InputEventMouseButton mouseEvent)
  {
    if (mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
    {
      HandleLeftMousePressed(mouseEvent);
    }
    else if (!mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
    {
      HandleMouseRelease();
    }
  }

  private void HandleLeftMousePressed(InputEventMouseButton mouseEvent)
  {
    var spaceState = GetWorld2D().DirectSpaceState;

    var queryParameters = new PhysicsPointQueryParameters2D
    {
      CollideWithAreas = true,
      CollideWithBodies = false,
      CollisionMask = 1, // Adjust collision mask for cards
      Position = GetGlobalMousePosition()
    };

    var results = spaceState.IntersectPoint(queryParameters);

    Card topCard = null;
    int topZIndex = int.MinValue;

    foreach (var result in results)
    {
      if (result["collider"].As<Area2D>() is Area2D area && area.GetParent() is Card card)
      {
        if (card.IsDraggable && card.ZIndex > topZIndex)
        {
          topCard = card;
          topZIndex = card.ZIndex;
        }
      }
    }

    if (topCard != null)
    {
      _draggedCard = topCard;

      // Store the initial position for snapping back
      _draggedCard.InitialPosition = _draggedCard.Position;

      // Bring card to the front
      _draggedCard.BringToFront();

      // Check if the card is part of a pile
      if (_draggedCard.FindOverlappingDropZones().FirstOrDefault() is TableauZone tableauZone)
      {
        _draggedPile = tableauZone.GetPileFromCard(_draggedCard);
      }
      else
      {
        _draggedPile.Clear();
        _draggedPile.Add(_draggedCard); // Treat single card as pile
      }

      // Set drag offset for smooth dragging
      _dragOffset = _draggedCard.Position - mouseEvent.Position;
    }
  }

  private void HandleMouseRelease()
  {
    if (_draggedCard != null)
    {
      var overlappingZone = _draggedCard.FindOverlappingDropZones().FirstOrDefault();

      if (overlappingZone is TableauZone tableauZone)
      {
        // Validate if the cards can be dropped
        if (tableauZone.CanAcceptCard(_draggedCard))
        {
          // Snap the pile to the zone
          foreach (var card in _draggedPile)
          {
            card.SnapToZone(tableauZone);
            tableauZone.AddCard(card);
          }
        }
        else
        {
          // Invalid drop, return the pile to the initial position
          foreach (var card in _draggedPile)
          {
            card.SnapToInitialPosition();
          }
        }
      }
      else
      {
        // No valid drop zone, return the pile to the initial position
        foreach (var card in _draggedPile)
        {
          card.SnapToInitialPosition();
        }
      }

      // Clear references
      _draggedCard = null;
      _draggedPile.Clear();
    }
  }

  private void HandleCardDrag(InputEventMouseMotion motionEvent)
  {
    foreach (var card in _draggedPile)
    {
      card.Position += motionEvent.Relative; // Move all cards in the pile
    }
  }
}
