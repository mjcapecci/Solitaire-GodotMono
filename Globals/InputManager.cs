using Godot;

using System;

public partial class InputManager : Node2D
{
  private static InputManager Instance;

  private RayCast2D _raycast;
  private Card _draggedCard = null;
  private Vector2 _dragOffset;

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    _raycast = new RayCast2D();
    _raycast.Enabled = false;
    _raycast.CollideWithAreas = true;
    _raycast.CollisionMask = 1; // Only pick cards
    _raycast.HitFromInside = true;
    AddChild(_raycast);
  }

  public override void _UnhandledInput(InputEvent @event)
  {
    if (@event is InputEventMouseButton mouseEvent)
    {
      HandleMouseButtonEvent(mouseEvent);
    }
    else if (@event is InputEventMouseMotion motionEvent && _draggedCard != null)
    {
      _draggedCard.Position = motionEvent.Position + _dragOffset;
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
      _draggedCard = null;
    }
    else if (mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Right)
    {
      HandleRightMousePressed();
    }
  }

  private void HandleLeftMousePressed(InputEventMouseButton mouseEvent)
  {
    _raycast.Enabled = true;
    _raycast.GlobalPosition = GetGlobalMousePosition();
    _raycast.TargetPosition = Vector2.Zero;
    _raycast.ForceRaycastUpdate();

    var collider = _raycast.GetCollider();

    if (collider is Area2D area && area.GetParent() is Card card)
    {
      _draggedCard = card;
      _dragOffset = card.Position - mouseEvent.Position;
    }
  }

  private void HandleRightMousePressed()
  {
    _raycast.Enabled = true;
    _raycast.GlobalPosition = GetGlobalMousePosition();
    _raycast.ForceRaycastUpdate();

    var collider = _raycast.GetCollider();
    if (collider is Area2D area && area.GetParent() is Card card)
    {
      card.FlipCard();
    }
  }
}
