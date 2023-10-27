using RawDeal.Utils;

namespace RawDeal.Decks;

public class PlayeableCardsInHand: DeckCollection<FormatterCardRepresentation>
{
    
    private readonly ICollection<FormatterCardRepresentation> _deck;
    
    public PlayeableCardsInHand(ICollection<FormatterCardRepresentation> collectionImplementation) : base(collectionImplementation)
    {
        _deck = _collectionImplementation;
    }
}