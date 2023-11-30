using RawDeal.Utils;

namespace RawDeal.Cards;

public interface ICardTypeStrategy
{
    bool IsEffectApplicable();
    void PerformEffect(FormatterCardRepresentation card, Player opponent);
}