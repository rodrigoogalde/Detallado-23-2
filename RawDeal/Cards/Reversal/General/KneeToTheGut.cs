using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Reversal.General;

public class KneeToTheGut: ICardReversalStrategy
{
    private readonly View _view;
    private readonly Player _player;
    private readonly Game _game;
    public KneeToTheGut(View view, Player player, Game game)
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
        Card cardInObjectFormat = card.CardInObjectFormat!;
        return card.Type == "MANEUVER" &&
               cardInObjectFormat.Subtypes!.Contains("Strike") &&
               int.Parse(cardInObjectFormat.Damage!) <= 7;
    }

    public void PerformEffect(FormatterCardRepresentation card, Game game, Player player, Player playerOnWait)
    {
        PerformReversal(card, player);
    }

    public void PerformReversal(FormatterCardRepresentation card, Player player)
    {
        // TODO: Revisar su implementación
        ReverseAndDamage reverseAndDamage = new ReverseAndDamage(_view, _player, player, card);
        reverseAndDamage.Execute();
    }
}