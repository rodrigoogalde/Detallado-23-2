namespace RawDeal.Cards.Type.Maneuver;

public interface ICardManeuverStrategy : ICardTypeStrategy
{
    void PerformManeuver(Player opponent);
}