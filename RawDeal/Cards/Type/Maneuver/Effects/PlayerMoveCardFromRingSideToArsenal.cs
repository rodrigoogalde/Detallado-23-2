using RawDeal.Cards.Maneuver;
using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Type.Maneuver.Effects;

public class PlayerMoveCardFromRingSideToArsenal: ICardManeuverStrategy
{
    private readonly IEffect _effect;
    
    public PlayerMoveCardFromRingSideToArsenal(View view, Player player, int numberOfCards)
    {
        _effect = new ShuffleCardsFromRingsideToArsenal(view, player, numberOfCards);
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