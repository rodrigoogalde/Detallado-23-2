using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Reversal.General;

public class ElbowToTheFace: ICardReversalStrategy
{
    private readonly View _view;
    private readonly Player _player;
    
    public ElbowToTheFace(View view, Player player)
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
        Card cardInObjectFormat = card.CardInObjectFormat!;
        return card.Type == "MANEUVER" &&
               cardInObjectFormat.DamageValue <= 7;
    }

    public void PerformEffect(FormatterCardRepresentation card, Player opponent)
    {
        PerformReversal(card, opponent);
    }

    public void PerformReversal(FormatterCardRepresentation card, Player opponent)
    {
        ReverseAndDamage reverseAndDamage = new ReverseAndDamage(_view, _player, opponent, card);
        reverseAndDamage.Execute();
    }
}