using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Maneuver;

public class OpponentsDiscardsCardsFromHand: ICardManeuverStrategy
{
    private IEffect _effect;
    
    public OpponentsDiscardsCardsFromHand(View view, Player player, int cardsToDiscard)
    {
        _effect = new DiscardCardFromHand(view, player, cardsToDiscard);
    }
    public bool IsEffectApplicable(Game game, Player player, Player playerOnWait)
    {
        return true;
    }

    public void PerformEffect(FormatterCardRepresentation card, Game game, Player player, Player playerOnWait)
    {
        TryPerformManeuver(card, game, player, playerOnWait);
    }

    public void TryPerformManeuver(FormatterCardRepresentation card, Game game, Player player, Player playerOnWait)
    {
        throw new NotImplementedException();
    }

    public void PerformManeuver(FormatterCardRepresentation card, Game game, Player player, Player playerOnWait)
    {
        throw new NotImplementedException();
    }
}