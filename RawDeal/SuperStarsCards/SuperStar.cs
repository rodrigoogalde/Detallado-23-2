using RawDeal.Cards;
using RawDealView;

namespace RawDeal.SuperStarsCards;

public abstract class SuperStar
{
    public SuperCardInfo SuperCard { get; set; }
    protected Player Player { get; set; }
    protected View GameView { get; set; }
    public string? Name { get; set; }
    public string? Logo { get; set; }
    public int HandSize { get; set; }
    public int SuperstarValue { get; set; }
    public string? SuperstarAbility { get; set;}

    protected SuperStar(SuperCardInfo superCard, Player player, View view)
    {
        SuperCard = superCard;
        Player = player;
        GameView = view;
        Name = superCard.Name;
        Logo = superCard.Logo;
        HandSize = superCard.HandSize;
        SuperstarValue = superCard.SuperstarValue;
        SuperstarAbility = superCard.SuperstarAbility;
    }

    public abstract bool HasTheConditionsToUseAbility();

    public virtual void UseAbilityBeforeDrawing(Player playerOnWait)
    {
    }

    public virtual void UseAbilityOncePerTurn()
    {
    }

    public abstract void UseAbility(Player playerOnWait);

    public virtual bool CanSteelMoreThanOneCard()
    {
        return false;
    }

    public virtual bool IsManKind()
    {
        return false;
    }
}