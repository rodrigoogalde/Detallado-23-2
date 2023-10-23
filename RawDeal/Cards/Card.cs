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

    public bool HasAnotherLogo;
    private readonly LoaderCardInfo _loaderCardInfo = new();
    
    public Card(string title)
    {
        Title = title;
        _loaderCardInfo.LoadCardData(this);
    }
    
    public void CheckIfHaveAnotherLogo(SuperCardInfo superCard)
    {
        SuperCardFormatter superCardsInfo = new();
        foreach (var unused in superCardsInfo.CardsJson!.Where(superCardInfo => Subtypes!.Contains(superCardInfo.Logo!) && superCard.Logo != superCardInfo.Logo))
        {
            HasAnotherLogo = true;
        }
    }

    public bool IsPlayeableCard(int fortitude)
    {
        return (Types!.Contains("Maneuver") || Types.Contains("Action")) 
               && fortitude >= long.Parse(Fortitude) ;
    }
    
    public bool CanBeUsedAsReversal(int fortitude, List<string> subTypes)
    {
        return Types!.Contains("Reversal") && int.Parse(Fortitude) >= fortitude && Subtypes!.Intersect(subTypes).Any();
    }
}