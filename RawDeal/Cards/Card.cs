using RawDeal.Decks;

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
    
    private const string CardPlayAsAction = "Action";
    private const string ReversalCardType = "Reversal";
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
        return (Types!.Contains("Maneuver") || Types.Contains("Action")) 
               && fortitude >= long.Parse(Fortitude) ;
    }
    
    public bool CanBeUsedAsReversal(int fortitude, Tuple<Card, string> cardPlayedByOpponent)
    {
        // return Subtypes!.Any(subtype => subtype.Contains(usedAs)) && fortitude >= int.Parse(Fortitude);
        return cardPlayedByOpponent.Item2 == CardPlayAsAction.ToUpper() ?
            ReversalAsAction(cardPlayedByOpponent.Item1, fortitude) : ReversalForCardType(fortitude, cardPlayedByOpponent.Item2);
    }

    private bool ReversalAsAction(Card opponentCard, int fortitude)
    {
        return (from subtype in Subtypes
            where ReversalForCardType(fortitude, ReversalCardType)
            select subtype.Split(ReversalCardType)[1]).Any(typeOfReversal => opponentCard.Subtypes!.Contains(typeOfReversal));
    }
    
    private bool ReversalForCardType(int fortitude, string usedAs)
    {
        return Subtypes!.Any(subtype => subtype.Contains(usedAs) && fortitude >= int.Parse(Fortitude));
    }
}