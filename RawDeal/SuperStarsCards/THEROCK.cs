using RawDeal.Cards;
using RawDeal.Collections;
using RawDeal.Effects;
using RawDeal.Options;
using RawDealView;
using RawDealView.Options;

namespace RawDeal.SuperStarsCards;

public class Therock: SuperStar
{   
    
    private const int NoCards = 0;
    
    public Therock(SuperCardInfo superCard, Player player, View view) : base(superCard, player, view)
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
        StringListCollection cardsInRingside = Player.TransformMazeToStringFormat(CardSetFull.RingsidePile);
        if (cardsInRingside.Count > NoCards) 
            AskPlayerIfHeWantsToUseTheRockAbility();
    }

    private void AskPlayerIfHeWantsToUseTheRockAbility()
    {
        if (GameView.DoesPlayerWantToUseHisAbility(SuperCard.Name)) TheRockPlaysHisAbility();
    }
    
    private void TheRockPlaysHisAbility()
    {
        const int cardsToRecover = 1;
        GameView.SayThatPlayerIsGoingToUseHisAbility(SuperCard.Name, SuperCard.SuperstarAbility!);
        DeckShufflingEffect deckShufflingEffect = new(GameView, Player, cardsToRecover);
        deckShufflingEffect.Execute();
    }
}