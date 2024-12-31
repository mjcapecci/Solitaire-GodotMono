using Godot;

public partial class InputManager : Node2D
{
  public override void _UnhandledInput(InputEvent @event)
  {
    if (@event is InputEventMouseButton mouseEvent)
    {
      HandleMouseButton(mouseEvent);
    }
    else if (@event is InputEventMouseMotion motionEvent)
    {
      DragManager.UpdateDrag(motionEvent.Relative);
    }
  }

  private void HandleMouseButton(InputEventMouseButton mouseEvent)
  {
    if (mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
    {
      StartDrag(mouseEvent.Position);
    }
    else if (!mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
    {
      DragManager.EndDrag(GetGlobalMousePosition());
    }
  }

  private void StartDrag(Vector2 mousePosition)
  {
    var spaceState = GetWorld2D().DirectSpaceState;

    var queryParameters = new PhysicsPointQueryParameters2D
    {
      CollideWithAreas = true,
      CollideWithBodies = false,
      CollisionMask = 1,
      Position = mousePosition
    };

    var results = spaceState.IntersectPoint(queryParameters);

    Card topCard = null;
    TableauZone fallbackZone = null;
    int topZIndex = int.MinValue;

    foreach (var result in results)
    {
      if (result["collider"].As<Area2D>() is Area2D area)
      {
        // Check if it's a card
        var card = area.GetParent() as Card;
        if (card != null && card.IsDraggable && card.ZIndex > topZIndex)
        {
          topCard = card;
          topZIndex = card.ZIndex;
        }
        // Check for a tableau zone as a fallback
        else if (card == null && area.GetParent() is TableauZone zone)
        {
          fallbackZone = zone;
        }
      }
    }

    // If a top card is found, start drag with it
    if (topCard != null)
    {
      DragManager.StartDrag(topCard, topCard.GetParent(), topCard.Position - mousePosition);
    }
    // Otherwise, handle fallback for tableau zones
    else if (fallbackZone != null)
    {
      DragManager.StartDrag(null, fallbackZone, Vector2.Zero);
    }
  }
}
