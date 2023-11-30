using RawDeal.Cards;
using RawDealView;

namespace RawDeal.Effects;

public class MoveTopCardFromArsenalToRingsidePile: IEffect
{
    private readonly Player _player;
    
    public MoveTopCardFromArsenalToRingsidePile(Player player)
    {
        _player = player;
    }
    public void Execute()
    {
        _player.MoveTopCardFromArsenalToRingSide();
    }
}