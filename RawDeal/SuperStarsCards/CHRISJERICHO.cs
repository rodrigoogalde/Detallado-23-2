using RawDeal.Cards;
using RawDealView;
using RawDealView.Options;

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
        return Player.ChooseWhichMazeOfCardsTransformToStringFormat(CardSet.Hand).Count >= OneCard;
    }

    public override void UseAbility(Player playerOnWait)
    {
        TheJerichoAbilityFirstPart(TotalCardsToDiscard);
        TheJerichoAbilitySecondPart(TotalCardsToDiscard, playerOnWait);
    }
    
    private void TheJerichoAbilityFirstPart(int totalCardsToDiscard)
    {
        var indexCardToDiscard = GameView.AskPlayerToSelectACardToDiscard(
            Player.ChooseWhichMazeOfCardsTransformToStringFormat(CardSet.Hand),
            SuperCard.Name, 
            SuperCard.Name, 
            totalCardsToDiscard);
        Player.DiscardCardFromHandToRingside(indexCardToDiscard);
    }
    
    private void TheJerichoAbilitySecondPart(int totalCardsToDiscard, Player playerOnWait)
    {
        SuperStar superStarOpponent = playerOnWait.SuperStar;
        var indexCardToDiscard = GameView.AskPlayerToSelectACardToDiscard(
            playerOnWait.ChooseWhichMazeOfCardsTransformToStringFormat(CardSet.Hand),
            superStarOpponent.Name!, 
            superStarOpponent.Name!, 
            totalCardsToDiscard);
        playerOnWait.DiscardCardFromHandToRingside(indexCardToDiscard);
    }
}