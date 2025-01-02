using Godot;
using System;

using Solitaire.Classes;
using Solitaire.Classes.Enums;

public partial class CardDetails
{
  public Suit Suit { get; set; }
  public Rank Rank { get; set; }
  public SuitDetails SuitDetails { get; set; }
  public Texture2D Texture { get; set; }
}
