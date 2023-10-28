namespace RawDeal.Decks;

public class StringListCollection : ListCollection<string>
{
    
    private readonly ICollection<string> _deck;
    
    public StringListCollection(ICollection<string> collectionImplementation) : base(collectionImplementation)
    {
        _deck = _collectionImplementation;
    }
}