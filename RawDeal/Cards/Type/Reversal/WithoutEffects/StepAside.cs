using RawDeal.Cards.Reversal;
using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Type.Reversal.WithoutEffects;

public class StepAside: ICardReversalStrategy
{
    private readonly View _view;
    private readonly Player _player;
    
    public StepAside(View view, Player player)
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
        return card.Type == "MANEUVER" && cardInObjectFormat.Subtypes!.Contains("Strike");
    }

    public void PerformEffect(FormatterCardRepresentation card, Player opponent)
    {
        PerformReversal(card, opponent);
    }

    public void PerformReversal(FormatterCardRepresentation card, Player player)
    {
        ReversalEffect reversalEffect = new ReversalEffect(_view, _player, card);
        reversalEffect.Execute();
    }
}