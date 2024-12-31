using Godot;

using Solitaire.Classes;

using System;
using System.Collections.Generic;

public partial class Card : Node2D
{

  public static int GlobalTopZIndex = 1; // shared among all Cards
  private Sprite2D _cardSprite;
  private Area2D _cardArea;

  public CardDetails Details;
  public Suit Suit;
  public SuitDetails SuitDetails;
  public Solitaire.Classes.Enums.Rank Rank;

  public bool IsFaceUp { get; set; }
  public bool IsDraggable { get; set; }
  public Vector2 InitialPosition { get; set; }

  public void Initialize(CardDetails details)
  {
    Details = details;
    Suit = details.Suit;
    SuitDetails = new SuitDetails(Suit);
    Rank = details.Rank;
  }

  public override void _Ready()
  {
    _cardSprite = GetNode<Sprite2D>("CardSprite");
    _cardArea = GetNode<Area2D>("CardArea");
    _cardSprite.Texture = AssetManager.supportTextures["card_back"];
  }

  public void FlipCard(bool faceUp)
  {
    IsFaceUp = faceUp;
    _cardSprite.Texture = IsFaceUp
        ? AssetManager.cardTextures[$"{Rank}{Suit}"]
        : AssetManager.supportTextures["card_back"];
  }

  public List<IZone> FindOverlappingDropZones()
  {
    var areas = _cardArea.GetOverlappingAreas();
    List<IZone> zones = new List<IZone>();

    foreach (var area in areas)
    {
      if (area is IZone dropZone)
      {
        zones.Add(dropZone);
      }
    }
    return zones;
  }

  public void SnapToInitialPosition()
  {
    var tween = CreateTween();
    tween.Stop();
    tween.TweenProperty(this, "position", InitialPosition, 0.1f).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.InOut);
    tween.Play();
    tween.Finished += () =>
    {
      ZIndex = 1;
      ActionManager.EmitCardPositionReset(this);
    };
  }

  public void SnapToZone(IZone zone)
  {
    var lastCard = zone.GetTopCard();

    if (lastCard != null)
    {
      // Snap the card to the last card's position with an offset
      GlobalPosition = lastCard.GlobalPosition + new Vector2(0, 20); // Adjust vertical offset
      ZIndex = 1;
      ActionManager.EmitCardPositionReset(this);
    }
    else
    {
      // Snap to the zone's position if empty
      GlobalPosition = zone.GlobalPosition;
      ZIndex = 1;
      ActionManager.EmitCardPositionReset(this);
    }
  }

  public void SetCollisionEnabled(bool enabled)
  {
    _cardArea.Monitoring = enabled;
    _cardArea.Monitorable = enabled;
  }

  public void BringToFront()
  {
    // GD.Print(GlobalTopZIndex);
    // GlobalTopZIndex++;
    ZIndex = 4096;
  }
}
