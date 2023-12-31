using RawDeal.Effects;
using RawDeal.Options;
using RawDeal.SuperStarsCards;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Type.Action;

public class Recovery: ICardActionStrategy
{
    private readonly View _view;
    private readonly Player _player;
    private readonly int _numberOfCardsToShuffle;
    private readonly int _numberOfCardsToDraw;
    private Card? _card;
    
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
        _card = card.CardInObjectFormat;
        PerformAction(opponent);
    }

    public void PerformAction(Player opponent)
    {
        _player.MoveCardFromHandToRingArea(_card!);
        DeckShufflingEffect shuffle = 
            new DeckShufflingEffect(_view, _player, _numberOfCardsToShuffle);
        shuffle.Execute();
        
        DrawCardEffect drawCardEffect = new DrawCardEffect(_player, _view, _numberOfCardsToDraw);
        drawCardEffect.Execute();
    }
}