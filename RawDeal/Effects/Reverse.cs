using RawDeal.Cards;
using RawDeal.Options;
using RawDeal.SuperStarsCards;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Effects;

public class Reverse: IEffect
{
    private View _view;
    private Player _player;
    private FormatterCardRepresentation _card;
    
    public Reverse(View view, Player player, FormatterCardRepresentation card)
    {
        _view = view;
        _player = player;
        _card = card;
    }
    
    public void Execute()
    {
        SuperStar superStar = _player.SuperStar;
        Card cardInObjectFormat = _card.CardInObjectFormat!;
        if (_player.LastCardPlayedFromDeck == CardSetFull.Arsenal) return;
        _player.MoveCardFromHandToRingArea(cardInObjectFormat);
        _view.SayThatPlayerReversedTheCard(superStar.Name!, _card.CardInStringFormat!);
    }
}