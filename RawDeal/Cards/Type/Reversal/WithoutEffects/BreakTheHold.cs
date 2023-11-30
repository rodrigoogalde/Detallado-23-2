using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Reversal.WithoutEffects;

public class BreakTheHold: ICardReversalStrategy
{
    private readonly View _view;
    private readonly Player _player;
    
    public BreakTheHold(View view, Player player)
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
        Card cardInObjectFormat = card.CardInObjectFormat!;
        return card.Type == "MANEUVER" && cardInObjectFormat.Subtypes!.Contains("Submission");
    }
    
    public void PerformEffect(FormatterCardRepresentation card, Player opponent)
    {
        PerformReversal(card, opponent);
    }
    
    public void PerformReversal(FormatterCardRepresentation card, Player opponent)
    {
        Reverse reverse = new Reverse(_view, _player, card);
        reverse.Execute();
    }
}