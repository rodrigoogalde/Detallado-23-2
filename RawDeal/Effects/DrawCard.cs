using RawDeal.SuperStarsCards;
using RawDealView;

namespace RawDeal.Effects;

public class DrawCard: IEffect
{
    private readonly Player _player;
    private readonly View _view;
    private readonly int _totalCardsToDraw;
    
    public DrawCard(Player player, View view, int totalCardsToDraw)
    {
        _player = player;
        _view = view;
        _totalCardsToDraw = totalCardsToDraw;
    }
    public void Execute()
    {
        SuperStar superStar = _player.SuperStar;
        for (int i = 0; i < _totalCardsToDraw; i++) _player.DrawCard();
        _view.SayThatPlayerDrawCards(superStar.Name!, _totalCardsToDraw);
    }
}