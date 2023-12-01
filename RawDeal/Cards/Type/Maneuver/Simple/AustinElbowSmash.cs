using RawDeal.Cards.Maneuver;
using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Type.Maneuver.Simple;

public class AustinElbowSmash: ICardManeuverStrategy
{
    private readonly View _view;
    private readonly Player _player;
    
    public AustinElbowSmash(View view, Player player)
    {
        _view = view;
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
        PerformManeuver(opponent);
    }

    public void PerformManeuver(Player opponent)
    {
    }
}