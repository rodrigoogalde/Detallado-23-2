using RawDeal.Utils;

namespace RawDeal.Collections;

public class CardRepresentationListCollection: ListCollection<FormatterCardRepresentation>
{
    
    private readonly ICollection<FormatterCardRepresentation> _deck;
    
    public CardRepresentationListCollection(ICollection<FormatterCardRepresentation> collectionImplementation) : base(collectionImplementation)
    {
        _deck = CollectionImplementation;
    }
}