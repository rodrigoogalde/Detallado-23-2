namespace RawDeal.Cards;

public class SuperCardInfo
{
    public int HandSize;
    public int SuperstarValue;
    public string? SuperstarAbility;
    public readonly string Name;
    public string? Logo;

    private readonly SuperCardFormatter _loaderSuperCardInfo = new();

    public SuperCardInfo(string name)
    {
        Name = name;
        _loaderSuperCardInfo.LoadCardData(this);
    }
}