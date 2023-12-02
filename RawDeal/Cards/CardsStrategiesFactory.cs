using RawDeal.Cards.Type.Action;
using RawDeal.Cards.Type.Maneuver.Effects;
using RawDeal.Cards.Type.Maneuver.Simple;
using RawDeal.Cards.Type.Reversal.General;
using RawDeal.Cards.Type.Reversal.WithoutEffects;
using RawDealView;

namespace RawDeal.Cards;

public class CardsStrategiesFactory
{
    private readonly View _view;
    private readonly Player _player;
    private const string CardPlayAsAction = "ACTION";
    private const string ReversalCardType = "REVERSAL";
    private const string ManeuverCardType = "MANEUVER";
    public CardsStrategiesFactory(View view, Player player)
    {
        _view = view;
        _player = player;
    }
    
    public ICardTypeStrategy BuildCard(Card card, string type)
    {
        switch (type)
        {
            case ManeuverCardType:
                return CreateManeuverStrategy(card);
            case CardPlayAsAction:
                return CreateActionStrategy(card);
            case ReversalCardType:
                return CreateReversalStrategy(card);
            default:
                return null!;
        }
    }

    private ICardTypeStrategy CreateManeuverStrategy(Card card)
    {
        string cardTitle = card.Title;
        ICardTypeStrategy strategy = cardTitle switch
        {
            "Ankle Lock" => new OpponentDiscardCardExecutor(_view, 1),
            "Arm Bar" => new DiscardCardExecutor(_view, _player, 1),
            "Arm Drag" => new DiscardCardExecutor(_view, _player, 1),
            "Austin Elbow Smash" => new AustinElbowSmash(_player),
            "Bear Hug" => new OpponentDiscardCardExecutor(_view, 1),
            "Boston Crab" => new OpponentDiscardCardExecutor(_view, 1),
            "Bulldog" => new Bulldog(_view, _player),
            "Chicken Wing" => new ShuffleExecutor(_view, _player, 2),
            "Choke Hold" => new OpponentDiscardCardExecutor(_view, 1),
            "DDT" => new Ddt(_view, _player),
            "Double Leg Takedown" => new DrawCardExecutor(_view, _player),
            "Figure Four Leg Lock" => new OpponentDiscardCardExecutor(_view, 1),
            "Fisherman's Suplex" => new FishermansSuplex(_view, _player),
            "Guillotine Stretch" => new GuillotineStretch(_view, _player),
            "Head Butt" => new DiscardCardExecutor(_view, _player, 1),
            "Headlock Takedown" => new OpponentCardDrawExecutor(_view, 1),
            "Kick" => new CollateralDamageManeuverExecutor(_view, _player),
            "Lionsault" => new Lionsault(_view, _player), 
            "Power Slam" => new OpponentDiscardCardExecutor(_view, 1),
            "Press Slam" => new PressSlam(_view, _player),
            "Pump Handle Slam" => new OpponentDiscardCardExecutor(_view, 2),
            "Reverse DDT" => new DrawCardExecutor(_view, _player),
            "Running Elbow Smash" => new CollateralDamageManeuverExecutor(_view, _player),
            "Samoan Drop" => new OpponentDiscardCardExecutor(_view, 1),
            "Spinning Heel Kick" => new OpponentDiscardCardExecutor(_view, 1),
            "Standing Side Headlock" => new OpponentCardDrawExecutor(_view, 1),
            "Torture Rack" => new OpponentDiscardCardExecutor(_view, 1),
            "Tree of Woe" => new OpponentDiscardCardExecutor(_view, 2),
            _ => new NoEffectStrategy()
        };
        return strategy;
    }

    private ICardTypeStrategy CreateActionStrategy(Card card)
    {
        string cardTitle = card.Title;
        ICardTypeStrategy strategy = cardTitle switch
        {
            "Offer Handshake" => new OfferHandshake(_view, _player),
            "Jockeying for Position" => new JockeyingForPosition(_view, _player),
            "Puppies! Puppies!" => new Puppies(_view, _player, 5, 2),
            "Recovery" => new Recovery(_view, _player, 2, 1),
            "Spit At Opponent" => new SpitAtOpponent(_view, _player),
            _ => new NoEffectActionStrategy(_view, _player)
        };
        return strategy;
    }

    private ICardTypeStrategy CreateReversalStrategy(Card card)
    {
        string cardTitle = card.Title;
        ICardTypeStrategy strategy = cardTitle switch
        {
            "Break the Hold" => new BreakTheHold(_view, _player),
            "Chyna Interferes" => new ChynaInterferes(_view, _player),
            "Clean Break" => new CleanBreak(_view, _player),
            "Elbow to the Face" => new ElbowToTheFace(_view, _player),
            "Escape Move" => new EscapeMove(_view, _player),
            "Knee to the Gut" => new KneeToTheGut(_view, _player),
            "Manager Interferes" => new ManagerInterferes(_view, _player),
            "No Chance in Hell" => new NoChanceInHell(_view, _player),
            "Jockeying for Position" => new JockeyingForPosition(_view, _player),
            "Rolling Takedown" => new RollingTakedown(_view, _player),
            "Step Aside" => new StepAside(_view, _player),
            _ => new NoEffectStrategy()
        };
        return strategy;
    }
}