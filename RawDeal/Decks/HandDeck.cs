using RawDeal.Cards;

namespace RawDeal.Decks;

public sealed class HandDeck : Collection<Card>
{
    private List<Card> _cards = new();
    private const int EmptyDeck = 0;

    public Card this[int index]
    {
        get { throw new NotImplementedException(); }
    }
}