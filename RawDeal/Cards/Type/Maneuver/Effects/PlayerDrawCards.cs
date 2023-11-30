using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Maneuver;

public class PlayerDrawCards : ICardManeuverStrategy
{
    private IEffect _effect;
    
    public PlayerDrawCards(View view, Player player, int cardsToDiscard)
    {
        _effect = new DrawCard(player, view, cardsToDiscard);
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