using RawDeal.Decks;
using RawDeal.Options;

namespace RawDeal.Cards;

public class Card
{
    public readonly string Title;
    public List<string>? Types;
    public List<string>? Subtypes;
    public string Fortitude = null!;
    public string StunValue = null!;
    public string? CardEffect;
    public string? Damage;
    public CardSetFull PlayedFromDeck;
    
    private const string CardPlayAsAction = "Action";
    private const string CardPlayAsManeuver = "Maneuver";
    
    private readonly LoaderCardInfo _loaderCardInfo = new();
    
    public Card(string title)
    {
        Title = title;
        _loaderCardInfo.LoadCardData(this);
    }
    
    public bool CheckIfHaveAnotherLogo(SuperCardInfo superCard)
    {
        LoaderSuperCardInfo superCardsInfo = new();
        bool hasAnotherLogo = false;
        foreach (var unused in superCardsInfo.CardsJson!.
                     Where(superCardInfo => Subtypes!.Contains(superCardInfo.Logo!) 
                                            && superCard.Logo != superCardInfo.Logo))
        {
            hasAnotherLogo = true;
        }
        return hasAnotherLogo;
    }

    public bool IsPlayeableCard(int fortitude)
    {
        return (Types!.Contains(CardPlayAsManeuver) || Types.Contains(CardPlayAsAction)) 
               && fortitude >= long.Parse(Fortitude) ;
    }
    
    public bool CanBeUsedAsReversal(int fortitude, string usedAs)
    {
        return Subtypes!.Any(subtype => subtype.Contains(usedAs)) && fortitude >= int.Parse(Fortitude);
    }
    
    public void SetPlayedFromDeck(CardSetFull deckName)
    {
        PlayedFromDeck = deckName;
    }

}