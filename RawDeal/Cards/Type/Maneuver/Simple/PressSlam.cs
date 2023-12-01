using RawDeal.Cards.Maneuver;
using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Type.Maneuver.Simple;

public class PressSlam: ICardManeuverStrategy
{
    private readonly View _view;
    private readonly Player _player;
    
    public PressSlam(View view, Player player)
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
        RecieveCollateralDamage recieveCollateralDamage = new RecieveCollateralDamage(_view, _player);
        recieveCollateralDamage.Execute();
        DiscardCardFromHand discardCard = new DiscardCardFromHand(_view, opponent, 2);
        discardCard.Execute();
    }
}