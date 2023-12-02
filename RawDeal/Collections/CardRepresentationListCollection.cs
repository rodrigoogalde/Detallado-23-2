using RawDeal.Utils;

namespace RawDeal.Collections;

public class CardRepresentationListCollection: ListCollection<FormatterCardRepresentation>
{
    public CardRepresentationListCollection(ICollection<FormatterCardRepresentation> collectionImplementation) : base(collectionImplementation)
    {
    }
}