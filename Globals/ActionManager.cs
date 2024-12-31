using Godot;

using System;

public partial class ActionManager : Node
{

  public static ActionManager Instance;

  [Signal] public delegate void OnCardDrawnEventHandler(Card card);
  [Signal] public delegate void OnTablueaPlayedEventHandler(Card card, TableauZone zone);
  [Signal] public delegate void OnFoundationPlayedEventHandler(Card card, FoundationZone zone);
  [Signal] public delegate void OnStockPileResetEventHandler();
  [Signal] public delegate void OnCardPositionResetEventHandler(Card card);


  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    Instance = this;
  }

  public static void EmitCardDrawn(Card card)
  {
    Instance.EmitSignal(SignalName.OnCardDrawn, card);
  }

  public static void EmitTableauPlayed(Card card, TableauZone zone)
  {
    Instance.EmitSignal(SignalName.OnTablueaPlayed, card, zone);
  }

  public static void EmitFoundationPlayed(Card card, FoundationZone zone)
  {
    Instance.EmitSignal(SignalName.OnFoundationPlayed, card, zone);
  }

  public static void EmitStockPileReset()
  {
    Instance.EmitSignal(SignalName.OnStockPileReset);
  }

  public static void EmitCardPositionReset(Card card)
  {
    Instance.EmitSignal(SignalName.OnCardPositionReset, card);
  }
}
