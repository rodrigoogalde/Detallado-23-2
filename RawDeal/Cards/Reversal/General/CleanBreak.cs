using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Reversal.General;

public class CleanBreak: ICardReversalStrategy
{
    private readonly View _view;
    
    public CleanBreak(View view)
    {
        _view = view;
    }
    
    public bool IsEffectApplicable(Game game, Player player, Player playerOnWait)
    {
        return IsReversalApplicable(game, player);
    }
    
    public bool IsReversalApplicable(Game game, Player player)
    {
        FormatterCardRepresentation card = player.GetLastCardPlayedByOpponent();
        Card cardInObjectFormat = card.CardInObjectFormat!;
        // TODO: Check if the card is played from Hand
        return cardInObjectFormat.Title == "Jockeying for Position";
    }

    public void PerformEffect(FormatterCardRepresentation card, Game game, Player player, Player playerOnWait)
    {
        PerformReversal(card, game, player, playerOnWait);
    }

    public void PerformReversal(FormatterCardRepresentation card, Game game, Player player, Player playerOnWait)
    {
        // TODO: Reverse, Opponent Discard 4 cards, Draw 1 card
        Reverse reverse = new Reverse(_view, player, card);
        reverse.Execute();
    }
}