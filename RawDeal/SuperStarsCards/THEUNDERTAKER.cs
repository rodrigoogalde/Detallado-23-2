using RawDeal.Cards;
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
        for (int i = 2 ; i > 0 ; i--) TheUndertakerDiscardCards(i);
        TheUndertakerTakeCardsFromRingside();
    }
    
    private void TheUndertakerDiscardCards(int totalCardsToDiscard)
    {
        var indexCardToDiscard = GameView.AskPlayerToSelectACardToDiscard(
            Player.TransformMazeToStringFormat(CardSetFull.Hand).ToList(),
            SuperCard.Name, SuperCard.Name, totalCardsToDiscard);
        Player.DiscardCardFromHandToRingsideWithIndex(indexCardToDiscard);
    }
    
    private void TheUndertakerTakeCardsFromRingside()
    {
        const int cardsToRecover = 1;
        var indexCardToTake = GameView.AskPlayerToSelectCardsToPutInHisHand(SuperCard.Name, cardsToRecover, 
            Player.TransformMazeToStringFormat(CardSetFull.RingsidePile).ToList());
        Player.MoveCardFromRingsideToHandWithIndex(indexCardToTake);
    }
}