using RawDeal.Effects;
using RawDeal.Options;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Type.Action;

public class SpitAtOpponent: ICardActionStrategy
{
    private readonly View _view;
    private readonly Player _player;
    private Card? _card;
    
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
        _card = card.CardInObjectFormat!;
        PerformAction(opponent);
    }

    public void PerformAction(Player opponent)
    {
        _player.MoveCardFromHandToRingside(_card!);
        HandDiscardEffect handDiscardCard = new HandDiscardEffect(_view, _player, 1);
        handDiscardCard.Execute();
        HandDiscardEffect opponentHandDiscardCard = new HandDiscardEffect(_view, opponent, 4);
        opponentHandDiscardCard.Execute();
    }
}