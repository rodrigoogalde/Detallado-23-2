using RawDeal.Cards.Maneuver;
using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Type.Maneuver.Effects;

public class OpponentsDiscardsCardsFromHand: ICardManeuverStrategy
{
    private Player _player;
    private View _view;
    private int _cardsToDiscard;
    
    public OpponentsDiscardsCardsFromHand(View view, Player player, int cardsToDiscard)
    {
        _view = view;
        _player = player;
        _cardsToDiscard = cardsToDiscard;
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
        for (int i = _cardsToDiscard; i > 0; i--)
        {
            if (opponent.GetHandCards().Count == 0) return;
            DiscardCardFromHand discardCardFromHand = new DiscardCardFromHand(_view, opponent, i);
            discardCardFromHand.Execute();    
        }
        
    }
}