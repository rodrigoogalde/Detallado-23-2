using RawDeal.Effects;
using RawDeal.SuperStarsCards;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Type.Maneuver.Simple;

public class GuillotineStretch: ICardManeuverStrategy
{
    private readonly View _view;
    private readonly Player _player;
    private readonly SuperStar _superStar;
    
    public GuillotineStretch(View view, Player player)
    {
        _view = view;
        _player = player;
        _superStar = _player.SuperStar;
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
        HandDiscardEffect handDiscardEffect = new HandDiscardEffect(_view, opponent, 1);
        handDiscardEffect.Execute();
        int cardsToDiscard = _view.AskHowManyCardsToDrawBecauseOfACardEffect(_superStar.Name!, 1);
        DrawCardEffect drawCardsEffect = new DrawCardEffect(_player, _view, cardsToDiscard);
        drawCardsEffect.Execute();
    }
}