using RawDeal.Cards;
using RawDealView;
using RawDealView.Options;

namespace RawDeal.SuperStarsCards;

public class Mankind: SuperStar
{   
    
    private const int OneCard = 1;
    public Mankind(SuperCard superCard, Player player, View view) : base(superCard, player, view)
    {
        SuperCard = superCard;
        Player = player;
        GameView = view;
    }

    public override bool HasTheConditionsToUseAbility()
    {
        return false;
    }

    public override void UseAbility(Player playerOnWait)
    {
    }

    public override bool CanSteelMoreThanOneCard()
    {
        // ELIMINAR
        // return Player.TransformCardsInArsenalIntoStringFormat().Count >= OneCard;
        return Player.ChooseWhichMazeOfCardsTransformToStringFormat(CardSet.Arsenal).Count >= OneCard;
        
    }
    
    public override bool IsManKind()
    { return true; }
}