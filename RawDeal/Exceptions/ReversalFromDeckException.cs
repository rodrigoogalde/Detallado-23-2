using RawDeal.SuperStarsCards;
using RawDealView;
namespace RawDeal.Exceptions;

public class ReversalFromDeckException: Exception
{
    private int _stunValue;
    private View _view;
    private Player _playerOnTurn;
    
    public ReversalFromDeckException(int stunValue)
    {
        _stunValue = stunValue;
    }
    public void ReversalFromDeckMessage(View view, Player playerOnTurn, SuperStar superStar)
    {
        _view = view;
        _playerOnTurn = playerOnTurn;
        _view.SayThatCardWasReversedByDeck(superStar.Name!);
        CheckIfThePlayerCanDrawCardsForHisStunValue();
    }

    private void CheckIfThePlayerCanDrawCardsForHisStunValue()
    {
        if (_stunValue == 0) { return;}
        PlayerDrawAsManyCardsAsStunValue();
    }
    
    private void PlayerDrawAsManyCardsAsStunValue()
    {
        SuperStar superStar = _playerOnTurn.SuperStar;
        int cardToDraw = _view.AskHowManyCardsToDrawBecauseOfStunValue(superStar.Name!, _stunValue);
        for (int i = 0; i < cardToDraw; i++)
        {
            _playerOnTurn.DrawCard();
        }
        _view.SayThatPlayerDrawCards(superStar.Name!, cardToDraw);
    }
}