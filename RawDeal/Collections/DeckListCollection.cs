using RawDeal.Cards;
using RawDeal.Cards.Formatter;
using RawDeal.Decks;
using RawDealView.Formatters;

namespace RawDeal.Collections;

public class DeckListCollection : ListCollection<Card>
{
    private readonly ICollection<Card> _deck;
    
    public DeckListCollection(ICollection<Card> collectionImplementation) : base(collectionImplementation)
    {
        _deck = CollectionImplementation;
    }
    
    public List<string> TransformListOfCardsIntoStringFormat() =>
        (_deck.Count > EmptyDeck)
            ? _deck.Select(card => Formatter.CardToString(new FormatterCardInfo(card))).ToList()
            : new List<string>();
}