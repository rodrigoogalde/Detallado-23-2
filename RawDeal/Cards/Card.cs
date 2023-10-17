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
    private readonly CardInfo _cardInfo = new();
    
    public Card(string title)
    {
        Title = title;
        _cardInfo.LoadCardData(this);
    }
    
    public void CheckIfHaveAnotherLogo(SuperCardInfo superCard)
    {
        SuperCardFormatter superCardsInfo = new();
        foreach (var unused in superCardsInfo.CardsJson!.Where(superCardInfo => Subtypes!.Contains(superCardInfo.Logo!) && superCard.Logo != superCardInfo.Logo))
        {
            HasAnotherLogo = true;
        }
    }
    
    public bool CanBeUsedAsReversal(int fortitude, List<string> subTypes)
    {
        return Types!.Contains("Reversal") && int.Parse(Fortitude) >= fortitude && Subtypes!.Intersect(subTypes).Any();
    }
}