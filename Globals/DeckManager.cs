using Godot;

using Solitaire.Classes;
using Solitaire.Classes.Enums;

using System;
using System.Collections.Generic;
using System.Linq;

public partial class DeckManager : Node
{

  public static DeckManager Instance;

  private Dictionary<string, Texture2D> _cardTextures = new Dictionary<string, Texture2D>();
  private Dictionary<string, Texture2D> _supportTextures = new Dictionary<string, Texture2D>();

  private Dictionary<string, (Rank rank, Suit suit)> _cards = new Dictionary<string, (Rank rank, Suit suit)>
  {
    {$"{Rank.Two}{Suit.Clubs}", (Rank.Two, Suit.Clubs)},
    {$"{Rank.Three}{Suit.Clubs}", (Rank.Three, Suit.Clubs)},
    {$"{Rank.Four}{Suit.Clubs}", (Rank.Four, Suit.Clubs)},
    {$"{Rank.Five}{Suit.Clubs}", (Rank.Five, Suit.Clubs)},
    {$"{Rank.Six}{Suit.Clubs}", (Rank.Six, Suit.Clubs)},
    {$"{Rank.Seven}{Suit.Clubs}", (Rank.Seven, Suit.Clubs)},
    {$"{Rank.Eight}{Suit.Clubs}", (Rank.Eight, Suit.Clubs)},
    {$"{Rank.Nine}{Suit.Clubs}", (Rank.Nine, Suit.Clubs)},
    {$"{Rank.Ten}{Suit.Clubs}", (Rank.Ten, Suit.Clubs)},
    {$"{Rank.Jack}{Suit.Clubs}", (Rank.Jack, Suit.Clubs)},
    {$"{Rank.Queen}{Suit.Clubs}", (Rank.Queen, Suit.Clubs)},
    {$"{Rank.King}{Suit.Clubs}", (Rank.King, Suit.Clubs)},
    {$"{Rank.Ace}{Suit.Clubs}", (Rank.Ace, Suit.Clubs)},
    {$"{Rank.Two}{Suit.Diamonds}", (Rank.Two, Suit.Diamonds)},
    {$"{Rank.Three}{Suit.Diamonds}", (Rank.Three, Suit.Diamonds)},
    {$"{Rank.Four}{Suit.Diamonds}", (Rank.Four, Suit.Diamonds)},
    {$"{Rank.Five}{Suit.Diamonds}", (Rank.Five, Suit.Diamonds)},
    {$"{Rank.Six}{Suit.Diamonds}", (Rank.Six, Suit.Diamonds)},
    {$"{Rank.Seven}{Suit.Diamonds}", (Rank.Seven, Suit.Diamonds)},
    {$"{Rank.Eight}{Suit.Diamonds}", (Rank.Eight, Suit.Diamonds)},
    {$"{Rank.Nine}{Suit.Diamonds}", (Rank.Nine, Suit.Diamonds)},
    {$"{Rank.Ten}{Suit.Diamonds}", (Rank.Ten, Suit.Diamonds)},
    {$"{Rank.Jack}{Suit.Diamonds}", (Rank.Jack, Suit.Diamonds)},
    {$"{Rank.Queen}{Suit.Diamonds}", (Rank.Queen, Suit.Diamonds)},
    {$"{Rank.King}{Suit.Diamonds}", (Rank.King, Suit.Diamonds)},
    {$"{Rank.Ace}{Suit.Diamonds}", (Rank.Ace, Suit.Diamonds)},
    {$"{Rank.Two}{Suit.Hearts}", (Rank.Two, Suit.Hearts)},
    {$"{Rank.Three}{Suit.Hearts}", (Rank.Three, Suit.Hearts)},
    {$"{Rank.Four}{Suit.Hearts}", (Rank.Four, Suit.Hearts)},
    {$"{Rank.Five}{Suit.Hearts}", (Rank.Five, Suit.Hearts)},
    {$"{Rank.Six}{Suit.Hearts}", (Rank.Six, Suit.Hearts)},
    {$"{Rank.Seven}{Suit.Hearts}", (Rank.Seven, Suit.Hearts)},
    {$"{Rank.Eight}{Suit.Hearts}", (Rank.Eight, Suit.Hearts)},
    {$"{Rank.Nine}{Suit.Hearts}", (Rank.Nine, Suit.Hearts)},
    {$"{Rank.Ten}{Suit.Hearts}", (Rank.Ten, Suit.Hearts)},
    {$"{Rank.Jack}{Suit.Hearts}", (Rank.Jack, Suit.Hearts)},
    {$"{Rank.Queen}{Suit.Hearts}", (Rank.Queen, Suit.Hearts)},
    {$"{Rank.King}{Suit.Hearts}", (Rank.King, Suit.Hearts)},
    {$"{Rank.Ace}{Suit.Hearts}", (Rank.Ace, Suit.Hearts)},
    {$"{Rank.Two}{Suit.Spades}", (Rank.Two, Suit.Spades)},
    {$"{Rank.Three}{Suit.Spades}", (Rank.Three, Suit.Spades)},
    {$"{Rank.Four}{Suit.Spades}", (Rank.Four, Suit.Spades)},
    {$"{Rank.Five}{Suit.Spades}", (Rank.Five, Suit.Spades)},
    {$"{Rank.Six}{Suit.Spades}", (Rank.Six, Suit.Spades)},
    {$"{Rank.Seven}{Suit.Spades}", (Rank.Seven, Suit.Spades)},
    {$"{Rank.Eight}{Suit.Spades}", (Rank.Eight, Suit.Spades)},
    {$"{Rank.Nine}{Suit.Spades}", (Rank.Nine, Suit.Spades)},
    {$"{Rank.Ten}{Suit.Spades}", (Rank.Ten, Suit.Spades)},
    {$"{Rank.Jack}{Suit.Spades}", (Rank.Jack, Suit.Spades)},
    {$"{Rank.Queen}{Suit.Spades}", (Rank.Queen, Suit.Spades)},
    {$"{Rank.King}{Suit.Spades}", (Rank.King, Suit.Spades)},
    {$"{Rank.Ace}{Suit.Spades}", (Rank.Ace, Suit.Spades)},

  };

