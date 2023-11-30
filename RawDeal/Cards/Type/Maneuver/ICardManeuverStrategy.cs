using RawDeal.Utils;

namespace RawDeal.Cards.Maneuver;

public interface ICardManeuverStrategy : ICardTypeStrategy
{
    void PerformManeuver(Player opponent);
}