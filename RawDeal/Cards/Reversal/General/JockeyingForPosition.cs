using RawDeal.Effects;
using RawDeal.Exceptions;
using RawDeal.SuperStarsCards;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Reversal.General;

public class JockeyingForPosition: ICardReversalStrategy
{
    private readonly View _view;
    private readonly Player _player;
    private readonly Game _game;
    public JockeyingForPosition(View view, Player player, Game game)
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
        if (card.Type == null) return false;
        return cardInObjectFormat.Title == "Jockeying for Position";
    }

    public void PerformEffect(FormatterCardRepresentation card, Game game, Player player, Player playerOnWait)
    {
        PerformReversal(card, player);
    }

    public void PerformReversal(FormatterCardRepresentation card, Player player)
    {
        Reverse reverse = new Reverse(_view, _player, card);
        reverse.Execute();
        // TODO: Check if the card was played from hand
        _player.CheckIfJockeyForPositionIsPlayed(card.CardInObjectFormat!);
    }
}