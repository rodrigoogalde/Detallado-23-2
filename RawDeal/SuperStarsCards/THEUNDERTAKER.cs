using RawDeal.Cards;
using RawDealView;

namespace RawDeal.SuperStarsCards;

public class Theundertaker: SuperStar
{    
    public Theundertaker(SuperCard superCard, Player player, View view) : base(superCard, player, view)
    {
        SuperCard = superCard;
        Player = player;
        GameView = view;
    }

    public override bool HasTheConditionsToUseAbility()
    {
        return Player.CardsInHandInStringFormat().Count >= 2;
    }

    public override void UseAbility(Player playerOnWait)
    {
        for (int i = 2 ; i > 0 ; i--) TheUndertakerAbilityFirtsPart(i);
        TheUndertakerAbilitySecondPart();
    }
    
    private void TheUndertakerAbilityFirtsPart(int totalCardsToDiscard)
    {
        var indexCardToDiscard = GameView.AskPlayerToSelectACardToDiscard(Player.CardsInHandInStringFormat(),
            SuperCard.Name, SuperCard.Name, totalCardsToDiscard);
        Player.DiscardCardFromHandToRingside(indexCardToDiscard);
    }
    
    private void TheUndertakerAbilitySecondPart()
    {
        var indexCardToTake = GameView.AskPlayerToSelectCardsToPutInHisHand(SuperCard.Name, 1, Player.CardsInRingsideInStringFormat());
        Player.MoveCardFromRingsideToHand(indexCardToTake);
    }
}