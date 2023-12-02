using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Type.Maneuver.Effects;

public class ShuffleExecutor: ICardManeuverStrategy
{
    private readonly IEffect _effect;
    
    public ShuffleExecutor(View view, Player player, int numberOfCards)
    {
        _effect = new DeckShufflingEffect(view, player, numberOfCards);
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