using RawDeal.Cards;
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
        PlayerDiscardCards(TotalCardsToDiscard);
        OpponentDiscardCards(TotalCardsToDiscard, playerOnWait);
    }
    
    private void PlayerDiscardCards(int totalCardsToDiscard)
    {
        var indexCardToDiscard = GameView.AskPlayerToSelectACardToDiscard(
            Player.TransformMazeToStringFormat(CardSetFull.Hand).ToList(),
            SuperCard.Name, 
            SuperCard.Name, 
            totalCardsToDiscard);
        Player.DiscardCardFromHandToRingsideWithIndex(indexCardToDiscard);
    }
    
    private void OpponentDiscardCards(int totalCardsToDiscard, Player playerOnWait)
    {
        SuperStar superStarOpponent = playerOnWait.SuperStar;
        var indexCardToDiscard = GameView.AskPlayerToSelectACardToDiscard(
            playerOnWait.TransformMazeToStringFormat(CardSetFull.Hand).ToList(),
            superStarOpponent.Name!, 
            superStarOpponent.Name!, 
            totalCardsToDiscard);
        playerOnWait.DiscardCardFromHandToRingsideWithIndex(indexCardToDiscard);
    }
}