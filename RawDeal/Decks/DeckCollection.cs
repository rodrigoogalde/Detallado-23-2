using RawDeal.Cards;
using RawDealView.Formatters;

namespace RawDeal.Decks;

public class DeckCollection : Collection<Card>
{
    private readonly ICollection<Card> _deck;
    
    public DeckCollection(ICollection<Card> collectionImplementation) : base(collectionImplementation)
    {
        _deck = _collectionImplementation;
    }
    
    public List<string> TransformListOfCardsIntoStringFormat() =>
        (_deck.Count > EmptyDeck)
            ? _deck.Select(card => Formatter.CardToString(new FormatterCardInfo(card))).ToList()
            : new List<string>();
}