using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Maneuver;

public class OpponentDrawCards : ICardManeuverStrategy
{
    private readonly int _cardsToDiscard;
    private readonly View _view;
    private Player _player;
 
    public OpponentDrawCards(View view, Player player, int cardsToDiscard)
    {
        _cardsToDiscard = cardsToDiscard;
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
        DrawCard drawCard = new DrawCard(opponent, _view, _cardsToDiscard);
        drawCard.Execute();
    }
}