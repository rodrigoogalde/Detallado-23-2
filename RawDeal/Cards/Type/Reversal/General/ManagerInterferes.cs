using RawDeal.Effects;
using RawDeal.Options;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Reversal.General;

public class ManagerInterferes: ICardReversalStrategy
{
    private readonly View _view;
    private readonly Player _player;
    
    public ManagerInterferes(View view, Player player)
    {
        _view = view;
        _player = player;
    }
    
    public bool IsEffectApplicable()
    {
        return IsReversalApplicable(_player);
    }
    
    public bool IsReversalApplicable(Player player)
    {
        FormatterCardRepresentation card = player.GetLastCardPlayedByOpponent();
        return card.Type == "MANEUVER";
    }

    public void PerformEffect(FormatterCardRepresentation cardPlayedByOpponent, Player opponent)
    {
        PerformReversal(cardPlayedByOpponent, opponent);
    }

    public void PerformReversal(FormatterCardRepresentation card, Player opponent)
    {
        Reverse reverse = new Reverse(_view, _player, card);
        reverse.Execute();
        if (_player.LastCardPlayedFromDeck == CardSetFull.Arsenal) return;
        
        DrawCard drawCard = new DrawCard(_player, _view, 1);
        drawCard.Execute();
         
        Damager damager = new Damager(_view, _player, opponent, card);
        damager.Execute();
    }
}