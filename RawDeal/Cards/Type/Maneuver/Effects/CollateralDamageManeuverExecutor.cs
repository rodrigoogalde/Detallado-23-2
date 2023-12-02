using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Type.Maneuver.Effects;

public class CollateralDamageManeuverExecutor: ICardManeuverStrategy
{
    private readonly View _view;
    private readonly Player _player;
    
    public CollateralDamageManeuverExecutor(View view, Player player)
    {
        _view = view;
        _player = player;
    }
    public bool IsEffectApplicable()
    {
        return true;
    }

    public void PerformEffect(FormatterCardRepresentation card, Player opponent)
    {
        PerformManeuver(opponent);
    }

    public void PerformManeuver(Player opponent)
    {
        CollateralDamageEffect collateralDamageEffect = new CollateralDamageEffect(_view, _player);
        collateralDamageEffect.Execute();
    }
}