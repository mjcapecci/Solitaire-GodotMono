using Godot;

using System;
using System.Linq;

public partial class GameManager : Node
{
  public static GameManager Instance;

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    Instance = this;
    ActionManager.Instance.OnTablueaPlayed += CheckForAutoWin;
  }

  private void CheckForAutoWin(Card card, TableauZone zone)
  {

    if (CheckAutoWinState())
    {
      AutoWin();
    };
  }

  public bool CheckAutoWinState()
  {
    // Check if all cards are face-up
    if (!AreAllCardsRevealed())
      return false;

    // Check if all Tableau piles are in a valid order
    if (!AreTableauPilesValid())
      return false;

    // Check if Waste pile cards can be moved to Foundation
    if (!AreWastePileCardsPlayable())
      return false;

    return true;
  }

  private bool AreAllCardsRevealed()
  {
    foreach (var tableauZone in GetTree().GetNodesInGroup("TableauZones"))
    {
      if (tableauZone is TableauZone zone)
      {
        foreach (var card in zone.GetChildren().OfType<Card>())
        {
          if (!card.IsFaceUp)
            return false;
        }
      }
    }
    return true;
  }

  private bool AreTableauPilesValid()
  {
    foreach (var tableauZone in GetTree().GetNodesInGroup("TableauZones"))
    {
      if (tableauZone is TableauZone zone)
      {
        var cards = zone.GetChildren().OfType<Card>().ToList();
        for (int i = 1; i < cards.Count; i++)
        {
          if (!CanPlaceOn(cards[i], cards[i - 1]))
            return false;
        }
      }
    }
    return true;
  }

  private bool AreWastePileCardsPlayable()
  {
    var wastePileCards = GetNode("/root/Table/Deck/WastePile").GetChildren().OfType<Card>().ToList();
    foreach (var card in wastePileCards)
    {
      if (!CanMoveToFoundation(card))
        return false;
    }
    return true;
  }

  private bool CanPlaceOn(Card card, Card target)
  {
    return card.Rank == target.Rank - 1 &&
           card.SuitDetails.Color != target.SuitDetails.Color;
  }

  private bool CanMoveToFoundation(Card card)
  {
    foreach (var foundationZone in GetTree().GetNodesInGroup("FoundationZones"))
    {
      if (foundationZone is FoundationZone zone && zone.CanAcceptCard(card))
        return true;
    }
    return false;
  }

  public void AutoWin()
  {
    while (true)
    {
      bool cardMoved = false;

      foreach (var tableauZone in GetTree().GetNodesInGroup("TableauZones"))
      {
        if (tableauZone is TableauZone zone)
        {
          var topCard = zone.GetTopCard();
          if (topCard != null && CanMoveToFoundation(topCard))
          {
            ActionManager.EmitFoundationPlayed(topCard, FindFoundationZone(topCard));
            cardMoved = true;
          }
        }
      }

      var wastePileCards = GetNode("/root/Table/Deck/WastePile").GetChildren().OfType<Card>().ToList();
      foreach (var card in wastePileCards)
      {
        if (CanMoveToFoundation(card))
        {
          ActionManager.EmitFoundationPlayed(card, FindFoundationZone(card));
          cardMoved = true;
        }
      }

      if (!cardMoved)
        break;
    }
  }

  private FoundationZone FindFoundationZone(Card card)
  {
    foreach (var foundationZone in GetTree().GetNodesInGroup("FoundationZones"))
    {
      if (foundationZone is FoundationZone zone && zone.CanAcceptCard(card))
        return zone;
    }
    return null;
  }
}
