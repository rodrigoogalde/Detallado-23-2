using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Reversal.General;

public class CleanBreak: ICardReversalStrategy
{
    private readonly View _view;
    private readonly Player _player;
    
    public CleanBreak(View view, Player player)
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
        Reverse reverse = new Reverse(_view, _player, card);
        reverse.Execute();
        DiscardCardFromOpponentHand discardCardFromOpponentHand = new DiscardCardFromOpponentHand(_view, opponent, 4);
        discardCardFromOpponentHand.Execute();
        DrawCard drawCard = new DrawCard(_player, _view, 1);
        drawCard.Execute();
    }
}