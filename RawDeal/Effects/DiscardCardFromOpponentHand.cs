using RawDeal.Cards;
using RawDeal.Options;
using RawDealView;

namespace RawDeal.Effects;

public class DiscardCardFromOpponentHand: IEffect
{
    private readonly View _view;
    private readonly Player _player;
    private readonly Player _opponent;
    private readonly int _totalCardsToDiscard;
    private readonly SuperCardInfo _superCard;
    private readonly SuperCardInfo _superCardOpponent;
    
    public DiscardCardFromOpponentHand(View view, Player player, Player opponent, int totalCardsToDiscard)
    {
        _view = view;
        _player = player;
        _opponent = opponent;
        _totalCardsToDiscard = totalCardsToDiscard;
        var superStar = player.SuperStar;
        var superStarOpponent = opponent.SuperStar;
        _superCard = superStar.SuperCard;
        _superCardOpponent = superStarOpponent.SuperCard;
    }
    
    public void Execute()
    {
        for (var i = _totalCardsToDiscard; 0 < i; i--)
        {
            if (_opponent.GetHandCards().Count == 0) return;
            var indexCardToDiscard = _view.AskPlayerToSelectACardToDiscard(
                _opponent.TransformMazeToStringFormat(CardSetFull.Hand).ToList(),
                _superCardOpponent.Name, _superCard.Name, i);
            _opponent.DiscardCardFromHandToRingsideWithIndex(indexCardToDiscard);
        }
    }
}