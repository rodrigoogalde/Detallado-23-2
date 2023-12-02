using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Type.Maneuver.Effects;

public class OpponentCardDrawExecutor : ICardManeuverStrategy
{
    private readonly int _cardsToDiscard;
    private readonly View _view;
 
    public OpponentCardDrawExecutor(View view, int cardsToDiscard)
    {
        _cardsToDiscard = cardsToDiscard;
        _view = view;
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
        DrawCardEffect drawCardEffect = new DrawCardEffect(opponent, _view, _cardsToDiscard);
        drawCardEffect.Execute();
    }
}