using RawDeal.Cards;
using RawDealView;

namespace RawDeal.SuperStarsCards;

public class Stonecold: SuperStar
{    
    public Stonecold(SuperCard superCard, Player player, View view) : base(superCard, player, view)
    {
        SuperCard = superCard;
        Player = player;
        GameView = view;
    }

    public override bool HasTheConditionsToUseAbility()
    {
        return Player.CardsInArsenalInStringFormat().Count > 0;
    }

    public override void UseAbilityOncePerTurn()
    {
        throw new NotImplementedException();
    }

    public override void UseAbility(Player playerOnWait)
    {
        Player.MoveTopeCardFromArsenalToHand();
        GameView.SayThatPlayerDrawCards(SuperCard.Name, 1);
        Player.MoveCardFromHandToArsenalBottom(
            GameView.AskPlayerToReturnOneCardFromHisHandToHisArsenal(SuperCard.Name, Player.CardsInHandInStringFormat()
            ));
    }
}