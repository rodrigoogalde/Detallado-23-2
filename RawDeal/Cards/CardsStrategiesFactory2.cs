using RawDeal.Cards.Maneuver;
using RawDeal.Cards.Maneuver.Simple;
using RawDeal.Cards.Reversal.General;
using RawDeal.Cards.Reversal.WithoutEffects;
using RawDeal.Cards.Type.Action;
using RawDeal.Cards.Type.Maneuver.Effects;
using RawDeal.Cards.Type.Maneuver.Simple;
using RawDealView;

namespace RawDeal.Cards;

public class CardsStrategiesFactory2
{
    private readonly View _view;
    private readonly Player _player;
    private readonly Game _game;
    public CardsStrategiesFactory2(View view, Player player, Game game)
    {
        _view = view;
        _player = player;
        _game = game;
    }
    
    public ICardTypeStrategy BuildCard(Card card, string type)
    {
        string cardTitle = card.Title;
        switch (cardTitle)
        {
            case "Abdominal Stretch":
                // Handle Abdominal Stretch here
                break;
            case "Ankle Lock":
                return new OpponentsDiscardsCardsFromHand(_view, _player, 1);
            case "Arm Bar":
                return new PlayerDiscardCardFromHisHand(_view, _player, 1);
            case "Arm Bar Takedown":
                // TODO: Check as action
                return new NoManeuverEffect();
                break;
            case "Arm Drag":
                return new PlayerDiscardCardFromHisHand(_view, _player, 1);
            case "Atomic Drop":
                // Handle Atomic Drop here
                break;
            case "Atomic Facebuster":
                // Handle Atomic Facebuster here
                break;
            case "Austin Elbow Smash":
                // Handle Austin Elbow Smash here
                break;
            case "Ayatollah of Rock 'n' Roll-a":
                // Handle Ayatollah of Rock 'n' Roll-a here
                break;
            case "Back Body Drop":
                // Handle Back Body Drop here
                break;
            case "Back Breaker":
                return new NoManeuverEffect();
            case "Bear Hug":
                return new OpponentsDiscardsCardsFromHand(_view, _player, 1);
            case "Belly to Back Suplex":
                // Handle Belly to Back Suplex here
                break;
            case "Belly to Belly Suplex":
                // Handle Belly to Belly Suplex here
                break;
            case "Big Boot":
                return new NoManeuverEffect();
            case "Body Slam":
                // Handle Body Slam here
                break;
            case "Boston Crab":
                return new OpponentsDiscardsCardsFromHand(_view, _player, 1);
            case "Bow & Arrow":
                // Handle Bow & Arrow here
                break;
            case "Break the Hold":
                return new BreakTheHold(_view, _player);
            case "Bulldog":
                return new Bulldog(_view, _player);
            case "Camel Clutch":
                // Handle Camel Clutch here
                break;
            case "Chair Shot":
                return new NoManeuverEffect();
            case "Chicken Wing":
                return new PlayerMoveCardFromRingSideToArsenal(_view, _player, 2);
            case "Chin Lock":
                // Handle Chin Lock here
                break;
            case "Choke Hold":
                return new OpponentsDiscardsCardsFromHand(_view, _player, 1);
            case "Chop":
                // Handle Chop here
                break;
            case "Chyna Interferes":
                return new ChynaInterferes(_view, _player);
            case "Clean Break":
                return new CleanBreak(_view, _player);
            case "Clothesline":
                // Handle Clothesline here
                break;
            case "Cobra Clutch":
                // Handle Cobra Clutch here
                break;
            case "Collar & Elbow Lockup":
                // Handle Collar & Elbow Lockup here
                break;
            case "Comeback!":
                // Handle Comeback! here
                break;
            case "Cross Body Block":
                // Handle Cross Body Block here
                break;
            case "DDT":
                // Handle DDT here
                break;
            case "Deluding Yourself":
                // Handle Deluding Yourself here
                break;
            case "Discus Punch":
                // Handle Discus Punch here
                break;
            case "Disqualification!":
                // Handle Disqualification! here
                break;
            case "Distract the Ref":
                // Handle Distract the Ref here
                break;
            case "Diversion":
                // Handle Diversion here
                break;
            case "Don't Think Too Hard":
                // Handle Don't Think Too Hard here
                break;
            case "Don't You Never... EVER!":
                // Handle Don't You Never... EVER! here
                break;
            case "Double Arm DDT":
                // Handle Double Arm DDT here
                break;
            case "Double Digits":
                // Handle Double Digits here
                break;
            case "Double Leg Takedown":
                return new PlayerDrawCards(_view, _player);
            case "Drop Kick":
                // Handle Drop Kick here
                break;
            case "Ego Boost":
                // Handle Ego Boost here
                break;
            case "Elbow to the Face":
                return new ElbowToTheFace(_view, _player);
            case "Ensugiri":
                // Handle Ensugiri here
                break;
            case "Escape Move":
                return new EscapeMove(_view, _player);
            case "Facebuster":
                // Handle Facebuster here
                break;
            case "Figure Four Leg Lock":
                return new OpponentsDiscardsCardsFromHand(_view, _player, 1);
            case "Fireman's Carry":
                // Handle Fireman's Carry here
                break;
            case "Fisherman's Suplex":
                return new FishermansSuplex(_view, _player);
            case "Flash in the Pan":
                // Handle Flash in the Pan here
                break;
            case "Full Nelson":
                // Handle Full Nelson here
                break;
            case "Get Crowd Support":
                // Handle Get Crowd Support here
                break;
            case "Guillotine Stretch":
                return new GuillotineStretch(_view, _player);
            case "Gut Buster":
                return new NoManeuverEffect();
            case "Have a Nice Day!":
                // Handle Have a Nice Day! here
                break;
            case "Haymaker":
                // Handle Haymaker here
                break;
            case "Head Butt":
                return new PlayerDiscardCardFromHisHand(_view, _player, 1);
            case "Headlock Takedown":
                return new OpponentDrawCards(_view, _player, 1);
            case "Hellfire & Brimstone":
                // Handle Hellfire & Brimstone here
                break;
            case "Hip Toss":
                // Handle Hip Toss here
                break;
            case "Hmmm":
                // Handle Hmmm here
                break;
            case "Hurricanrana":
                return new NoManeuverEffect();
            case "I Am the Game":
                // Handle I Am the Game here
                break;
            case "Inverse Atomic Drop":
                return new NoManeuverEffect();
            case "Irish Whip":
                // Handle Irish Whip here
                break;
            case "Jockeying for Position":
                return new JockeyingForPosition(_view, _player);
            case "Kane's Chokeslam":
                // Handle Kane's Chokeslam here
                break;
            case "Kane's Flying Clothesline":
                // Handle Kane's Flying Clothesline here
                break;
            case "Kane's Return!":
                // Handle Kane's Return! here
                break;
            case "Kane's Tombstone Piledriver":
                // Handle Kane's Tombstone Piledriver here
                break;
            case "Kick":
                return new PlayerMoveCardFromArsenalToRingSide(_player);
            case "Knee to the Gut":
                return new KneeToTheGut(_view, _player);
            case "Leaping Knee to the Face":
                // Handle Leaping Knee to the Face here
                break;
            case "Lionsault":
                return new Lionsault(_view, _player); 
            case "Lou Thesz Press":
                // Handle Lou Thesz Press here
                break;
            case "Maintain Hold":
                // Handle Maintain Hold here
                break;
            case "Manager Interferes":
                return new ManagerInterferes(_view, _player);
            case "Mandible Claw":
                // Handle Mandible Claw here
                break;
            case "Marking Out":
                // Handle Marking Out here
                break;
            case "Mr. Socko":
                // Handle Mr. Socko here
                break;
            case "No Chance in Hell":
                return new NoChanceInHell(_view, _player);
            case "Not Yet":
                // Handle Not Yet here
                break;
            case "Offer Handshake":
                return new OfferHandshake(_view, _player);
            case "Open Up a Can of Whoop-A%$":
                // Handle Open Up a Can of Whoop-A%$ here
                break;
            case "Pat & Gerry":
                // Handle Pat & Gerry here
                break;
            case "Pedigree":
                // Handle Pedigree here
                break;
            case "Power Slam":
                return new OpponentsDiscardsCardsFromHand(_view, _player, 1);
            case "Power of Darkness":
                // Handle Power of Darkness here
                break;
            case "Powerbomb":
                // Handle Powerbomb here
                break;
            case "Press Slam":
                return new PressSlam(_view, _player);
            case "Pump Handle Slam":
                return new OpponentsDiscardsCardsFromHand(_view, _player, 2);
            case "Punch":
                // Handle Punch here
                break;
            case "Puppies! Puppies!":
                return new Puppies(_view, _player, 5, 2);
            case "Recovery":
                return new Recovery(_view, _player, 2, 1);
            case "Reverse DDT":
                return new PlayerDrawCards(_view, _player);
            case "Rock Bottom":
                // Handle Rock Bottom here
                break;
            case "Roll Out of the Ring":
                // Handle Roll Out of the Ring here
                break;
            case "Rolling Takedown":
                return new RollingTakedown(_view, _player);
            case "Roundhouse Punch":
                // Handle Roundhouse Punch here
                break;
            case "Running Elbow Smash":
                return new PlayerMoveCardFromArsenalToRingSide(_player);
            case "Russian Leg Sweep":
                return new NoManeuverEffect();
            case "Samoan Drop":
                return new OpponentsDiscardsCardsFromHand(_view, _player, 1);
            case "Shake It Off":
                // Handle Shake It Off here
                break;
            case "Shane O'Mac":
                // Handle Shane O'Mac here
                break;
            case "Shoulder Block":
                // Handle Shoulder Block here
                break;
            case "Sit Out Powerbomb":
                // Handle Sit Out Powerbomb here
                break;
            case "Sleeper":
                // Handle Sleeper here
                break;
            case "Smackdown Hotel":
                // Handle Smackdown Hotel here
                break;
            case "Snap Mare":
                // Handle Snap Mare here
                break;
            case "Spear":
                // Handle Spear here
                break;
            case "Spinning Heel Kick":
                return new OpponentsDiscardsCardsFromHand(_view, _player, 1);
            case "Spit At Opponent":
                return new SpitAtOpponent(_view, _player);
            case "Stagger":
                // Handle Stagger here
                break;
            case "Standing Side Headlock":
                return new OpponentDrawCards(_view, _player, 1);
            case "Step Aside":
                return new StepAside(_view, _player);
            case "Step Over Toe Hold":
                // Handle Step Over Toe Hold here
                break;
            case "Stone Cold Stunner":
                // Handle Stone Cold Stunner here
                break;
            case "Superkick":
                // Handle Superkick here
                break;
            case "Take That Move, Shine It Up Real Nice, Turn That Sumb*tch Sideways, and Stick It Straight Up Your Roody Poo Candy A%$!":
                // Handle Take That Move, Shine It Up Real Nice, Turn That Sumb*tch Sideways, and Stick It Straight Up Your Roody Poo Candy A%$! here
                break;
            case "The People's Elbow":
                // Handle The People's Elbow here
                break;
            case "The People's Eyebrow":
                // Handle The People's Eyebrow here
                break;
            case "Torture Rack":
                return new OpponentsDiscardsCardsFromHand(_view, _player, 1);
            case "Tree of Woe": // TODO: Check this
                return new OpponentsDiscardsCardsFromHand(_view, _player, 1);
            case "Undertaker Sits Up!":
                // Handle Undertaker Sits Up! here
                break;
            case "Undertaker's Chokeslam":
                // Handle Undertaker's Chokeslam here
                break;
            case "Undertaker's Flying Clothesline":
                // Handle Undertaker's Flying Clothesline here
                break;
            case "Undertaker's Tombstone Piledriver":
                // Handle Undertaker's Tombstone Piledriver here
                break;
            case "Vertical Suplex":
                // Handle Vertical Suplex here
                break;
            case "View of Villainy":
                // Handle View of Villainy here
                break;
            case "Walls of Jericho":
                // Handle Walls of Jericho here
                break;
            case "Whaddya Got?":
                // Handle Whaddya Got? here
                break;
            case "Wrist Lock":
                // Handle Wrist Lock here
                break;
            case "Y2J":
                // Handle Y2J here
                break;
            default:
                // Handle default case here
                return null!;
        }
        return null!;
    }
}