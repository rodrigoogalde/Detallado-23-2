using RawDeal.Cards.Maneuver;
using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Type.Maneuver.Effects;

public class PlayerDiscardCardFromHisHand : ICardManeuverStrategy
{
    private IEffect _effect;
    private readonly Player _player;
    
    public PlayerDiscardCardFromHisHand(View view, Player player, int cardsToDiscard)
    {
        _effect = new DiscardCardFromHand(view, player, cardsToDiscard);
        _player = player;
    }
    public bool IsEffectApplicable()
    {
        return _player.GetHandCards().Count >= 1;
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