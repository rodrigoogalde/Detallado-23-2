using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Reversal.WithoutEffects;

public class BreakTheHold: ICardReversalStrategy
{
    private readonly View _view;
    
    public BreakTheHold(View view)
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
        return card.Type == "MANEUVER" && cardInObjectFormat.Subtypes!.Contains("Submission");
    }
    
    public void PerformEffect(FormatterCardRepresentation card, Game game, Player player, Player playerOnWait)
    {
        PerformReversal(card, game, player, playerOnWait);
    }
    
    public void PerformReversal(FormatterCardRepresentation card, Game game, Player player, Player playerOnWait)
    {
        Reverse reverse = new Reverse(_view, player, card);
        reverse.Execute();
    }
}