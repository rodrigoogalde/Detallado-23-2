using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Maneuver;

public class OpponentsDiscardsCardsFromHand: ICardManeuverStrategy
{
    private readonly IEffect _effect;
    
    public OpponentsDiscardsCardsFromHand(View view, Player player, int cardsToDiscard)
    {
        _effect = new DiscardCardFromHand(view, player, cardsToDiscard);
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
        _effect.Execute();
    }
}