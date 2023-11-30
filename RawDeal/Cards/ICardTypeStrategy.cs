using RawDeal.Utils;

namespace RawDeal.Cards;

public interface ICardTypeStrategy
{
    bool IsEffectApplicable();
    void PerformEffect(FormatterCardRepresentation card, Game game, Player player, Player playerOnWait);
}