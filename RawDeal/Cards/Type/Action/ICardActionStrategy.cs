namespace RawDeal.Cards.Type.Action;

public interface ICardActionStrategy : ICardTypeStrategy
{
    void PerformAction(Player opponent);
}