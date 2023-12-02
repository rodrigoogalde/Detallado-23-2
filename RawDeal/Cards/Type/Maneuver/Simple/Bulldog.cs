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
        HandDiscardEffect handDiscardEffect = new HandDiscardEffect(_view, _player, 1);
        handDiscardEffect.Execute();
        OpponentHandDiscardEffect opponentHandDiscardEffect = new OpponentHandDiscardEffect(_view, _player,
            opponent, 1);
        opponentHandDiscardEffect.Execute();
    }
}