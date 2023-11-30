using RawDeal.Cards;
using RawDeal.Options;
using RawDeal.SuperStarsCards;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Effects;

public class ReverseAndDamage: IEffect
{
    private View _view;
    private Player _player;
    private Player _opponent;
    private FormatterCardRepresentation _card;
    
    public ReverseAndDamage(View view, Player player, Player opponent, FormatterCardRepresentation card)
    {
        _view = view;
        _player = player;
        _card = card;
        _opponent = opponent;
    }
    
    public void Execute()
    {
        SuperStar superStar = _player.SuperStar;
        Card cardInObjectFormat = _card.CardInObjectFormat!;
        if (_player.LastCardPlayedFromDeck == CardSetFull.Arsenal) return; 
        
        _player.MoveCardFromHandToRingArea(cardInObjectFormat);
        _view.SayThatPlayerReversedTheCard(superStar.Name!, _card.CardInStringFormat!);
        Damager damager = new Damager(_view, _player, _opponent, _card);
        damager.Execute();
        
    }
}