using RawDeal.Utils;

namespace RawDeal.Cards.Reversal;

public interface ICardReversalStrategy: ICardTypeStrategy
{
    void PerformReversal(FormatterCardRepresentation card, Player player);
    bool IsReversalApplicable(Player player);
}