using RawDeal.Cards.Maneuver;
using RawDeal.Effects;
using RawDeal.Utils;

namespace RawDeal.Cards.Type.Maneuver.Effects;

public class PlayerMoveCardFromArsenalToRingSide: ICardManeuverStrategy
{
    private readonly IEffect _effect;
    
    public PlayerMoveCardFromArsenalToRingSide(Player player)
    {
        _effect = new MoveTopCardFromArsenalToRingsidePile(player);
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