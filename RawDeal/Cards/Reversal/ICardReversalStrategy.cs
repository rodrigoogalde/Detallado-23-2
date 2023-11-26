using RawDeal.Utils;

namespace RawDeal.Cards.Reversal;

public interface ICardReversalStrategy: ICardTypeStrategy
{
    void PerformReversal(FormatterCardRepresentation card, Game game, Player player, Player playerOnWait);
    bool IsReversalApplicable(Game game, Player player);
}