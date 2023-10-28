using RawDeal.SuperStarsCards;
using RawDeal.Utils;
using RawDealView;
namespace RawDeal.Exceptions;

public class ReversalFromHandException: Exception
{
    private readonly FormatterCardRepresentation _cardChoosed;
    public ReversalFromHandException(FormatterCardRepresentation cardChoosed)
    {
        _cardChoosed = cardChoosed;
    }
    public void ReversalFromHandMessage(View view, Player player)
    {
        SuperStar superStarOpponent = player.SuperStar;
        view.SayThatPlayerReversedTheCard(superStarOpponent.Name!, _cardChoosed.CardInStringFormat!);
    }
}