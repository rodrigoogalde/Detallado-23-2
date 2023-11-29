using RawDeal.Effects;
using RawDeal.Options;
using RawDeal.SuperStarsCards;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Reversal.General;

public class ElbowToTheFace: ICardReversalStrategy
{
    private readonly View _view;
    private readonly Player _player;
    private readonly Game _game;
    
    public ElbowToTheFace(View view, Player player, Game game)
    {
        _view = view;
        _player = player;
        _game = game;
    }
    
    public bool IsEffectApplicable()
    {
        return IsReversalApplicable(_player);
    }
    
    public bool IsReversalApplicable(Player player)
    {
        FormatterCardRepresentation card = player.GetLastCardPlayedByOpponent();
        Card cardInObjectFormat = card.CardInObjectFormat!;
        return card.Type == "MANEUVER" &&
               int.Parse(cardInObjectFormat.Damage!) <= 7;
    }

    public void PerformEffect(FormatterCardRepresentation card, Game game, Player player, Player playerOnWait)
    {
        PerformReversal(card, player);
    }

    public void PerformReversal(FormatterCardRepresentation card, Player player)
    {
        ReverseAndDamage reverseAndDamage = new ReverseAndDamage(_view, _player, player, card);
        reverseAndDamage.Execute();
    }
}