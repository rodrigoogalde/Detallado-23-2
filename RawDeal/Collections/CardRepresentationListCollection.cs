using RawDeal.Utils;

namespace RawDeal.Decks;

public class CardRepresentationListCollection: ListCollection<FormatterCardRepresentation>
{
    
    private readonly ICollection<FormatterCardRepresentation> _deck;
    
    public CardRepresentationListCollection(ICollection<FormatterCardRepresentation> collectionImplementation) : base(collectionImplementation)
    {
        _deck = _collectionImplementation;
    }
}