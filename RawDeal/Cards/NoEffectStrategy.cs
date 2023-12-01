using RawDeal.Utils;

namespace RawDeal.Cards;

public class NoEffectStrategy: ICardTypeStrategy
{
    public bool IsEffectApplicable()
    {   
        return false;
    }

    public void PerformEffect(FormatterCardRepresentation card, Player opponent)
    {
    }
}