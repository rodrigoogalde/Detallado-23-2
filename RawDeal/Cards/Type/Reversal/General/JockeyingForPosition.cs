using RawDeal.Cards.Reversal;
using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Type.Reversal.General;

public class JockeyingForPosition: ICardReversalStrategy
{
    private readonly View _view;
    private readonly Player _player;
    public JockeyingForPosition(View view, Player player)
    {
        _view = view;
        _player = player;
    }
    
    public bool IsEffectApplicable()
    {
        return IsReversalApplicable(_player);
    }
    
    public bool IsReversalApplicable(Player player)
    {
        FormatterCardRepresentation card = player.GetLastCardPlayedByOpponent();
        if (card.Type == null) return false;
        Card cardInObjectFormat = card.CardInObjectFormat!;
        return cardInObjectFormat.Title == "Jockeying for Position";
    }

    public void PerformEffect(FormatterCardRepresentation card, Player opponent)
    {
        PerformReversal(card, opponent);
    }

    public void PerformReversal(FormatterCardRepresentation card, Player opponent)
    {
        ReversalEffect reversalEffect = new ReversalEffect(_view, _player, card);
        reversalEffect.Execute();
        _player.CheckIfJockeyForPositionIsPlayed(card.CardInObjectFormat!);
    }
}