using Godot;

using Solitaire.Classes.Enums;

using System;
using System.Collections.Generic;
using System.Linq;

public partial class DeckManager : Node
{

  private static DeckManager Instance;

  private Dictionary<string, Texture2D> _cardTextures = new Dictionary<string, Texture2D>();
  private Dictionary<string, Texture2D> _supportTextures = new Dictionary<string, Texture2D>();

  private Dictionary<string, (string rank, string suit)> _cards = new Dictionary<string, (string rank, string suit)>
  {
    {"card_clubs_02", ("02", "clubs")},
    {"card_clubs_03", ("03", "clubs")},
    {"card_clubs_04", ("04", "clubs")},
    {"card_clubs_05", ("05", "clubs")},
    {"card_clubs_06", ("06", "clubs")},
    {"card_clubs_07", ("07", "clubs")},
    {"card_clubs_08", ("08", "clubs")},
    {"card_clubs_09", ("09", "clubs")},
    {"card_clubs_10", ("10", "clubs")},
    {"card_clubs_J", ("J", "clubs")},
    {"card_clubs_Q", ("Q", "clubs")},
    {"card_clubs_K", ("K", "clubs")},
    {"card_clubs_A", ("A", "clubs")},
    {"card_spades_02", ("02", "spades")},
    {"card_spades_03", ("03", "spades")},
    {"card_spades_04", ("04", "spades")},
    {"card_spades_05", ("05", "spades")},
    {"card_spades_06", ("06", "spades")},
    {"card_spades_07", ("07", "spades")},
    {"card_spades_08", ("08", "spades")},
    {"card_spades_09", ("09", "spades")},
    {"card_spades_10", ("10", "spades")},
    {"card_spades_J", ("J", "spades")},
    {"card_spades_Q", ("Q", "spades")},
    {"card_spades_K", ("K", "spades")},
    {"card_spades_A", ("A", "spades")},
    {"card_hearts_02", ("02", "hearts")},
    {"card_hearts_03", ("03", "hearts")},
    {"card_hearts_04", ("04", "hearts")},
    {"card_hearts_05", ("05", "hearts")},
    {"card_hearts_06", ("06", "hearts")},
    {"card_hearts_07", ("07", "hearts")},
    {"card_hearts_08", ("08", "hearts")},
    {"card_hearts_09", ("09", "hearts")},
    {"card_hearts_10", ("10", "hearts")},
    {"card_hearts_J", ("J", "hearts")},
    {"card_hearts_Q", ("Q", "hearts")},
    {"card_hearts_K", ("K", "hearts")},
    {"card_hearts_A", ("A", "hearts")},
    {"card_diamonds_02", ("02", "diamonds")},
    {"card_diamonds_03", ("03", "diamonds")},
    {"card_diamonds_04", ("04", "diamonds")},
    {"card_diamonds_05", ("05", "diamonds")},
    {"card_diamonds_06", ("06", "diamonds")},
    {"card_diamonds_07", ("07", "diamonds")},
    {"card_diamonds_08", ("08", "diamonds")},
    {"card_diamonds_09", ("09", "diamonds")},
    {"card_diamonds_10", ("10", "diamonds")},
    {"card_diamonds_J", ("J", "diamonds")},
    {"card_diamonds_Q", ("Q", "diamonds")},
    {"card_diamonds_K", ("K", "diamonds")},
    {"card_diamonds_A", ("A", "diamonds")}
  };

  private Dictionary<CardDetails, CardPile> _cardsInPlay = new Dictionary<CardDetails, CardPile>();

  public void AddCardToPlay(CardDetails cardDetails, CardPile location)
  {
    if (!_cardsInPlay.ContainsKey(cardDetails))
    {
      _cardsInPlay.Add(cardDetails, location);
    }
  }

  public void UpdateCardPile(CardDetails cardDetails, CardPile newLocation)
  {
    if (_cardsInPlay.ContainsKey(cardDetails))
    {
      _cardsInPlay[cardDetails] = newLocation;
    }
  }

  public void RemoveCardFromPlay(CardDetails cardDetails)
  {
    if (_cardsInPlay.ContainsKey(cardDetails))
    {
      _cardsInPlay.Remove(cardDetails);
    }
  }

