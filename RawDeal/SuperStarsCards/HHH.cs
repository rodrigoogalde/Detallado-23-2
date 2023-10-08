using RawDeal.Cards;
using RawDealView;

namespace RawDeal.SuperStarsCards;

public class Hhh: SuperStar
{
    public Hhh(SuperCard superCard, Player player, View view) : base(superCard, player, view)
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
}