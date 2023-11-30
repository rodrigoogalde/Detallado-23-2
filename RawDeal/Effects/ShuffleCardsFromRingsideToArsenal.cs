using RawDeal.Cards;
using RawDeal.Options;
using RawDealView;

namespace RawDeal.Effects;

public class ShuffleCardsFromRingsideToArsenal: IEffect
{
    private readonly View _view;
    private readonly Player _player;
    private readonly int _totalCardsToShuffle;
    private readonly SuperCardInfo _superCard;
    
    public ShuffleCardsFromRingsideToArsenal(View view, Player player, int totalCardsToShuffle)
    {
        _view = view;
        _player = player;
        _totalCardsToShuffle = totalCardsToShuffle;
        var superStar = player.SuperStar;
        _superCard = superStar.SuperCard;
    }
    
    public void Execute()
    {
        for (int i = 0; i < _totalCardsToShuffle; i++)
        {
            _view.SayThatPlayerIsGoingToUseHisAbility(_superCard.Name, _superCard.SuperstarAbility!);
            int indexCardsToRecover = _view.AskPlayerToSelectCardsToRecover(_superCard.Name, _totalCardsToShuffle,
                _player.TransformMazeToStringFormat(CardSetFull.RingsidePile).ToList());
            _player.MoveCardFromRingsideToArsenalWithIndex(indexCardsToRecover);
        }
    }
}