using System.Collections;
using RawDeal.Cards;
using RawDealView.Formatters;

namespace RawDeal.Decks;

public abstract class Collection<T> : ICollection<T>
{
    protected ICollection<T> _collectionImplementation;
    protected const int EmptyDeck = 0;

    protected Collection(ICollection<T> collectionImplementation)
    {
        _collectionImplementation = collectionImplementation;
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
    
    public void Insert(int index, T item)
    {
        CheckIfIndexIsValid(index);
        CheckIfIndexIsSupported();
        ((List<T>)_collectionImplementation).Insert(index, item);
    }

    public int Count => _collectionImplementation.Count;

    public bool IsReadOnly => _collectionImplementation.IsReadOnly;

    public T this[int index]
    {
        get
        {
            CheckIfIndexIsValid(index);
            CheckIfIndexIsSupported();
            return ((List<T>)_collectionImplementation)[index];
        }
    }

    private void CheckIfIndexIsValid(int index)
    {
        if (index < 0 || index >= Count)
        {
            throw new IndexOutOfRangeException("Index is out of range.");
        }
    }

    private void CheckIfIndexIsSupported()
    {
        if (_collectionImplementation is not List<T> list)
        {
            throw new IndexOutOfRangeException("Index is out of range.");
        }
    }
}