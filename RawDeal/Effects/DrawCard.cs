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
        int cardsDrawn = 0;
        for (int i = 0; i < _totalCardsToDraw; i++)
        {
            if (_player.DrawCard())
            {
                cardsDrawn += 1;
            }
        }
        _view.SayThatPlayerDrawCards(superStar.Name!, cardsDrawn);
    }
}