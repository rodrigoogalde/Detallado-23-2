using RawDeal.Options;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Effects;

public class DamageReversalEffect: IEffect
{
    private readonly View _view;
    private readonly Player _player;
    private readonly Player _opponent;
    private readonly FormatterCardRepresentation _card;
    
    public DamageReversalEffect(View view, Player player, Player opponent, FormatterCardRepresentation card)
    {
        _view = view;
        _player = player;
        _card = card;
        _opponent = opponent;
    }
    
    public void Execute()
    {
        if (_player.GetLastCardPlayedFromDeck() == CardSetFull.Arsenal) return; 
        ReversalEffect reversalEffect = new ReversalEffect(_view, _player, _card);
        reversalEffect.Execute();
        DamageEffect damageEffect = new DamageEffect(_view, _player, _opponent, _card);
        damageEffect.Execute();
        
    }
}