using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Reversal.General;

public class CleanBreak: ICardReversalStrategy
{
    private readonly View _view;
    private readonly Player _player;
    private readonly Game _game;
    
    public CleanBreak(View view, Player player, Game game)
    {
        _view = view;
        _player = player;
        _game = game;
    }
    
    public bool IsEffectApplicable()
    {
        return IsReversalApplicable(_player);
    }
    
    public bool IsReversalApplicable(Player player)
    {
        FormatterCardRepresentation card = player.GetLastCardPlayedByOpponent();
        Card cardInObjectFormat = card.CardInObjectFormat!;
        // TODO: Check if the card is played from Hand
        if (card.Type == null) return false;
        return cardInObjectFormat.Title == "Jockeying for Position";
    }

    public void PerformEffect(FormatterCardRepresentation card, Game game, Player player, Player playerOnWait)
    {
        PerformReversal(card, player);
    }

    public void PerformReversal(FormatterCardRepresentation card, Player player)
    {
        Reverse reverse = new Reverse(_view, _player, card);
        reverse.Execute();
        DiscardCardFromOpponentHand discardCardFromOpponentHand = new DiscardCardFromOpponentHand(_view, player, 4);
        discardCardFromOpponentHand.Execute();
        DrawCard drawCard = new DrawCard(_player, _view, 1);
        drawCard.Execute();
    }
}