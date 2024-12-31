using Godot;

using System;
using System.Collections.Generic;

public class ZoneManager
{
  private List<Card> _cards = new List<Card>();

  public void AddCard(Card card)
  {
    if (!_cards.Contains(card))
    {
      _cards.Add(card);
    }
  }

  public void RemoveCard(Card card)
  {
    _cards.Remove(card);
  }

  public List<Card> GetCards()
  {
    return new List<Card>(_cards); // Return a copy to avoid direct modification
  }

  public Card GetTopCard()
  {
    return _cards.Count > 0 ? _cards[_cards.Count - 1] : null;
  }

  public int GetCardCount()
  {
    return _cards.Count;
  }

  public List<Card> GetPileFromCard(Card startingCard)
  {
    var index = _cards.IndexOf(startingCard);
    if (index >= 0)
    {
      return _cards.GetRange(index, _cards.Count - index);
    }
    return new List<Card>();
  }

  public bool IsCardInZone(Card card)
  {
    return _cards.Contains(card);
  }
}
