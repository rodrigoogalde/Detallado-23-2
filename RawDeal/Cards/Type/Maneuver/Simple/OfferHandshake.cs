using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Maneuver.Simple;

public class OfferHandshake: ICardManeuverStrategy
{
    private readonly View _view;
    private readonly Player _player;
    
    public OfferHandshake(View view, Player player)
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
        DrawCard drawCard = new DrawCard(opponent, _view, 3);
        drawCard.Execute();
        DiscardCardFromHand discardCardFromHand = new DiscardCardFromHand(_view, _player, 1);
        discardCardFromHand.Execute();
    }
}