namespace RawDeal.Decks;

public class StringCollection : Collection<string>
{
    
    private readonly ICollection<string> _deck;
    
    public StringCollection(ICollection<string> collectionImplementation) : base(collectionImplementation)
    {
        _deck = _collectionImplementation;
    }
}