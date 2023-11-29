using RawDeal.Effects;
using RawDeal.SuperStarsCards;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Reversal.WithoutEffects;

public class StepAside: ICardReversalStrategy
{
    private readonly View _view;
    private readonly Player _player;
    private readonly Game _game;
    
    public StepAside(View view, Player player, Game game)
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
        return card.Type == "MANEUVER" && cardInObjectFormat.Subtypes!.Contains("Strike");
    }

    public void PerformEffect(FormatterCardRepresentation card, Game game, Player player, Player playerOnWait)
    {
        PerformReversal(card, player);
    }

    public void PerformReversal(FormatterCardRepresentation card, Player player)
    {
        Reverse reverse = new Reverse(_view, _player, card);
        reverse.Execute();
    }
}