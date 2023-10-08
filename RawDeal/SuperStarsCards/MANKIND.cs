using RawDeal.Cards;
using RawDealView;

namespace RawDeal.SuperStarsCards;

public class Mankind: SuperStar
{   
    public Mankind(SuperCard superCard, Player player, View view) : base(superCard, player, view)
    {
        SuperCard = superCard;
        Player = player;
        GameView = view;
    }

    public override bool HasTheConditionsToUseAbility()
    {
        return false;
    }

    public override void UseAbility(Player playerOnWait)
    {
    }

    public override bool CanSteelMoreThanOneCard()
    { return Player.CardsInArsenalInStringFormat().Count >= 1; }
    
    public override bool IsManKind()
    { return true; }
}