  private Dictionary<CardDetails, CardPile> _cardsInPlay = new Dictionary<CardDetails, CardPile>();
  private List<Card> _allCards = new List<Card>();

  public Vector2 stockPilePosition = new Vector2(187, 140);
  public Vector2 wastePilePosition = new Vector2(187, 300);

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    Instance = this;
    LoadCardTextures();
  }

  private void LoadCardTextures()
  {
    _cardTextures.Add($"{Rank.Two}{Suit.Clubs}", (Texture2D) GD.Load("res://assets/cards/card_clubs_02.png"));
    _cardTextures.Add($"{Rank.Three}{Suit.Clubs}", (Texture2D) GD.Load("res://assets/cards/card_clubs_03.png"));
    _cardTextures.Add($"{Rank.Four}{Suit.Clubs}", (Texture2D) GD.Load("res://assets/cards/card_clubs_04.png"));
    _cardTextures.Add($"{Rank.Five}{Suit.Clubs}", (Texture2D) GD.Load("res://assets/cards/card_clubs_05.png"));
    _cardTextures.Add($"{Rank.Six}{Suit.Clubs}", (Texture2D) GD.Load("res://assets/cards/card_clubs_06.png"));
    _cardTextures.Add($"{Rank.Seven}{Suit.Clubs}", (Texture2D) GD.Load("res://assets/cards/card_clubs_07.png"));
    _cardTextures.Add($"{Rank.Eight}{Suit.Clubs}", (Texture2D) GD.Load("res://assets/cards/card_clubs_08.png"));
    _cardTextures.Add($"{Rank.Nine}{Suit.Clubs}", (Texture2D) GD.Load("res://assets/cards/card_clubs_09.png"));
    _cardTextures.Add($"{Rank.Ten}{Suit.Clubs}", (Texture2D) GD.Load("res://assets/cards/card_clubs_10.png"));
    _cardTextures.Add($"{Rank.Jack}{Suit.Clubs}", (Texture2D) GD.Load("res://assets/cards/card_clubs_J.png"));
    _cardTextures.Add($"{Rank.Queen}{Suit.Clubs}", (Texture2D) GD.Load("res://assets/cards/card_clubs_Q.png"));
    _cardTextures.Add($"{Rank.King}{Suit.Clubs}", (Texture2D) GD.Load("res://assets/cards/card_clubs_K.png"));
    _cardTextures.Add($"{Rank.Ace}{Suit.Clubs}", (Texture2D) GD.Load("res://assets/cards/card_clubs_A.png"));
    _cardTextures.Add($"{Rank.Two}{Suit.Diamonds}", (Texture2D) GD.Load("res://assets/cards/card_diamonds_02.png"));
    _cardTextures.Add($"{Rank.Three}{Suit.Diamonds}", (Texture2D) GD.Load("res://assets/cards/card_diamonds_03.png"));
    _cardTextures.Add($"{Rank.Four}{Suit.Diamonds}", (Texture2D) GD.Load("res://assets/cards/card_diamonds_04.png"));
    _cardTextures.Add($"{Rank.Five}{Suit.Diamonds}", (Texture2D) GD.Load("res://assets/cards/card_diamonds_05.png"));
    _cardTextures.Add($"{Rank.Six}{Suit.Diamonds}", (Texture2D) GD.Load("res://assets/cards/card_diamonds_06.png"));
    _cardTextures.Add($"{Rank.Seven}{Suit.Diamonds}", (Texture2D) GD.Load("res://assets/cards/card_diamonds_07.png"));
    _cardTextures.Add($"{Rank.Eight}{Suit.Diamonds}", (Texture2D) GD.Load("res://assets/cards/card_diamonds_08.png"));
    _cardTextures.Add($"{Rank.Nine}{Suit.Diamonds}", (Texture2D) GD.Load("res://assets/cards/card_diamonds_09.png"));
    _cardTextures.Add($"{Rank.Ten}{Suit.Diamonds}", (Texture2D) GD.Load("res://assets/cards/card_diamonds_10.png"));
    _cardTextures.Add($"{Rank.Jack}{Suit.Diamonds}", (Texture2D) GD.Load("res://assets/cards/card_diamonds_J.png"));
    _cardTextures.Add($"{Rank.Queen}{Suit.Diamonds}", (Texture2D) GD.Load("res://assets/cards/card_diamonds_Q.png"));
    _cardTextures.Add($"{Rank.King}{Suit.Diamonds}", (Texture2D) GD.Load("res://assets/cards/card_diamonds_K.png"));
    _cardTextures.Add($"{Rank.Ace}{Suit.Diamonds}", (Texture2D) GD.Load("res://assets/cards/card_diamonds_A.png"));
    _cardTextures.Add($"{Rank.Two}{Suit.Hearts}", (Texture2D) GD.Load("res://assets/cards/card_hearts_02.png"));
    _cardTextures.Add($"{Rank.Three}{Suit.Hearts}", (Texture2D) GD.Load("res://assets/cards/card_hearts_03.png"));
    _cardTextures.Add($"{Rank.Four}{Suit.Hearts}", (Texture2D) GD.Load("res://assets/cards/card_hearts_04.png"));
    _cardTextures.Add($"{Rank.Five}{Suit.Hearts}", (Texture2D) GD.Load("res://assets/cards/card_hearts_05.png"));
    _cardTextures.Add($"{Rank.Six}{Suit.Hearts}", (Texture2D) GD.Load("res://assets/cards/card_hearts_06.png"));
    _cardTextures.Add($"{Rank.Seven}{Suit.Hearts}", (Texture2D) GD.Load("res://assets/cards/card_hearts_07.png"));
    _cardTextures.Add($"{Rank.Eight}{Suit.Hearts}", (Texture2D) GD.Load("res://assets/cards/card_hearts_08.png"));
    _cardTextures.Add($"{Rank.Nine}{Suit.Hearts}", (Texture2D) GD.Load("res://assets/cards/card_hearts_09.png"));
    _cardTextures.Add($"{Rank.Ten}{Suit.Hearts}", (Texture2D) GD.Load("res://assets/cards/card_hearts_10.png"));
    _cardTextures.Add($"{Rank.Jack}{Suit.Hearts}", (Texture2D) GD.Load("res://assets/cards/card_hearts_J.png"));
    _cardTextures.Add($"{Rank.Queen}{Suit.Hearts}", (Texture2D) GD.Load("res://assets/cards/card_hearts_Q.png"));
    _cardTextures.Add($"{Rank.King}{Suit.Hearts}", (Texture2D) GD.Load("res://assets/cards/card_hearts_K.png"));
    _cardTextures.Add($"{Rank.Ace}{Suit.Hearts}", (Texture2D) GD.Load("res://assets/cards/card_hearts_A.png"));
    _cardTextures.Add($"{Rank.Two}{Suit.Spades}", (Texture2D) GD.Load("res://assets/cards/card_spades_02.png"));
    _cardTextures.Add($"{Rank.Three}{Suit.Spades}", (Texture2D) GD.Load("res://assets/cards/card_spades_03.png"));
    _cardTextures.Add($"{Rank.Four}{Suit.Spades}", (Texture2D) GD.Load("res://assets/cards/card_spades_04.png"));
    _cardTextures.Add($"{Rank.Five}{Suit.Spades}", (Texture2D) GD.Load("res://assets/cards/card_spades_05.png"));
    _cardTextures.Add($"{Rank.Six}{Suit.Spades}", (Texture2D) GD.Load("res://assets/cards/card_spades_06.png"));
    _cardTextures.Add($"{Rank.Seven}{Suit.Spades}", (Texture2D) GD.Load("res://assets/cards/card_spades_07.png"));
    _cardTextures.Add($"{Rank.Eight}{Suit.Spades}", (Texture2D) GD.Load("res://assets/cards/card_spades_08.png"));
    _cardTextures.Add($"{Rank.Nine}{Suit.Spades}", (Texture2D) GD.Load("res://assets/cards/card_spades_09.png"));
    _cardTextures.Add($"{Rank.Ten}{Suit.Spades}", (Texture2D) GD.Load("res://assets/cards/card_spades_10.png"));
    _cardTextures.Add($"{Rank.Jack}{Suit.Spades}", (Texture2D) GD.Load("res://assets/cards/card_spades_J.png"));
    _cardTextures.Add($"{Rank.Queen}{Suit.Spades}", (Texture2D) GD.Load("res://assets/cards/card_spades_Q.png"));
    _cardTextures.Add($"{Rank.King}{Suit.Spades}", (Texture2D) GD.Load("res://assets/cards/card_spades_K.png"));
    _cardTextures.Add($"{Rank.Ace}{Suit.Spades}", (Texture2D) GD.Load("res://assets/cards/card_spades_A.png"));
  }

  private void LoadSupportTextures()
  {
    _supportTextures.Add("card_empty", (Texture2D) GD.Load("res://assets/cards/card_empty.png"));
    _supportTextures.Add("card_back", (Texture2D) GD.Load("res://assets/cards/card_back.png"));
  }


  public static Texture2D GetCardTexture(string cardName)
  {
    if (Instance._cardTextures.Count == 0)
    {
      Instance.LoadCardTextures();
    }

    return Instance._cardTextures[cardName];
  }

  public static Texture2D GetSupportTexture(string supportName)
  {
    if (Instance._supportTextures.Count == 0)
    {
      Instance.LoadSupportTextures();
    }

    return Instance._supportTextures[supportName];
  }

  public static Dictionary<CardDetails, CardPile> ShuffleDeck()
  {
    var random = new Random();
    var cards = Instance._cards.Keys.ToList();
    cards = cards.OrderBy(_ => random.Next()).ToList();

    Instance._cardsInPlay.Clear();

    foreach (var cardKey in cards)
    {
      var (rank, suit) = Instance._cards[cardKey];
      var cardDetails = new CardDetails
      {
        Suit = suit,
        Rank = rank,
        SuitDetails = new SuitDetails(suit),
        Texture = Instance._cardTextures[cardKey],
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
    cardInstance.GlobalPosition = new Vector2(0, 0); // Adjust the position as needed

    Instance._allCards.Add(cardInstance);
    return cardInstance;
  }

  public static bool CanDealToTableauZones()
  {
    return Instance._cardsInPlay.Values.Count(pile => pile == CardPile.Tableau) == 0;
  }

  public static bool CanDrawFromStock()
  {
    GD.Print(Instance._cardsInPlay.Values.Contains(CardPile.StockPile));
    return Instance._cardsInPlay.Values.Contains(CardPile.StockPile);
  }

  public static void DrawFromStock()
  {
    var stockCardDetails = Instance._cardsInPlay.FirstOrDefault(kv => kv.Value == CardPile.StockPile).Key;

    if (stockCardDetails != null)
    {
      Instance._cardsInPlay[stockCardDetails] = CardPile.WastePile;

      var card = Instance._allCards.First(c => c.Details == stockCardDetails);
      card.FlipCard(true);

      var wastePileCount = Instance._cardsInPlay.Values.Count(pile => pile == CardPile.WastePile);
      card.GlobalPosition = new Vector2(187, 240 + wastePileCount * 35); // Adjust the position as needed
      card.IsDraggable = true;
    }
  }

  public static void ResetWasteToStock()
  {
    foreach (var cardDetails in Instance._cardsInPlay.Keys.ToList())
    {
      if (Instance._cardsInPlay[cardDetails] == CardPile.WastePile)
      {
        Instance._cardsInPlay[cardDetails] = CardPile.StockPile;
      }

      if (Instance._cardsInPlay[cardDetails] == CardPile.StockPile)
      {
        var card = Instance._allCards.First(c => c.Details == cardDetails);
        card.FlipCard(false);
        card.GlobalPosition = new Vector2(187, 140); // Adjust the position as needed
      }
    }
  }

  public static void DealToTableauZones(List<TableauZone> tableauZones)
  {
    int tableauIndex = 0;
    foreach (var tableauZone in tableauZones)
    {
      int cardsToDeal = tableauIndex + 1;

      // Deal cards one by one
      for (int i = cardsToDeal - 1; i >= 0; i--)
      {
        var stockCards = GetStockPileCards();
        if (stockCards.Count == 0)
          break;

        var card = stockCards.Last();
        UpdateCardPile(card, CardPile.Tableau);

        tableauZone.AddCard(card);
        card.GlobalPosition = tableauZone.GlobalPosition + new Vector2(0, i * 35); // Offset vertically for stacking
        card.ZIndex = cardsToDeal - i; // Set the z-index to ensure correct stacking order
        card.FlipCard(i == tableauIndex); // Flip all cards face up
        card.IsDraggable = i == tableauIndex; // Only the top card is draggable
      }

      tableauIndex++;
    }
  }

  public static List<Card> GetStockPileCards()
  {
    return Instance._allCards.Where(card => Instance._cardsInPlay[card.Details] == CardPile.StockPile).ToList();
  }

  public static List<Card> GetWastePileCards()
  {
    return Instance._allCards.Where(card => Instance._cardsInPlay[card.Details] == CardPile.WastePile).ToList();
  }

  public static void UpdateCardPile(Card card, CardPile newPile)
  {
    if (Instance._cardsInPlay.ContainsKey(card.Details))
    {
      Instance._cardsInPlay[card.Details] = newPile;
    }
  }
}
