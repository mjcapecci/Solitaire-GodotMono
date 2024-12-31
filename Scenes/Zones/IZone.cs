using Godot;

using System;
using System.Collections.Generic;

public interface IZone
{
  void AddCard(Card card);
  void RemoveCard(Card card);
  Vector2 Position { get; }
  Vector2 GlobalPosition { get; }
  bool CanAcceptCard(Card card); // Validate card before adding
  Card GetTopCard(); // Retrieve the last card
  List<Card> GetPileFromCard(Card startingCard); // Optional for Tableau
}
