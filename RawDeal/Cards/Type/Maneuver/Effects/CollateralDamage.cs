using RawDeal.Cards.Maneuver;
using RawDeal.SuperStarsCards;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Type.Maneuver.Effects;

public class CollateralDamage: ICardManeuverStrategy
{
    private readonly View _view;
    private readonly Player _player;
    private readonly SuperStar _superStar;
    private FormatterCardRepresentation _card;
    
    public CollateralDamage(View view, Player player)
    {
        _view = view;
        _player = player;
        _superStar = _player.SuperStar;
        _card = new FormatterCardRepresentation();
    }
    public bool IsEffectApplicable()
    {
        return true;
    }

    public void PerformEffect(FormatterCardRepresentation card, Player opponent)
    {
        _card = card;
        PerformManeuver(opponent);
    }

    public void PerformManeuver(Player opponent)
    {
        _view.SayThatPlayerDamagedHimself(_superStar.Name!,1);
        _view.SayThatSuperstarWillTakeSomeDamage(_superStar.Name!, 1);
        _player.TakeDamage(1);
    }
}