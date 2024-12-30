using Godot;

using System;
using System.Linq;

public partial class Deck : Node2D
{

  PackedScene cardScene = (PackedScene) ResourceLoader.Load("res://Scenes/Card/Card.tscn");

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    foreach (var cardDetails in DeckManager.DealHand())
    {
      var card = (Card) cardScene.Instantiate();
      card.Initialize(cardDetails);
      AddChild(card);
      var initialPosition = new Vector2(GetViewportRect().Size.X - 100, GetViewportRect().Size.Y - 150);
      card.Position = initialPosition;
      var finalPosition = new Vector2(50 + 100 * GetChildCount(), GetViewportRect().Size.Y - 150);
      var tween = card.CreateTween();
      tween.TweenProperty(card, "position", finalPosition, 0.5).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.Out).SetDelay(0.2 * GetChildCount());
      tween.TweenCallback(Callable.From(() => card.FlipCard()));
    }
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(double delta)
  {
  }
}
