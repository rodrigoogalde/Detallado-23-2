using RawDeal.Cards;

namespace RawDeal.Decks;

public class PlayersDecksCollections
{
    private DeckCollection _cardsInRingside;
    private DeckCollection _cardsInRingArea;
    private DeckCollection _cardsInHand;
    private DeckCollection _cardsInArsenal;
    
    public PlayersDecksCollections()
    {
        _cardsInArsenal = new DeckCollection(new List<Card>());
        _cardsInHand = new DeckCollection(new List<Card>());
        _cardsInRingside = new DeckCollection(new List<Card>());
        _cardsInRingArea = new DeckCollection(new List<Card>());
    }
}