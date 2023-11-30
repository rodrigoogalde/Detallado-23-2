using RawDeal.Effects;
using RawDeal.Options;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Reversal.General;

public class ManagerInterferes: ICardReversalStrategy
{
    private readonly View _view;
    private readonly Player _player;
    private readonly Game _game;
    
    public ManagerInterferes(View view, Player player, Game game)
    {
        _view = view;
        _player = player;
        _game = game;
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

    public void PerformEffect(FormatterCardRepresentation cardPlayedByOpponent, Game game, Player player, Player playerOnWait)
    {
        PerformReversal(cardPlayedByOpponent, player);
    }

    public void PerformReversal(FormatterCardRepresentation card, Player player)
    {
        Reverse reverse = new Reverse(_view, _player, card);
        reverse.Execute();
        switch (_player.LastCardPlayedFromDeck)
        {
            case CardSetFull.Arsenal:
                return;
            case CardSetFull.Hand:
            {
                DrawCard drawCard = new DrawCard(_player, _view, 1);
                drawCard.Execute();
                break;
            }
        }
        Damager damager = new Damager(_view, _player, player, card);
        damager.Execute();
    }
}