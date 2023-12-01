using RawDeal.Cards;
using RawDeal.Effects;
using RawDeal.Options;
using RawDealView;

namespace RawDeal.SuperStarsCards;

public class Chrisjericho: SuperStar
{
    private const int TotalCardsToDiscard = 1;
    private const int OneCard = 1;
    public Chrisjericho(SuperCardInfo superCard, Player player, View view) : base(superCard, player, view)
    {
        SuperCard = superCard;
        Player = player;
        GameView = view;
    }
    
    public override bool HasTheConditionsToUseAbility()
    {
        return Player.TransformMazeToStringFormat(CardSetFull.Hand).Count >= OneCard;
    }

    public override void UseAbility(Player playerOnWait)
    {
        DiscardCardFromHand discardCardFromHand = new(GameView, Player, TotalCardsToDiscard);
        discardCardFromHand.Execute();
        DiscardCardFromHand discardCardFromOpponentHand = new(GameView, playerOnWait, TotalCardsToDiscard);
        discardCardFromOpponentHand.Execute();
    }
}