using RawDeal.Cards.Maneuver;
using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Type.Maneuver.Simple;

public class GuillotineStretch: ICardManeuverStrategy
{
    private readonly View _view;
    private readonly Player _player;
    
    public GuillotineStretch(View view, Player player)
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
        DiscardCardFromOpponentHand opponentDiscardCard = new DiscardCardFromOpponentHand(_view, opponent, 1);
        opponentDiscardCard.Execute();
        DrawCard drawCard = new DrawCard(_player, _view, 1);
        drawCard.Execute();
    }
}