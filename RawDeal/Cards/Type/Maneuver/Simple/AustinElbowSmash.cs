using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Type.Maneuver.Simple;

public class AustinElbowSmash: ICardManeuverStrategy
{
    private readonly Player _player;
    
    public AustinElbowSmash(Player player)
    {
        _player = player;
    }
    
    public bool IsEffectApplicable()
    {
        FormatterCardRepresentation card = _player.GetLastCardPlayed();
        Card cardInObjectForm = card.CardInObjectFormat!;
        return card.Type == "MANEUVER" && cardInObjectForm.NetDamage >= 5;
    }

    public void PerformEffect(FormatterCardRepresentation card, Player opponent)
    {
    }

    public void PerformManeuver(Player opponent)
    {
    }
}