using RawDeal.Effects;
using RawDeal.SuperStarsCards;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Reversal.General;

public class JockeyingForPosition: ICardReversalStrategy
{
    private readonly View _view;
    
    public JockeyingForPosition(View view)
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
        return cardInObjectFormat.Title == "Jockeying for Position";
    }

    public void PerformEffect(FormatterCardRepresentation card, Game game, Player player, Player playerOnWait)
    {
        PerformReversal(card, game, player, playerOnWait);
    }

    public void PerformReversal(FormatterCardRepresentation card, Game game, Player player, Player playerOnWait)
    {
        Reverse reverse = new Reverse(_view, player, card);
        reverse.Execute();
        // TODO: Check if the card was played from hand
        // if (card.PlayedFromDeck == "Arsenal") return;
        SuperStar superstar = player.SuperStar;
        var selectedEffect = _view.AskUserToSelectAnEffectForJockeyForPosition(superstar.Name!);
        // Add Bonus
        // playerOnWait.MoveTopCardFromRingAreaToRingside();
    }
}