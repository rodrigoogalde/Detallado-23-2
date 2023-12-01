using System.Collections;

namespace RawDeal.Collections;

public abstract class ListCollection<T> : ICollection<T>
{
    protected readonly ICollection<T> CollectionImplementation;
    protected const int EmptyDeck = 0;

    protected ListCollection(ICollection<T> collectionImplementation)
    {
        CollectionImplementation = collectionImplementation;
    }


    public IEnumerator<T> GetEnumerator()
    {
        return CollectionImplementation.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)CollectionImplementation).GetEnumerator();
    }

    public void Add(T item)
    {
        CollectionImplementation.Add(item);
    }

    public void Clear()
    {
        CollectionImplementation.Clear();
    }

    public bool Contains(T item)
    {
        return CollectionImplementation.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        CollectionImplementation.CopyTo(array, arrayIndex);
    }

    public bool Remove(T item)
    {
        return CollectionImplementation.Remove(item);
    }
    
    public void Insert(int index, T item)
    {
        CheckIfIndexIsValid(index);
        CheckIfIndexIsSupported();
        ((List<T>)CollectionImplementation).Insert(index, item);
    }

    public int Count => CollectionImplementation.Count;

    public bool IsReadOnly => CollectionImplementation.IsReadOnly;

    public T this[int index]
    {
        get
        {
            CheckIfIndexIsValid(index);
            CheckIfIndexIsSupported();
            return ((List<T>)CollectionImplementation)[index];
        }
    }

    private void CheckIfIndexIsValid(int index)
    {
        if (index < 0 || Count < index)
        {
            throw new IndexOutOfRangeException("Index is out of range.");
        }
    }

    private void CheckIfIndexIsSupported()
    {
        if (CollectionImplementation is not List<T>)
        {
            throw new IndexOutOfRangeException("Index is out of range.");
        }
    }
}