using RawDeal.Effects;
using RawDeal.SuperStarsCards;
using RawDeal.Utils;
using RawDealView;

namespace RawDeal.Cards.Type.Action;

public class NoEffectActionStrategy: ICardActionStrategy
{
    private readonly View _view;
    private readonly Player _player;
    private readonly SuperStar _superStar;
    private Card? _card;
    
    public NoEffectActionStrategy(View view,Player player)
    {
        _view = view;
        _player = player;
        _superStar = _player.SuperStar;
    }
    public bool IsEffectApplicable()
    {   
        return true;
    }

    public void PerformEffect(FormatterCardRepresentation card, Player opponent)
    {
        _card = card.CardInObjectFormat!;
        PerformAction(opponent);
    }

    public void PerformAction(Player opponent)
    {
        _player.MoveCardFromHandToRingside(_card!);
        _view.SayThatPlayerMustDiscardThisCard(_superStar.Name!, _card!.Title);
        DrawCard drawCard = new DrawCard(_player, _view, 1);
        drawCard.Execute();
    }
}