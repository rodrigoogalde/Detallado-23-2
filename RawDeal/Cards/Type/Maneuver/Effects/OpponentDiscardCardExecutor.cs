using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Type.Maneuver.Effects;

public class OpponentDiscardCardExecutor: ICardManeuverStrategy
{
    private readonly View _view;
    private readonly int _cardsToDiscard;
    
    public OpponentDiscardCardExecutor(View view, int cardsToDiscard)
    {
        _view = view;
        _cardsToDiscard = cardsToDiscard;
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
        HandDiscardEffect handDiscardEffect = new HandDiscardEffect(_view, opponent, _cardsToDiscard);
        handDiscardEffect.Execute();
    }
}