using RawDeal.Utils;

namespace RawDeal.Cards.Maneuver;

public interface ICardManeuverStrategy : ICardTypeStrategy
{
    void TryPerformManeuver(FormatterCardRepresentation card, Game game, Player player, Player playerOnWait);
    void PerformManeuver(FormatterCardRepresentation card, Game game, Player player, Player playerOnWait);
}