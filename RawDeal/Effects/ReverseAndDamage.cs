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
    private const int MaindKindDamageReduction = 1;
    
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
        SuperStar superStarOpponent = _opponent.SuperStar;
        Card cardInObjectFormat = _card.CardInObjectFormat!;
        if (_player.LastCardPlayedFromDeck == CardSetFull.Arsenal) return; 
        FormatterCardRepresentation opponentCardFormatter = _player.GetLastCardPlayedByOpponent();
        Card opponentCard = opponentCardFormatter.CardInObjectFormat!;
        int damage = cardInObjectFormat.Damage == "#" ? 
            Convert.ToInt32(opponentCard.Damage!) : Convert.ToInt32(cardInObjectFormat.Damage);
        damage = superStarOpponent.IsManKind() ? damage - MaindKindDamageReduction : damage;
        _player.MoveCardFromHandToRingArea(cardInObjectFormat);
        _view.SayThatPlayerReversedTheCard(superStar.Name!, _card.CardInStringFormat!);
        _view.SayThatSuperstarWillTakeSomeDamage(superStarOpponent.Name!, damage);
        _opponent.TakeDamage(damage);
    }
}