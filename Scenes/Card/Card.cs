using Godot;

using Solitaire.Classes;

public partial class Card : Node2D
{
  private Sprite2D _cardSprite;
  private Area2D _cardArea;

  public CardDetails Details;
  public Suit Suit;
  public SuitDetails SuitDetails;
  public Solitaire.Classes.Enums.Rank Rank;
  public bool IsFaceUp { get; set; }
  public bool IsDraggable { get; set; }

  public void Initialize(CardDetails details)
  {
    Details = details;
    Suit = details.Suit;
    Rank = details.Rank;
    SuitDetails = new SuitDetails(Suit);
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
    _cardSprite.Texture = faceUp ? AssetManager.cardTextures[$"{Rank}{Suit}"] : AssetManager.supportTextures["card_back"];
  }

  public void BringToFront()
  {
    ZIndex = 4096; // Arbitrary high value
  }
}
