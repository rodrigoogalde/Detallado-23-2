using System.Collections;

namespace RawDeal.Decks;

public abstract class Collection<T>: ICollection<T>
{
    private readonly ICollection<T> _collectionImplementation;

    protected Collection(ICollection<T> collectionImplementation)
    {
        _collectionImplementation = collectionImplementation;
    }

    protected Collection()
    {
        throw new NotImplementedException();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _collectionImplementation.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_collectionImplementation).GetEnumerator();
    }

    public void Add(T item)
    {
        _collectionImplementation.Add(item);
    }

    public void Clear()
    {
        _collectionImplementation.Clear();
    }

    public bool Contains(T item)
    {
        return _collectionImplementation.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        _collectionImplementation.CopyTo(array, arrayIndex);
    }

    public bool Remove(T item)
    {
        return _collectionImplementation.Remove(item);
    }

    public int Count => _collectionImplementation.Count;

    public bool IsReadOnly => _collectionImplementation.IsReadOnly;
}