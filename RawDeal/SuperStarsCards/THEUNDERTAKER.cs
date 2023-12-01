using RawDeal.Cards;
using RawDeal.Effects;
using RawDeal.Options;
using RawDealView;
using RawDealView.Options;

namespace RawDeal.SuperStarsCards;

public class Theundertaker: SuperStar
{    
    public Theundertaker(SuperCardInfo superCard, Player player, View view) : base(superCard, player, view)
    {
        SuperCard = superCard;
        Player = player;
        GameView = view;
    }

    public override bool HasTheConditionsToUseAbility()
    {
        return Player.TransformMazeToStringFormat(CardSetFull.Hand).Count >= 2;
    }

    public override void UseAbility(Player playerOnWait)
    {
        TheUndertakerDiscardCards();
        TheUndertakerTakeCardsFromRingside();
    }
    
    private void TheUndertakerDiscardCards()
    {
        DiscardCardFromHand discardCardFromHand = new(GameView, Player, 2);
        discardCardFromHand.Execute();
    }
    
    private void TheUndertakerTakeCardsFromRingside()
    {
        const int cardsToRecover = 1;
        var indexCardToTake = GameView.AskPlayerToSelectCardsToPutInHisHand(SuperCard.Name, cardsToRecover, 
            Player.TransformMazeToStringFormat(CardSetFull.RingsidePile).ToList());
        Player.MoveCardFromRingsideToHandWithIndex(indexCardToTake);
    }
}