using Godot;

using Solitaire.Classes;
using Solitaire.Classes.Enums;

using System;
using System.Collections.Generic;

public partial class AssetManager : Node
{
  public static AssetManager Instance;

  public static Dictionary<string, Texture2D> cardTextures = new Dictionary<string, Texture2D>();
  public static Dictionary<string, Texture2D> supportTextures = new Dictionary<string, Texture2D>();

  public static Dictionary<string, (Rank rank, Suit suit)> cards = new Dictionary<string, (Rank rank, Suit suit)>
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

  public override void _Ready()
  {
    Instance = this;
    LoadCardTextures();
    LoadSupportTextures();
  }


  private void LoadCardTextures()
  {
    cardTextures.Add($"{Rank.Two}{Suit.Clubs}", (Texture2D) GD.Load("res://assets/cards/card_clubs_02.png"));
    cardTextures.Add($"{Rank.Three}{Suit.Clubs}", (Texture2D) GD.Load("res://assets/cards/card_clubs_03.png"));
    cardTextures.Add($"{Rank.Four}{Suit.Clubs}", (Texture2D) GD.Load("res://assets/cards/card_clubs_04.png"));
    cardTextures.Add($"{Rank.Five}{Suit.Clubs}", (Texture2D) GD.Load("res://assets/cards/card_clubs_05.png"));
    cardTextures.Add($"{Rank.Six}{Suit.Clubs}", (Texture2D) GD.Load("res://assets/cards/card_clubs_06.png"));
    cardTextures.Add($"{Rank.Seven}{Suit.Clubs}", (Texture2D) GD.Load("res://assets/cards/card_clubs_07.png"));
    cardTextures.Add($"{Rank.Eight}{Suit.Clubs}", (Texture2D) GD.Load("res://assets/cards/card_clubs_08.png"));
    cardTextures.Add($"{Rank.Nine}{Suit.Clubs}", (Texture2D) GD.Load("res://assets/cards/card_clubs_09.png"));
    cardTextures.Add($"{Rank.Ten}{Suit.Clubs}", (Texture2D) GD.Load("res://assets/cards/card_clubs_10.png"));
    cardTextures.Add($"{Rank.Jack}{Suit.Clubs}", (Texture2D) GD.Load("res://assets/cards/card_clubs_J.png"));
    cardTextures.Add($"{Rank.Queen}{Suit.Clubs}", (Texture2D) GD.Load("res://assets/cards/card_clubs_Q.png"));
    cardTextures.Add($"{Rank.King}{Suit.Clubs}", (Texture2D) GD.Load("res://assets/cards/card_clubs_K.png"));
    cardTextures.Add($"{Rank.Ace}{Suit.Clubs}", (Texture2D) GD.Load("res://assets/cards/card_clubs_A.png"));
    cardTextures.Add($"{Rank.Two}{Suit.Diamonds}", (Texture2D) GD.Load("res://assets/cards/card_diamonds_02.png"));
    cardTextures.Add($"{Rank.Three}{Suit.Diamonds}", (Texture2D) GD.Load("res://assets/cards/card_diamonds_03.png"));
    cardTextures.Add($"{Rank.Four}{Suit.Diamonds}", (Texture2D) GD.Load("res://assets/cards/card_diamonds_04.png"));
    cardTextures.Add($"{Rank.Five}{Suit.Diamonds}", (Texture2D) GD.Load("res://assets/cards/card_diamonds_05.png"));
    cardTextures.Add($"{Rank.Six}{Suit.Diamonds}", (Texture2D) GD.Load("res://assets/cards/card_diamonds_06.png"));
    cardTextures.Add($"{Rank.Seven}{Suit.Diamonds}", (Texture2D) GD.Load("res://assets/cards/card_diamonds_07.png"));
    cardTextures.Add($"{Rank.Eight}{Suit.Diamonds}", (Texture2D) GD.Load("res://assets/cards/card_diamonds_08.png"));
    cardTextures.Add($"{Rank.Nine}{Suit.Diamonds}", (Texture2D) GD.Load("res://assets/cards/card_diamonds_09.png"));
    cardTextures.Add($"{Rank.Ten}{Suit.Diamonds}", (Texture2D) GD.Load("res://assets/cards/card_diamonds_10.png"));
    cardTextures.Add($"{Rank.Jack}{Suit.Diamonds}", (Texture2D) GD.Load("res://assets/cards/card_diamonds_J.png"));
    cardTextures.Add($"{Rank.Queen}{Suit.Diamonds}", (Texture2D) GD.Load("res://assets/cards/card_diamonds_Q.png"));
    cardTextures.Add($"{Rank.King}{Suit.Diamonds}", (Texture2D) GD.Load("res://assets/cards/card_diamonds_K.png"));
    cardTextures.Add($"{Rank.Ace}{Suit.Diamonds}", (Texture2D) GD.Load("res://assets/cards/card_diamonds_A.png"));
    cardTextures.Add($"{Rank.Two}{Suit.Hearts}", (Texture2D) GD.Load("res://assets/cards/card_hearts_02.png"));
    cardTextures.Add($"{Rank.Three}{Suit.Hearts}", (Texture2D) GD.Load("res://assets/cards/card_hearts_03.png"));
    cardTextures.Add($"{Rank.Four}{Suit.Hearts}", (Texture2D) GD.Load("res://assets/cards/card_hearts_04.png"));
    cardTextures.Add($"{Rank.Five}{Suit.Hearts}", (Texture2D) GD.Load("res://assets/cards/card_hearts_05.png"));
    cardTextures.Add($"{Rank.Six}{Suit.Hearts}", (Texture2D) GD.Load("res://assets/cards/card_hearts_06.png"));
    cardTextures.Add($"{Rank.Seven}{Suit.Hearts}", (Texture2D) GD.Load("res://assets/cards/card_hearts_07.png"));
    cardTextures.Add($"{Rank.Eight}{Suit.Hearts}", (Texture2D) GD.Load("res://assets/cards/card_hearts_08.png"));
    cardTextures.Add($"{Rank.Nine}{Suit.Hearts}", (Texture2D) GD.Load("res://assets/cards/card_hearts_09.png"));
    cardTextures.Add($"{Rank.Ten}{Suit.Hearts}", (Texture2D) GD.Load("res://assets/cards/card_hearts_10.png"));
    cardTextures.Add($"{Rank.Jack}{Suit.Hearts}", (Texture2D) GD.Load("res://assets/cards/card_hearts_J.png"));
    cardTextures.Add($"{Rank.Queen}{Suit.Hearts}", (Texture2D) GD.Load("res://assets/cards/card_hearts_Q.png"));
    cardTextures.Add($"{Rank.King}{Suit.Hearts}", (Texture2D) GD.Load("res://assets/cards/card_hearts_K.png"));
    cardTextures.Add($"{Rank.Ace}{Suit.Hearts}", (Texture2D) GD.Load("res://assets/cards/card_hearts_A.png"));
    cardTextures.Add($"{Rank.Two}{Suit.Spades}", (Texture2D) GD.Load("res://assets/cards/card_spades_02.png"));
    cardTextures.Add($"{Rank.Three}{Suit.Spades}", (Texture2D) GD.Load("res://assets/cards/card_spades_03.png"));
    cardTextures.Add($"{Rank.Four}{Suit.Spades}", (Texture2D) GD.Load("res://assets/cards/card_spades_04.png"));
    cardTextures.Add($"{Rank.Five}{Suit.Spades}", (Texture2D) GD.Load("res://assets/cards/card_spades_05.png"));
    cardTextures.Add($"{Rank.Six}{Suit.Spades}", (Texture2D) GD.Load("res://assets/cards/card_spades_06.png"));
    cardTextures.Add($"{Rank.Seven}{Suit.Spades}", (Texture2D) GD.Load("res://assets/cards/card_spades_07.png"));
    cardTextures.Add($"{Rank.Eight}{Suit.Spades}", (Texture2D) GD.Load("res://assets/cards/card_spades_08.png"));
    cardTextures.Add($"{Rank.Nine}{Suit.Spades}", (Texture2D) GD.Load("res://assets/cards/card_spades_09.png"));
    cardTextures.Add($"{Rank.Ten}{Suit.Spades}", (Texture2D) GD.Load("res://assets/cards/card_spades_10.png"));
    cardTextures.Add($"{Rank.Jack}{Suit.Spades}", (Texture2D) GD.Load("res://assets/cards/card_spades_J.png"));
    cardTextures.Add($"{Rank.Queen}{Suit.Spades}", (Texture2D) GD.Load("res://assets/cards/card_spades_Q.png"));
    cardTextures.Add($"{Rank.King}{Suit.Spades}", (Texture2D) GD.Load("res://assets/cards/card_spades_K.png"));
    cardTextures.Add($"{Rank.Ace}{Suit.Spades}", (Texture2D) GD.Load("res://assets/cards/card_spades_A.png"));
  }

  private void LoadSupportTextures()
  {
    supportTextures.Add("card_empty", (Texture2D) GD.Load("res://assets/cards/card_empty.png"));
    supportTextures.Add("card_back", (Texture2D) GD.Load("res://assets/cards/card_back.png"));
  }
}
