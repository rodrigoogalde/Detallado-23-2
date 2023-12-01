using RawDeal.Cards.Maneuver;
using RawDeal.Effects;
using RawDeal.SuperStarsCards;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Type.Maneuver.Simple;

public class GuillotineStretch: ICardManeuverStrategy
{
    private readonly View _view;
    private readonly Player _player;
    private readonly SuperStar _superstar;
    
    public GuillotineStretch(View view, Player player)
    {
        _view = view;
        _player = player;
        _superstar = _player.SuperStar;
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
        DiscardCardFromHand discardCardFromHand = new DiscardCardFromHand(_view, opponent, 1);
        discardCardFromHand.Execute();
        int cardsToDiscard = _view.AskHowManyCardsToDrawBecauseOfACardEffect(_superstar.Name!, 1);
        DrawCard drawCards = new DrawCard(_player, _view, cardsToDiscard);
        drawCards.Execute();
    }
}