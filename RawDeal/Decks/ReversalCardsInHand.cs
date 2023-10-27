using RawDeal.Utils;

namespace RawDeal.Decks;

public class ReversalCardsInHand : DeckCollection<FormatterCardRepresentation>
{
    
    private readonly ICollection<FormatterCardRepresentation> _deck;
    
    public ReversalCardsInHand(ICollection<FormatterCardRepresentation> collectionImplementation) : base(collectionImplementation)
    {
        _deck = _collectionImplementation;
    }
}