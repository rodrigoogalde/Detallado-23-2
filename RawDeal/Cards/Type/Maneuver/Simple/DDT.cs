using RawDeal.Cards.Type.Maneuver.Effects;
using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Maneuver.Simple;

public class Ddt: ICardManeuverStrategy
{
    private readonly View _view;
    private readonly Player _player;
    
    public Ddt(View view, Player player)
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
        DiscardCardFromHand discardCardFromHand = new DiscardCardFromHand(_view, opponent, 2);
        discardCardFromHand.Execute();    

    }
}