using RawDeal.Cards;
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
        return Player.ChooseWhichMazeOfCardsTransformToStringFormat(CardSet.Hand).Count >= 2;
    }

    public override void UseAbility(Player playerOnWait)
    {
        for (int i = 2 ; i > 0 ; i--) TheUndertakerAbilityFirtsPart(i);
        TheUndertakerAbilitySecondPart();
    }
    
    private void TheUndertakerAbilityFirtsPart(int totalCardsToDiscard)
    {
        var indexCardToDiscard = GameView.AskPlayerToSelectACardToDiscard(Player.ChooseWhichMazeOfCardsTransformToStringFormat(CardSet.Hand),
            SuperCard.Name, SuperCard.Name, totalCardsToDiscard);
        Player.DiscardCardFromHandToRingside(indexCardToDiscard);
    }
    
    private void TheUndertakerAbilitySecondPart()
    {
        const int cardsToRecover = 1;
        var indexCardToTake = GameView.AskPlayerToSelectCardsToPutInHisHand(SuperCard.Name, cardsToRecover, Player.ChooseWhichMazeOfCardsTransformToStringFormat(CardSet.RingsidePile));
        Player.MoveCardFromRingsideToHand(indexCardToTake);
    }
}