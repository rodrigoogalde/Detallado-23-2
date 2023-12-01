using RawDeal.Cards;
using RawDeal.Options;
using RawDealView;

namespace RawDeal.Effects;

public class DiscardCardFromHand: IEffect
{
    private readonly View _view;
    private readonly Player _player;
    private readonly int _totalCardsToDiscard;
    private readonly SuperCardInfo _superCard;

    public DiscardCardFromHand(View view, Player player, int totalCardsToDiscard)
    {
        _view = view;
        _player = player;
        _totalCardsToDiscard = totalCardsToDiscard;
        var superStar = player.SuperStar;
        _superCard = superStar.SuperCard;
    }
    
    public void Execute()
    {
        for (var i = _totalCardsToDiscard; 0 < i; i--)
        {
            if (_player.GetHandCards().Count == 0) return;
            var indexCardToDiscard = _view.AskPlayerToSelectACardToDiscard(
                _player.TransformMazeToStringFormat(CardSetFull.Hand).ToList(),
                _superCard.Name, _superCard.Name, i);
            _player.DiscardCardFromHandToRingsideWithIndex(indexCardToDiscard);
        }
    }
}