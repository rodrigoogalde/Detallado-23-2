using RawDeal.Cards;
using RawDealView;

namespace RawDeal.SuperStarsCards;

public class Chrisjericho: SuperStar
{
    public Chrisjericho(SuperCard superCard, Player player, View view) : base(superCard, player, view)
    {
        SuperCard = superCard;
        Player = player;
        GameView = view;
    }
    
    public override bool HasTheConditionsToUseAbility()
    {
        return Player.CardsInHandInStringFormat().Count >= 1;
    }

    public override void UseAbility(Player playerOnWait)
    {
        TheJerichoAbilityFirstPart(1);
        TheJerichoAbilitySecondPart(1, playerOnWait);
    }
    
    private void TheJerichoAbilityFirstPart(int totalCardsToDiscard)
    {
        var indexCardToDiscard = GameView.AskPlayerToSelectACardToDiscard(Player.CardsInHandInStringFormat(),
            Player.SuperCard!.Name, 
            Player.SuperCard!.Name, 
            totalCardsToDiscard);
        Player.DiscardCardFromHandToRingside(indexCardToDiscard);
    }
    
    private void TheJerichoAbilitySecondPart(int totalCardsToDiscard, Player playerOnWait)
    {
        var indexCardToDiscard = GameView.AskPlayerToSelectACardToDiscard(playerOnWait.CardsInHandInStringFormat(),
            playerOnWait.SuperCard!.Name, 
            playerOnWait.SuperCard!.Name, 
            totalCardsToDiscard);
        playerOnWait.DiscardCardFromHandToRingside(indexCardToDiscard);
    }
}