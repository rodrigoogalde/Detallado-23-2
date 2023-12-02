using RawDeal.Cards;
using RawDeal.Collections;
using RawDeal.Options;
using RawDealView;

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
        StringListCollection cardsInArsenal = Player.TransformMazeToStringFormat(CardSetFull.Arsenal);
        return cardsInArsenal.Count > NoCards;
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