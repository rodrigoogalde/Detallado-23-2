using RawDeal.Cards;
using RawDealView;
using RawDealView.Options;

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
        // ELIMINAR
        // return Player.TransformCardsInArsenalIntoStringFormat().Count > NoCards;
        
        return Player.ChooseWhichMazeOfCardsTransformToStringFormat(CardSet.Arsenal).Count > NoCards;
    }

    public override void UseAbilityOncePerTurn()
    {
        throw new NotImplementedException();
    }

    public override void UseAbility(Player playerOnWait)
    {
        Player.MoveTopeCardFromArsenalToHand();
        GameView.SayThatPlayerDrawCards(SuperCard.Name, NumberOfCardsToDraw);
        // ELIMINAR
        // Player.MoveCardFromHandToArsenalBottom(
        //     GameView.AskPlayerToReturnOneCardFromHisHandToHisArsenal(SuperCard.Name, Player.TransformCardsInHandIntoStringFormat()
        //     ));
        Player.MoveCardFromHandToArsenalBottom(
            GameView.AskPlayerToReturnOneCardFromHisHandToHisArsenal(SuperCard.Name, Player.ChooseWhichMazeOfCardsTransformToStringFormat(CardSet.Hand)
            ));
    }
}