namespace RawDeal.Cards;

public class SuperCard
{
    public int HandSize;
    public int SuperstarValue;
    public string? SuperstarAbility;
    public readonly string Name;
    public string? Logo;
    
    public readonly SuperCardInfo SuperCardInfo = new();

    public SuperCard(string name)
    {
        Name = name;
        SuperCardInfo.LoadCardData(this);
    }
}