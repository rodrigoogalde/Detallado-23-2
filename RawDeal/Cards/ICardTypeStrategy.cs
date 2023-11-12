namespace RawDeal.Cards;

public interface ICardTypeStrategy
{
    bool IsEffectApplicable(Game gameStatus, Player player, Player opponent);
    void PerformEffect(Card playableCard, Game gameStatus, Player player, Player opponent);
}