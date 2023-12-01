using RawDeal.Cards.Maneuver;
using RawDeal.Utils;

namespace RawDeal.Cards.Type.Maneuver.Effects;

public class NoManeuverEffect : ICardManeuverStrategy
{
    public bool IsEffectApplicable()
    {
        return false;
    }

    public void PerformEffect(FormatterCardRepresentation card, Player opponent)
    {
        return;
    }

    public void PerformManeuver(Player opponent)
    {
        return;
    }
}