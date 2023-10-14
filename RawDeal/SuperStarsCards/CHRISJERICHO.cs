using RawDeal.Cards;
using RawDealView;
using RawDealView.Options;

namespace RawDeal.SuperStarsCards;

public class Chrisjericho: SuperStar
{
    private const int TotalCardsToDiscard = 1;
    private const int OneCard = 1;
    public Chrisjericho(SuperCard superCard, Player player, View view) : base(superCard, player, view)
    {
        SuperCard = superCard;
        Player = player;
        GameView = view;
    }
    
    public override bool HasTheConditionsToUseAbility()
    {
        // ELIMINAR
        // return Player.TransformCardsInHandIntoStringFormat().Count >= OneCard;
        return Player.ChooseWhichMazeOfCardsTransformToStringFormat(CardSet.Hand).Count >= OneCard;
    }

    public override void UseAbility(Player playerOnWait)
    {
        TheJerichoAbilityFirstPart(TotalCardsToDiscard);
        TheJerichoAbilitySecondPart(TotalCardsToDiscard, playerOnWait);
    }
    
    private void TheJerichoAbilityFirstPart(int totalCardsToDiscard)
    {
        // ELIMINAR
        // var indexCardToDiscard = GameView.AskPlayerToSelectACardToDiscard(Player.TransformCardsInHandIntoStringFormat(),
        //     Player.SuperStarCardInfo!.Name, 
        //     Player.SuperStarCardInfo!.Name, 
        //     totalCardsToDiscard);
        var indexCardToDiscard = GameView.AskPlayerToSelectACardToDiscard(
            Player.ChooseWhichMazeOfCardsTransformToStringFormat(CardSet.Hand),
            Player.SuperStarCardInfo!.Name, 
            Player.SuperStarCardInfo!.Name, 
            totalCardsToDiscard);
        Player.DiscardCardFromHandToRingside(indexCardToDiscard);
    }
    
    private void TheJerichoAbilitySecondPart(int totalCardsToDiscard, Player playerOnWait)
    {
        // ELIMINAR
        // var indexCardToDiscard = GameView.AskPlayerToSelectACardToDiscard(playerOnWait.TransformCardsInHandIntoStringFormat(),
        //     playerOnWait.SuperStarCardInfo!.Name, 
        //     playerOnWait.SuperStarCardInfo!.Name, 
        //     totalCardsToDiscard);
        var indexCardToDiscard = GameView.AskPlayerToSelectACardToDiscard(
            playerOnWait.ChooseWhichMazeOfCardsTransformToStringFormat(CardSet.Hand),
            playerOnWait.SuperStarCardInfo!.Name, 
            playerOnWait.SuperStarCardInfo!.Name, 
            totalCardsToDiscard);
        playerOnWait.DiscardCardFromHandToRingside(indexCardToDiscard);
    }
}