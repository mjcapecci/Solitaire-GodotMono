using Godot;

using System.Collections.Generic;

public partial class FoundationZone : Area2D
{
  [Export] Texture2D foundationIconTexture = null;

  public override void _Ready()
  {
    GetNode<Sprite2D>("FoundationSuitIcon").Texture = foundationIconTexture;
    // BodyEntered += OnBodyEntered;
    // BodyExited += OnBodyExited;
  }

  // public void OnBodyEntered(Node body)
  // {
  //   throw new System.NotImplementedException();
  // }

  // public void OnBodyExited(Node body)
  // {
  //   throw new System.NotImplementedException();
  // }

  public bool CanAcceptCard(Card card)
  {
    throw new System.NotImplementedException();
  }

  public List<Card> GetPileFromCard(Card startingCard)
  {
    throw new System.NotImplementedException();
  }
}
