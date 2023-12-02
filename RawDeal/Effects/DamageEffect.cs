using RawDeal.Cards;
using RawDeal.SuperStarsCards;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Effects;

public class DamageEffect: IEffect
{
    private readonly View _view;
    private readonly Player _player;
    private readonly Player _opponent;
    private readonly Card _reversalCard;
    private readonly Card _damagerCard;
    private const int MaindKindDamageReduction = 1;
    private readonly SuperStar _superStarDamager;
    private readonly SuperStar _superStarReverser;
    private int _damage;
    
    public DamageEffect(View view, Player player, Player opponent, FormatterCardRepresentation card)
    {
        _view = view;
        _player = player;
        _reversalCard = card.CardInObjectFormat!;
        _opponent = opponent;
        _superStarReverser = _player.SuperStar;
        _superStarDamager = _opponent.SuperStar;
        
        FormatterCardRepresentation opponentCardFormatter = player.GetLastCardPlayedByOpponent();
        _damagerCard = opponentCardFormatter.CardInObjectFormat!;
    }
    
    public void Execute()
    {
        bool isDamageWildcard = _reversalCard.Damage == "#";
        bool shouldApplyMankindReduction = _superStarDamager.Name!.ToUpper() == "MANKIND" || 
                                           (_superStarReverser.Name!.ToUpper() == "MANKIND" && isDamageWildcard);

        int effectiveDamageValue = isDamageWildcard ? _damagerCard.DamageValue : _reversalCard.DamageValue;
        _damage = effectiveDamageValue - (shouldApplyMankindReduction ? MaindKindDamageReduction : 0);
        _player.GetOptionChoosedForJockeyingForPosition();
        if (_damage <= 0) return;
        _view.SayThatSuperstarWillTakeSomeDamage(_superStarDamager.Name!, _damage);
        _opponent.TakeDamage(_damage);
    }
}
