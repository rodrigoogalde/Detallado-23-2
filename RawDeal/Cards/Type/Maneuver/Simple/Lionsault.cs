using RawDeal.Cards.Maneuver;
using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Type.Maneuver.Simple;

public class Lionsault: ICardManeuverStrategy
{
    private readonly View _view;
    private readonly Player _player;
    
    public Lionsault(View view, Player player)
    {
        _view = view;
        _player = player;
    }
    
    public bool IsEffectApplicable()
    {
        // Only aplicable after a 4D or greater maneuver
        return true;
    }

    public void PerformEffect(FormatterCardRepresentation card, Player opponent)
    {
        PerformManeuver(opponent);
    }

    public void PerformManeuver(Player opponent)
    {
        DiscardCardFromHand discardCardFromHand = new DiscardCardFromHand(_view, opponent, 1);
        discardCardFromHand.Execute();
    }
}