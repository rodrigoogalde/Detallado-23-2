namespace RawDeal.Collections;

public class StringListCollection : ListCollection<string>
{
    public StringListCollection(ICollection<string> collectionImplementation) : base(collectionImplementation)
    {
    }
}