  public CardPile GetCardPile(CardDetails cardDetails)
  {
    if (_cardsInPlay.ContainsKey(cardDetails))
    {
      return _cardsInPlay[cardDetails];
    }
    return default(CardPile);
  }

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    Instance = this;
    LoadCardTextures();
  }

  private void LoadCardTextures()
  {
    _cardTextures.Add("card_clubs_02", (Texture2D) GD.Load("res://assets/cards/card_clubs_02.png"));
    _cardTextures.Add("card_clubs_03", (Texture2D) GD.Load("res://assets/cards/card_clubs_03.png"));
    _cardTextures.Add("card_clubs_04", (Texture2D) GD.Load("res://assets/cards/card_clubs_04.png"));
    _cardTextures.Add("card_clubs_05", (Texture2D) GD.Load("res://assets/cards/card_clubs_05.png"));
    _cardTextures.Add("card_clubs_06", (Texture2D) GD.Load("res://assets/cards/card_clubs_06.png"));
    _cardTextures.Add("card_clubs_07", (Texture2D) GD.Load("res://assets/cards/card_clubs_07.png"));
    _cardTextures.Add("card_clubs_08", (Texture2D) GD.Load("res://assets/cards/card_clubs_08.png"));
    _cardTextures.Add("card_clubs_09", (Texture2D) GD.Load("res://assets/cards/card_clubs_09.png"));
    _cardTextures.Add("card_clubs_10", (Texture2D) GD.Load("res://assets/cards/card_clubs_10.png"));
    _cardTextures.Add("card_clubs_J", (Texture2D) GD.Load("res://assets/cards/card_clubs_J.png"));
    _cardTextures.Add("card_clubs_Q", (Texture2D) GD.Load("res://assets/cards/card_clubs_Q.png"));
    _cardTextures.Add("card_clubs_K", (Texture2D) GD.Load("res://assets/cards/card_clubs_K.png"));
    _cardTextures.Add("card_clubs_A", (Texture2D) GD.Load("res://assets/cards/card_clubs_A.png"));
    _cardTextures.Add("card_spades_02", (Texture2D) GD.Load("res://assets/cards/card_spades_02.png"));
    _cardTextures.Add("card_spades_03", (Texture2D) GD.Load("res://assets/cards/card_spades_03.png"));
    _cardTextures.Add("card_spades_04", (Texture2D) GD.Load("res://assets/cards/card_spades_04.png"));
    _cardTextures.Add("card_spades_05", (Texture2D) GD.Load("res://assets/cards/card_spades_05.png"));
    _cardTextures.Add("card_spades_06", (Texture2D) GD.Load("res://assets/cards/card_spades_06.png"));
    _cardTextures.Add("card_spades_07", (Texture2D) GD.Load("res://assets/cards/card_spades_07.png"));
    _cardTextures.Add("card_spades_08", (Texture2D) GD.Load("res://assets/cards/card_spades_08.png"));
    _cardTextures.Add("card_spades_09", (Texture2D) GD.Load("res://assets/cards/card_spades_09.png"));
    _cardTextures.Add("card_spades_10", (Texture2D) GD.Load("res://assets/cards/card_spades_10.png"));
    _cardTextures.Add("card_spades_J", (Texture2D) GD.Load("res://assets/cards/card_spades_J.png"));
    _cardTextures.Add("card_spades_Q", (Texture2D) GD.Load("res://assets/cards/card_spades_Q.png"));
    _cardTextures.Add("card_spades_K", (Texture2D) GD.Load("res://assets/cards/card_spades_K.png"));
    _cardTextures.Add("card_spades_A", (Texture2D) GD.Load("res://assets/cards/card_spades_A.png"));
    _cardTextures.Add("card_hearts_02", (Texture2D) GD.Load("res://assets/cards/card_hearts_02.png"));
    _cardTextures.Add("card_hearts_03", (Texture2D) GD.Load("res://assets/cards/card_hearts_03.png"));
    _cardTextures.Add("card_hearts_04", (Texture2D) GD.Load("res://assets/cards/card_hearts_04.png"));
    _cardTextures.Add("card_hearts_05", (Texture2D) GD.Load("res://assets/cards/card_hearts_05.png"));
    _cardTextures.Add("card_hearts_06", (Texture2D) GD.Load("res://assets/cards/card_hearts_06.png"));
    _cardTextures.Add("card_hearts_07", (Texture2D) GD.Load("res://assets/cards/card_hearts_07.png"));
    _cardTextures.Add("card_hearts_08", (Texture2D) GD.Load("res://assets/cards/card_hearts_08.png"));
    _cardTextures.Add("card_hearts_09", (Texture2D) GD.Load("res://assets/cards/card_hearts_09.png"));
    _cardTextures.Add("card_hearts_10", (Texture2D) GD.Load("res://assets/cards/card_hearts_10.png"));
    _cardTextures.Add("card_hearts_J", (Texture2D) GD.Load("res://assets/cards/card_hearts_J.png"));
    _cardTextures.Add("card_hearts_Q", (Texture2D) GD.Load("res://assets/cards/card_hearts_Q.png"));
    _cardTextures.Add("card_hearts_K", (Texture2D) GD.Load("res://assets/cards/card_hearts_K.png"));
    _cardTextures.Add("card_hearts_A", (Texture2D) GD.Load("res://assets/cards/card_hearts_A.png"));
    _cardTextures.Add("card_diamonds_02", (Texture2D) GD.Load("res://assets/cards/card_diamonds_02.png"));
    _cardTextures.Add("card_diamonds_03", (Texture2D) GD.Load("res://assets/cards/card_diamonds_03.png"));
    _cardTextures.Add("card_diamonds_04", (Texture2D) GD.Load("res://assets/cards/card_diamonds_04.png"));
    _cardTextures.Add("card_diamonds_05", (Texture2D) GD.Load("res://assets/cards/card_diamonds_05.png"));
    _cardTextures.Add("card_diamonds_06", (Texture2D) GD.Load("res://assets/cards/card_diamonds_06.png"));
    _cardTextures.Add("card_diamonds_07", (Texture2D) GD.Load("res://assets/cards/card_diamonds_07.png"));
    _cardTextures.Add("card_diamonds_08", (Texture2D) GD.Load("res://assets/cards/card_diamonds_08.png"));
    _cardTextures.Add("card_diamonds_09", (Texture2D) GD.Load("res://assets/cards/card_diamonds_09.png"));
    _cardTextures.Add("card_diamonds_10", (Texture2D) GD.Load("res://assets/cards/card_diamonds_10.png"));
    _cardTextures.Add("card_diamonds_J", (Texture2D) GD.Load("res://assets/cards/card_diamonds_J.png"));
    _cardTextures.Add("card_diamonds_Q", (Texture2D) GD.Load("res://assets/cards/card_diamonds_Q.png"));
    _cardTextures.Add("card_diamonds_K", (Texture2D) GD.Load("res://assets/cards/card_diamonds_K.png"));
    _cardTextures.Add("card_diamonds_A", (Texture2D) GD.Load("res://assets/cards/card_diamonds_A.png"));
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

    for (int i = cards.Count - 1; i > 0; i--)
    {
      int j = random.Next(i + 1);
      (cards[i], cards[j]) = (cards[j], cards[i]);
    }

    Instance._cardsInPlay.Clear();

    foreach (var cardKey in cards)
    {
      var (rank, suit) = Instance._cards[cardKey];
      var cardDetails = new CardDetails(suit, rank, GetCardColor(suit), $"res://assets/cards/{cardKey}.png");
      Instance._cardsInPlay.Add(cardDetails, CardPile.StockPile);
    }

    return Instance._cardsInPlay;
  }

  public static List<CardDetails> DealHand()
  {
    var shuffledDeck = ShuffleDeck();
    var hand = new List<CardDetails>();

    foreach (var card in shuffledDeck.Keys.Take(5))
    {
      hand.Add(card);
    }

    return hand;
  }

  private static string GetCardColor(string suit)
  {
    return suit == "hearts" || suit == "diamonds" ? "red" : "black";
  }
}

