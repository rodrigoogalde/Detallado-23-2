using RawDeal.Cards;
using RawDeal.Options;
using RawDeal.SuperStarsCards;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Effects;

public class ReversalEffect: IEffect
{
    private readonly View _view;
    private readonly Player _player;
    private readonly FormatterCardRepresentation _card;
    
    public ReversalEffect(View view, Player player, FormatterCardRepresentation card)
    {
        _view = view;
        _player = player;
        _card = card;
    }
    
    public void Execute()
    {
        if (_player.GetLastCardPlayedFromDeck() == CardSetFull.Arsenal) return;
        SuperStar superStar = _player.SuperStar;
        Card cardInObjectFormat = _card.CardInObjectFormat!;
        _player.MoveCardFromHandToRingArea(cardInObjectFormat);
        _view.SayThatPlayerReversedTheCard(superStar.Name!, _card.CardInStringFormat!);
    }
}