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
        MoveTopCardFromArsenalToRingsidePile moveCard = new MoveTopCardFromArsenalToRingsidePile(_player);
        moveCard.Execute();
        DrawCard drawCard = new DrawCard(opponent, _view, 2);
        drawCard.Execute();
    }
}