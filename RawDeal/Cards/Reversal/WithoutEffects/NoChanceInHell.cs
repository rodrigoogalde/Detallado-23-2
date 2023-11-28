using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Reversal.WithoutEffects;

public class NoChanceInHell: ICardReversalStrategy
{
    private readonly View _view;
    private readonly Player _player;
    private readonly Game _game;
    
    public NoChanceInHell(View view, Player player, Game game)
    {
        _view = view;
        _player = player;
        _game = game;
    }
    
    public bool IsEffectApplicable()
    {
        return IsReversalApplicable(_game, _player);
    }
    
    public bool IsReversalApplicable(Game game, Player player)
    {
        FormatterCardRepresentation card = player.GetLastCardPlayedByOpponent();
        return card.Type == "ACTION";
    }

    public void PerformEffect(FormatterCardRepresentation card, Game game, Player player, Player playerOnWait)
    {
        PerformReversal(card, game, player, playerOnWait);
    }

    public void PerformReversal(FormatterCardRepresentation card, Game game, Player player, Player playerOnWait)
    {
        Reverse reverse = new Reverse(_view, player, card);
        reverse.Execute();
    }


}