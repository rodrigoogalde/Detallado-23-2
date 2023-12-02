using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Type.Maneuver.Effects;

public class DiscardCardExecutor : ICardManeuverStrategy
{
    private readonly IEffect _effect;
    private readonly Player _player;
    
    public DiscardCardExecutor(View view, Player player, int cardsToDiscard)
    {
        _effect = new HandDiscardEffect(view, player, cardsToDiscard);
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