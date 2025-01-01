using Godot;

using System;
using System.Collections.Generic;

public interface IZone
{
  Vector2 Position { get; }
  Vector2 GlobalPosition { get; }
  bool CanAcceptCard(Card card); // Validate card before adding
  Card GetTopCard(); // Retrieve the last card
  Vector2 GetNextCardPosition(); // Calculate the next card position
}
