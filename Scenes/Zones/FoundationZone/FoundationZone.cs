using Godot;

using System.Collections.Generic;

public partial class FoundationZone : Area2D, IZone
{
  [Export] Texture2D foundationIconTexture = null;

  private ZoneManager _zoneManager = new ZoneManager();

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

  public Card GetTopCard() => _zoneManager.GetTopCard();
  public void AddCard(Card card) => _zoneManager.AddCard(card);
  public void RemoveCard(Card card) => _zoneManager.RemoveCard(card);

  public bool CanAcceptCard(Card card)
  {
    throw new System.NotImplementedException();
  }

  public List<Card> GetPileFromCard(Card startingCard)
  {
    throw new System.NotImplementedException();
  }
}
