using RawDeal.SuperStarsCards;
using RawDealView;
namespace RawDeal.Exceptions;

public class ReversalFromHandException: Exception
{
    public void ReversalFromHandMessage(View view, Player player, int optionCardChoosed)
    {
        SuperStar superStarOpponent = player.SuperStar;
        view.SayThatPlayerReversedTheCard(superStarOpponent.Name!, player.MakeAListOfReversalCards()[optionCardChoosed]);
    }
}