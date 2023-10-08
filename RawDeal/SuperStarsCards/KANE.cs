using RawDeal.Cards;
using RawDealView;

namespace RawDeal.SuperStarsCards;

public class Kane: SuperStar
{
    public Kane(SuperCard superCard, Player player, View view) : base(superCard, player, view)
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
    
    public override void UseAbilityBeforeDrawing(Player playerOnWait)
    {
        GameView.SayThatPlayerIsGoingToUseHisAbility(SuperCard.Name, SuperCard.SuperstarAbility!);
        GameView.SayThatSuperstarWillTakeSomeDamage(playerOnWait.SuperCard!.Name, 1);
        playerOnWait.TakeDamage(1);
    }
}