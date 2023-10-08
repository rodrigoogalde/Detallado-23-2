using RawDeal.Cards;
using RawDealView;

namespace RawDeal.SuperStarsCards;

public abstract class SuperStar
{
    protected SuperCard SuperCard { get; set; }
    protected Player Player { get; set; }
    protected View GameView { get; set; }

    protected SuperStar(SuperCard superCard, Player player, View view)
    {
        SuperCard = superCard;
        Player = player;
        GameView = view;
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
    { return false; }
    
    public virtual bool IsManKind()
    { return false; }
}