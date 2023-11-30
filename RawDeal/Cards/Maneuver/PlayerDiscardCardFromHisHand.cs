using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Maneuver;

public class PlayerDiscardCardFromHisHand : ICardManeuverStrategy
{
    private IEffect _effect;
    
    public PlayerDiscardCardFromHisHand(View view, Player player, int cardsToDiscard)
    {
        _effect = new DiscardCardFromHand(view, player, cardsToDiscard);
    }
    public bool IsEffectApplicable()
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