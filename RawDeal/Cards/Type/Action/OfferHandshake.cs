using RawDeal.Cards.Maneuver;
using RawDeal.Effects;
using RawDeal.SuperStarsCards;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Type.Action;

public class OfferHandshake: ICardManeuverStrategy
{
    private readonly View _view;
    private readonly Player _player;
    private readonly SuperStar _superStar;
    private Card? _card;
    
    public OfferHandshake(View view, Player player)
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
        _card = card.CardInObjectFormat;
        PerformManeuver(opponent);
    }

    public void PerformManeuver(Player opponent)
    {
        _player.MoveCardFromHandToRingArea(_card!);
        int cardsToDiscard = _view.AskHowManyCardsToDrawBecauseOfACardEffect(_superStar.Name!, 3);
        DrawCard drawCard = new DrawCard(_player, _view, cardsToDiscard);
        drawCard.Execute();
        DiscardCardFromHand discardCardFromHand = new DiscardCardFromHand(_view, _player, 1);
        discardCardFromHand.Execute();
    }
}