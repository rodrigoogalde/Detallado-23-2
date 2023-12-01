using RawDeal.Exceptions;
using RawDeal.SuperStarsCards;
using RawDealView;

namespace RawDeal.Effects;

public class RecieveCollateralDamage: IEffect
{
    private View _view;
    private Player _player;
    private SuperStar _superStar;
    
    public RecieveCollateralDamage(View view, Player player)
    {
        _view = view;
        _player = player;
        _superStar = _player.SuperStar;
    }
    
    public void Execute()
    {
        _view.SayThatPlayerDamagedHimself(_superStar.Name!,1);
        _view.SayThatSuperstarWillTakeSomeDamage(_superStar.Name!, 1);
        if (!_player.TakeDamage(1)) return;
        _view.SayThatPlayerLostDueToSelfDamage(_superStar.Name!);
        throw new PlayerLosesDueToSelfDamage();
    }
}