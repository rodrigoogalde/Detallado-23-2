using RawDeal.Cards;
using RawDeal.Options;
using RawDealView;
using RawDealView.Options;

namespace RawDeal.SuperStarsCards;

public class Stonecold: SuperStar
{    
    
    private const int NoCards = 0;
    private const int NumberOfCardsToDraw = 1;
    public Stonecold(SuperCardInfo superCard, Player player, View view) : base(superCard, player, view)
    {
        SuperCard = superCard;
        Player = player;
        GameView = view;
    }

    public override bool HasTheConditionsToUseAbility()
    {
        return Player.TransformMazeToStringFormat(CardSetFull.Arsenal).Count > NoCards;
    }

    public override void UseAbilityOncePerTurn()
    {
        throw new NotImplementedException();
    }

    public override void UseAbility(Player playerOnWait)
    {
        Player.MoveTopCardFromArsenalToHand();
        GameView.SayThatPlayerDrawCards(SuperCard.Name, NumberOfCardsToDraw);
        Player.MoveCardFromHandToArsenalBottomWithIndex(
            GameView.AskPlayerToReturnOneCardFromHisHandToHisArsenal(SuperCard.Name,
                Player.TransformMazeToStringFormat(CardSetFull.Hand).ToList()
            ));
    }
}