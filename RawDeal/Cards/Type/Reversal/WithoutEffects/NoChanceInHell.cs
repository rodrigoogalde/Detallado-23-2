using RawDeal.Cards.Reversal;
using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Type.Reversal.WithoutEffects;

public class NoChanceInHell: ICardReversalStrategy
{
    private readonly View _view;
    private readonly Player _player;
    
    public NoChanceInHell(View view, Player player)
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
        return card.Type == "ACTION";
    }

    public void PerformEffect(FormatterCardRepresentation card, Player player)
    {
        PerformReversal(card, player);
    }

    public void PerformReversal(FormatterCardRepresentation card, Player player)
    {
        ReversalEffect reversalEffect = new ReversalEffect(_view, _player, card);
        reversalEffect.Execute();
    }
}