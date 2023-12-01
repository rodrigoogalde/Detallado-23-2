using RawDeal.Cards.Maneuver;
using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Type.Maneuver.Simple;

public class Bulldog: ICardManeuverStrategy
{
    private readonly View _view;
    private readonly Player _player;
    
    public Bulldog(View view, Player player)
    {
        _view = view;
        _player = player;
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
        DiscardCardFromHand discardCardFromHand = new DiscardCardFromHand(_view, _player, 1);
        discardCardFromHand.Execute();
        
        // TODO discard card from opponent's hand
        DiscardCardFromOpponentHand discardCardFromOpponentHand = new DiscardCardFromOpponentHand(_view, _player,
            opponent, 1);
        discardCardFromOpponentHand.Execute();
    }
}