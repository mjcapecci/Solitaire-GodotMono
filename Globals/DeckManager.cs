using Godot;

using Solitaire.Classes;
using Solitaire.Classes.Enums;

using System;
using System.Collections.Generic;
using System.Linq;

public partial class DeckManager : Node
{
  public static DeckManager Instance;

  private List<CardDetails> _cardsInPlay = new List<CardDetails>();
  private List<Card> _allCards = new List<Card>();

  public override void _Ready()
  {
    Instance = this;
  }

  public static List<CardDetails> ShuffleDeck()
  {
    var random = new Random();
    var cards = AssetManager.cards.Keys.ToList();
    cards = cards.OrderBy(_ => random.Next()).ToList();

    Instance._cardsInPlay.Clear();
    var shuffledCards = new List<CardDetails>();

    foreach (var cardKey in cards)
    {
      var (rank, suit) = AssetManager.cards[cardKey];
      var cardDetails = new CardDetails
      {
        Suit = suit,
        Rank = rank,
        SuitDetails = new SuitDetails(suit),
        Texture = AssetManager.cardTextures[cardKey],
      };

      shuffledCards.Add(cardDetails);
    }

    return shuffledCards;
  }

  public static Card CreateCardInstance(CardDetails details)
  {
    var cardScene = GD.Load<PackedScene>("res://Scenes/Card/Card.tscn");
    var cardInstance = (Card) cardScene.Instantiate();
    cardInstance.Initialize(details);

    Instance._allCards.Add(cardInstance);
    return cardInstance;
  }

  public static void FlipStockpile(Area2D stockpile)
  {
    var children = stockpile.GetChildren().OfType<Card>().ToList();
    children.Reverse();

    foreach (var child in children)
    {
      stockpile.RemoveChild(child);
      stockpile.AddChild(child);
    }
  }
}
