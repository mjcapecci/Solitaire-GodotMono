using Godot;

using Solitaire.Classes;
using Solitaire.Classes.Enums;

using System;
using System.Collections.Generic;
using System.Linq;

public partial class DeckManager : Node
{
  public static DeckManager Instance;

  private Dictionary<CardDetails, CardPile> _cardsInPlay = new Dictionary<CardDetails, CardPile>();
  private List<Card> _allCards = new List<Card>();

  public override void _Ready()
  {
    Instance = this;
  }

  public static Dictionary<CardDetails, CardPile> ShuffleDeck()
  {
    var random = new Random();
    var cards = AssetManager.cards.Keys.ToList();
    cards = cards.OrderBy(_ => random.Next()).ToList();

    Instance._cardsInPlay.Clear();

    foreach (var cardKey in cards)
    {
      var (rank, suit) = AssetManager.cards[cardKey];
      var cardDetails = new CardDetails
      {
        Suit = suit,
        Rank = rank,
        SuitDetails = new SuitDetails(suit),
        Texture = AssetManager.cardTextures[cardKey],
        CurrentPile = CardPile.StockPile
      };
      Instance._cardsInPlay[cardDetails] = CardPile.StockPile;
    }

    return Instance._cardsInPlay;
  }

  public static Card CreateCardInstance(CardDetails details)
  {
    var cardScene = GD.Load<PackedScene>("res://Scenes/Card/Card.tscn");
    var cardInstance = (Card) cardScene.Instantiate();
    cardInstance.Initialize(details);

    Instance._allCards.Add(cardInstance);
    return cardInstance;
  }

  public static List<Card> GetCardsInPile(CardPile pile)
  {
    return Instance._allCards
        .Where(card => Instance._cardsInPlay[card.Details] == pile)
        .ToList();
  }

  public static void UpdateCardPile(Card card, CardPile newPile)
  {
    if (Instance._cardsInPlay.ContainsKey(card.Details))
    {
      Instance._cardsInPlay[card.Details] = newPile;
    }
  }

  public static bool HasCardsInPile(CardPile pile)
  {
    return Instance._cardsInPlay.Values.Contains(pile);
  }

  public static void ResetWasteToStock()
  {
    foreach (var cardDetails in Instance._cardsInPlay.Keys.ToList())
    {
      if (Instance._cardsInPlay[cardDetails] == CardPile.WastePile)
      {
        Instance._cardsInPlay[cardDetails] = CardPile.StockPile;
      }
    }
  }
}
