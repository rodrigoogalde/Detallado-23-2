using RawDeal.Cards;
using RawDeal.Options;
using RawDeal.SuperStarsCards;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Effects;

public class Damager: IEffect
{
    private View _view;
    private Player _player;
    private Player _opponent;
    private readonly Card _reversalCard;
    private readonly Card _damagerCard;
    private const int MaindKindDamageReduction = 1;
    private readonly SuperStar _superStarDamager;
    private readonly SuperStar _superStarReverser;
    private int _damage;
    
    public Damager(View view, Player player, Player opponent, FormatterCardRepresentation card)
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
        bool shouldApplyMankindReduction = _superStarDamager.IsManKind() || 
                                           (_superStarReverser.IsManKind() && isDamageWildcard);

        string? effectiveDamageValue = isDamageWildcard ? _damagerCard.Damage! : _reversalCard.Damage;
        _damage = Convert.ToInt32(effectiveDamageValue) - (shouldApplyMankindReduction ? MaindKindDamageReduction : 0);

        // if (_superStarDamager.IsManKind())
        // {
        //     if (_reversalCard.Damage == "#")
        //     {
        //         _damage = Convert.ToInt32(_damagerCard.Damage!) - MaindKindDamageReduction;
        //     }
        //     else
        //     {
        //         _damage = Convert.ToInt32(_reversalCard.Damage) - MaindKindDamageReduction;
        //     }
        // } else if (_superStarReverser.IsManKind() && _reversalCard.Damage == "#")
        // {
        //     _damage = Convert.ToInt32(_damagerCard.Damage!) - MaindKindDamageReduction;
        // }
        // else
        // {
        //     _damage = Convert.ToInt32(_reversalCard.Damage == "#" ? _damagerCard.Damage! : _reversalCard.Damage);
        // }
        if (_damage <= 0) return;
        _view.SayThatSuperstarWillTakeSomeDamage(_superStarDamager.Name!, _damage);
        _opponent.TakeDamage(_damage);
    }
}
