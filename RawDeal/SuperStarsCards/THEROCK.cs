using RawDeal.Cards;
using RawDealView;
using RawDealView.Options;

namespace RawDeal.SuperStarsCards;

public class Therock: SuperStar
{   
    
    private const int NoCards = 0;
    
    public Therock(SuperCard superCard, Player player, View view) : base(superCard, player, view)
    {
        SuperCard = superCard;
        Player = player;
        GameView = view;
    }
    public override bool HasTheConditionsToUseAbility()
    {
        return false;
    }

    public override void UseAbility(Player playerOnWait)
    {
    }

    public override void UseAbilityBeforeDrawing(Player playerOnWait)
    {
        // ELIMINAR
        // if (Player.TransformCardsInRingsideIntoStringFormat().Count > NoCards) AskPlayerIfHeWantsToUseTheRockAbility();
        if (Player.ChooseWhichMazeOfCardsTransformToStringFormat(CardSet.RingsidePile).Count > NoCards) AskPlayerIfHeWantsToUseTheRockAbility();
    }

    private void AskPlayerIfHeWantsToUseTheRockAbility()
    {
        if (GameView.DoesPlayerWantToUseHisAbility(Player.SuperStarCardInfo!.Name)) TheRockAbility();
    }
    
    private void TheRockAbility()
    {
        const int cardsToRecover = 1;
        GameView.SayThatPlayerIsGoingToUseHisAbility(SuperCard.Name, SuperCard.SuperstarAbility!);
        // ELIMINAR
        // int indexCardsToRecover = GameView.AskPlayerToSelectCardsToRecover(SuperCard.Name, cardsToRecover, Player.TransformCardsInRingsideIntoStringFormat());
        int indexCardsToRecover = GameView.AskPlayerToSelectCardsToRecover(SuperCard.Name, cardsToRecover, Player.ChooseWhichMazeOfCardsTransformToStringFormat(CardSet.RingsidePile));
        Player.MoveCardFromRingsideToArsenal(indexCardsToRecover);
    }
}