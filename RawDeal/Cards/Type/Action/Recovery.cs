using RawDeal.Effects;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Type.Action;

public class Recovery: ICardActionStrategy
{
    private readonly View _view;
    private readonly Player _player;
    private readonly int _numberOfCardsToShuffle;
    private readonly int _numberOfCardsToDraw;
    
    public Recovery(View view, Player player, int numberOfCardsToShuffle, int numberOfCardsToDraw)
    {
        _view = view;
        _player = player;
        _numberOfCardsToShuffle = numberOfCardsToShuffle;
        _numberOfCardsToDraw = numberOfCardsToDraw;
    }
    public bool IsEffectApplicable()
    {
        return true;
    }

    public void PerformEffect(FormatterCardRepresentation card, Player opponent)
    {
        PerformAction(opponent);
    }

    public void PerformAction(Player opponent)
    {
        ShuffleCardsFromRingsideToArsenal shuffle = 
            new ShuffleCardsFromRingsideToArsenal(_view, _player, _numberOfCardsToShuffle);
        shuffle.Execute();
        DrawCard drawCard = new DrawCard(_player, _view, _numberOfCardsToDraw);
        drawCard.Execute();
    }
}