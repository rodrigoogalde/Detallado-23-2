using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Reversal.General;

public class KneeToTheGut: ICardReversalStrategy
{
    private readonly View _view;
    
    public KneeToTheGut(View view)
    {
        _view = view;
    }
    
    public bool IsEffectApplicable(Game game, Player player, Player playerOnWait)
    {
        return IsReversalApplicable(game, player);
    }
    
    public bool IsReversalApplicable(Game game, Player player)
    {
        FormatterCardRepresentation card = player.GetLastCardPlayedByOpponent();
        Card cardInObjectFormat = card.CardInObjectFormat!;
        return card.Type == "MANEUVER" &&
               cardInObjectFormat.Subtypes!.Contains("Strike") &&
               int.Parse(cardInObjectFormat.Damage!) <= 7;
    }

    public void PerformEffect(FormatterCardRepresentation card, Game game, Player player, Player playerOnWait)
    {
        PerformReversal(card, game, player, playerOnWait);
    }

    public void PerformReversal(FormatterCardRepresentation card, Game game, Player player, Player playerOnWait)
    {
        // TODO: Revisar su implementación
        Reverse reverse = new Reverse(_view, player, card);
        reverse.Execute();
    }
}