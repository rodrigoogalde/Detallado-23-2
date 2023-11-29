using RawDeal.Cards;
using RawDeal.Options;
using RawDeal.SuperStarsCards;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Effects;

public class Damager: IEffect
{
    private View _view;
    private Player _player;
    private Player _opponent;
    private FormatterCardRepresentation _card;
    
    public Damager(View view, Player player, Player opponent, FormatterCardRepresentation card)
    {
        _view = view;
        _player = player;
        _card = card;
        _opponent = opponent;
    }
    
    public void Execute()
    {
        SuperStar superStarOpponent = _opponent.SuperStar;
        FormatterCardRepresentation opponentCardFormatter = _player.GetLastCardPlayedByOpponent();
        Card cardInObjectFormat = _card.CardInObjectFormat!;
        Card opponentCard = opponentCardFormatter.CardInObjectFormat!;

        int damage = cardInObjectFormat.Damage == "#" ? 
            Convert.ToInt32(opponentCard.Damage!) : Convert.ToInt32(cardInObjectFormat.Damage);
        _view.SayThatSuperstarWillTakeSomeDamage(superStarOpponent.Name!, damage);
        _opponent.TakeDamage(damage);
    }
}