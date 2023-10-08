using RawDeal.Cards;
using RawDealView;

namespace RawDeal.SuperStarsCards;

public class Therock: SuperStar
{   
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
        if (Player.CardsInRingsideInStringFormat().Count > 0) AskPlayerIfHeWantsToUseTheRockAbility();
    }

    private void AskPlayerIfHeWantsToUseTheRockAbility()
    {
        if (GameView.DoesPlayerWantToUseHisAbility(Player.SuperCard!.Name)) TheRockAbility();
    }
    
    private void TheRockAbility()
    {
        GameView.SayThatPlayerIsGoingToUseHisAbility(SuperCard.Name, SuperCard.SuperstarAbility!);
        int indexCardsToRecover = GameView.AskPlayerToSelectCardsToRecover(SuperCard.Name, 1, Player.CardsInRingsideInStringFormat());
        Player.MoveCardFromRingsideToArsenal(indexCardsToRecover);
    }
}