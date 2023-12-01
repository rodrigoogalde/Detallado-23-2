using RawDeal.Cards.Maneuver;
using RawDeal.Effects;
using RawDeal.Exceptions;
using RawDeal.SuperStarsCards;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Type.Maneuver.Simple;

public class FishermansSuplex: ICardManeuverStrategy
{
    private readonly View _view;
    private readonly Player _player;
    private readonly SuperStar _superStar;
    
    public FishermansSuplex(View view, Player player)
    {
        _view = view;
        _player = player;
        _superStar = _player.SuperStar;
    }
    public bool IsEffectApplicable()
    {
        return true;
    }

    public void PerformEffect(FormatterCardRepresentation card, Player opponent)
    {
        PerformManeuver(opponent);
    }

    public void PerformManeuver(Player opponent)
    {
        RecieveCollateralDamage recieveCollateralDamage = new RecieveCollateralDamage(_view, _player);
        recieveCollateralDamage.Execute();
        int cardsToDiscard = _view.AskHowManyCardsToDrawBecauseOfACardEffect(_superStar.Name!, 1);
        DrawCard drawCards = new DrawCard(_player, _view, cardsToDiscard);
        drawCards.Execute();
    }
}
