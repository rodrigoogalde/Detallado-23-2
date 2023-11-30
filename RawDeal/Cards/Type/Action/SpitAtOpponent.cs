using RawDeal.Cards.Maneuver;
using RawDeal.Effects;
using RawDeal.Options;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Type.Action;

public class SpitAtOpponent: ICardActionStrategy
{
    private readonly View _view;
    private readonly Player _player;
    
    public SpitAtOpponent(View view, Player player)
    {
        _view = view;
        _player = player;
    }
    public bool IsEffectApplicable()
    {
        return _player.TransformMazeToStringFormat(CardSetFull.Hand).Count >= 2;
    }

    public void PerformEffect(FormatterCardRepresentation card, Player opponent)
    {
        PerformAction(opponent);
    }

    public void PerformAction(Player opponent)
    {
        DiscardCardFromHand discardCard = new DiscardCardFromHand(_view, _player, 1);
        discardCard.Execute();
        DiscardCardFromHand opponentDiscardCard = new DiscardCardFromHand(_view, opponent, 4);
        opponentDiscardCard.Execute();
    }
}