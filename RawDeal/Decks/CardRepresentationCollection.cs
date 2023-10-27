using RawDeal.Utils;

namespace RawDeal.Decks;

public class CardRepresentationCollection: Collection<FormatterCardRepresentation>
{
    
    private readonly ICollection<FormatterCardRepresentation> _deck;
    
    public CardRepresentationCollection(ICollection<FormatterCardRepresentation> collectionImplementation) : base(collectionImplementation)
    {
        _deck = _collectionImplementation;
    }
}