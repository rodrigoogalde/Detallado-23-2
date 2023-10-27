using RawDeal.SuperStarsCards;
using RawDealView;
namespace RawDeal.Exceptions;

public class ReversalFromDeckException: Exception
{
    public void ReversalFromDeckMessage(View view, Player player)
    {
        SuperStar superStar = player.SuperStar;
        view.SayThatCardWasReversedByDeck(superStar.Name!);
    }
}