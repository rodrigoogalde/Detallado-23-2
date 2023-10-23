using System.Collections;
using RawDeal.Cards;
using RawDealView.Formatters;

namespace RawDeal.Decks;

public abstract class DeckCollection
{
    private List<Card> _cards = new();
    private const int EmptyDeck = 0;
    
    public virtual void Add(Card card)
    {
        _cards.Add(card);
    }
    
    public virtual void Remove(Card card)
    {
        _cards.Remove(card);
    }
    
    public virtual void Insert(int index, Card card)
    {
        _cards.Insert(index, card);
    }
    
    public virtual List<string> TransformListOfCardsIntoStringFormat() =>
        (_cards.Count > EmptyDeck)
            ? _cards.Select(card => Formatter.CardToString(new FormatterCardInfo(card))).ToList()
            : new List<string>();
}