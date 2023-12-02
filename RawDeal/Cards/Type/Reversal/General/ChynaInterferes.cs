using RawDeal.Cards.Reversal;
using RawDeal.Effects;
using RawDeal.Options;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Type.Reversal.General;

public class ChynaInterferes: ICardReversalStrategy
{
    private readonly View _view;
    private readonly Player _player;
    
    public ChynaInterferes(View view, Player player)
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
        return card.Type == "MANEUVER";
    }

    public void PerformEffect(FormatterCardRepresentation card, Player opponent)
    {
        PerformReversal(card, opponent);
    }

    public void PerformReversal(FormatterCardRepresentation card, Player opponent)
    {
        ReversalEffect reversalEffect = new ReversalEffect(_view, _player, card);
        reversalEffect.Execute();
        if (_player.GetLastCardPlayedFromDeck() == CardSetFull.Arsenal) return;
        DrawCardEffect drawCardEffect = new DrawCardEffect(_player, _view, 2);
        drawCardEffect.Execute();
        DamageEffect damageEffect = new DamageEffect(_view, _player, opponent, card);
        damageEffect.Execute();
    }
}