using RawDeal.Cards;
using RawDeal.Options;
using RawDealView;

namespace RawDeal.Effects;

public class DeckShufflingEffect: IEffect
{
    private readonly View _view;
    private readonly Player _player;
    private readonly int _totalCardsToShuffle;
    private readonly SuperCardInfo _superCard;
    
    public DeckShufflingEffect(View view, Player player, int totalCardsToShuffle)
    {
        _view = view;
        _player = player;
        _totalCardsToShuffle = totalCardsToShuffle;
        var superStar = player.SuperStar;
        _superCard = superStar.SuperCard;
    }
    
    public void Execute()
    {
        for (int i = _totalCardsToShuffle; i > 0; i--)
        {
            int indexCardsToRecover = _view.AskPlayerToSelectCardsToRecover(_superCard.Name, i,
                _player.TransformMazeToStringFormat(CardSetFull.RingsidePile).ToList());
            _player.MoveCardFromRingsideToArsenalWithIndex(indexCardsToRecover);
        }
    }
}