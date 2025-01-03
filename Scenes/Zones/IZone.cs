using Godot;

public interface IZone
{
  Vector2 Position { get; }
  Vector2 GlobalPosition { get; }
  bool CanAcceptCard(Card card);
  Card GetTopCard();
  Vector2 GetNextCardPosition();
}
