using RawDeal.Cards.Maneuver;
using RawDeal.Cards.Reversal.General;
using RawDeal.Cards.Reversal.WithoutEffects;
using RawDeal.Effects;
using RawDealView;

namespace RawDeal.Cards.Reversal;

public class CardsStrategiesFactory
{
    private Dictionary<string, ICardTypeStrategy> _card;
    private View _view;
    private Player _player;
    public CardsStrategiesFactory(View view, Player player)
    {
        _view = view;
        _player = player;
        _card = new Dictionary<string, ICardTypeStrategy>();
    }
    
    public Dictionary<string, ICardTypeStrategy> BuildCard(Card card)
    {
        Dictionary<string, ICardTypeStrategy> cardStrategy = new Dictionary<string, ICardTypeStrategy>();
        string cardTitle = card.Title;
        switch (cardTitle)
        {
            case "Abdominal Stretch":
                // Handle Abdominal Stretch here
                break;
            case "Ankle Lock":
                // Handle Ankle Lock here
                break;
            case "Arm Bar":
                cardStrategy.Add("MANEUVER", new PlayerDiscardCardFromHisHand(_view, _player, 1));
                break;
            case "Arm Bar Takedown":
                // Handle Arm Bar Takedown here
                break;
            case "Arm Drag":
                cardStrategy.Add("MANEUVER", new PlayerDiscardCardFromHisHand(_view, _player, 1));
                break;
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
                // Handle Back Breaker here
                break;
            case "Bear Hug":
                cardStrategy.Add("MANEUVER", new OpponentsDiscardsCardsFromHand(_view, _player, 1));
                break;
            case "Belly to Back Suplex":
                // Handle Belly to Back Suplex here
                break;
            case "Belly to Belly Suplex":
                // Handle Belly to Belly Suplex here
                break;
            case "Big Boot":
                // Handle Big Boot here
                break;
            case "Body Slam":
                // Handle Body Slam here
                break;
            case "Boston Crab":
                // Handle Boston Crab here
                break;
            case "Bow & Arrow":
                // Handle Bow & Arrow here
                break;
            case "Break the Hold":
                cardStrategy.Add("REVERSAL", new BreakTheHold(_view));
                break;
            case "Bulldog":
                // Handle Bulldog here
                break;
            case "Camel Clutch":
                // Handle Camel Clutch here
                break;
            case "Chair Shot":
                // Handle Chair Shot here
                break;
            case "Chicken Wing":
                // Handle Chicken Wing here
                break;
            case "Chin Lock":
                // Handle Chin Lock here
                break;
            case "Choke Hold":
                cardStrategy.Add("MANEUVER", new OpponentsDiscardsCardsFromHand(_view, _player, 1));
                break;
            case "Chop":
                // Handle Chop here
                break;
            case "Chyna Interferes":
                cardStrategy.Add("REVERSAL", new ChynaInterferes(_view));
                break;
            case "Clean Break":
                cardStrategy.Add("REVERSAL", new CleanBreak(_view));
                break;
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
                // Handle Double Leg Takedown here
                break;
            case "Drop Kick":
                // Handle Drop Kick here
                break;
            case "Ego Boost":
                // Handle Ego Boost here
                break;
            case "Elbow to the Face":
                cardStrategy.Add("REVERSAL", new ElbowToTheFace(_view));
                break;
            case "Ensugiri":
                // Handle Ensugiri here
                break;
            case "Escape Move":
                cardStrategy.Add("REVERSAL", new EscapeMove(_view));
                break;
            case "Facebuster":
                // Handle Facebuster here
                break;
            case "Figure Four Leg Lock":
                // Handle Figure Four Leg Lock here
                break;
            case "Fireman's Carry":
                // Handle Fireman's Carry here
                break;
            case "Fisherman's Suplex":
                // Handle Fisherman's Suplex here
                break;
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
                // Handle Guillotine Stretch here
                break;
            case "Gut Buster":
                // Handle Gut Buster here
                break;
            case "Have a Nice Day!":
                // Handle Have a Nice Day! here
                break;
            case "Haymaker":
                // Handle Haymaker here
                break;
            case "Head Butt":
                cardStrategy.Add("MANEUVER", new PlayerDiscardCardFromHisHand(_view, _player, 1));
                break;
            case "Headlock Takedown":
                // Handle Headlock Takedown here
                break;
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
                // Handle Hurricanrana here
                break;
            case "I Am the Game":
                // Handle I Am the Game here
                break;
            case "Inverse Atomic Drop":
                // Handle Inverse Atomic Drop here
                break;
            case "Irish Whip":
                // Handle Irish Whip here
                break;
            case "Jockeying for Position":
                // Handle Jockeying for Position here
                break;
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
                // Handle Kick here
                break;
            case "Knee to the Gut":
                cardStrategy.Add("REVERSAL", new KneeToTheGut(_view));
                break;
            case "Leaping Knee to the Face":
                // Handle Leaping Knee to the Face here
                break;
            case "Lionsault":
                // Handle Lionsault here
                break;
            case "Lou Thesz Press":
                // Handle Lou Thesz Press here
                break;
            case "Maintain Hold":
                // Handle Maintain Hold here
                break;
            case "Manager Interferes":
                cardStrategy.Add("REVERSAL", new ManagerInterferes(_view));
                break;
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
                cardStrategy.Add("REVERSAL", new NoChanceInHell(_view));
                break;
            case "Not Yet":
                // Handle Not Yet here
                break;
            case "Offer Handshake":
                // Handle Offer Handshake here
                break;
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
                // Handle Power Slam here
                break;
            case "Power of Darkness":
                // Handle Power of Darkness here
                break;
            case "Powerbomb":
                // Handle Powerbomb here
                break;
            case "Press Slam":
                // Handle Press Slam here
                break;
            case "Pump Handle Slam":
                // Handle Pump Handle Slam here
                break;
            case "Punch":
                // Handle Punch here
                break;
            case "Puppies! Puppies!":
                // Handle Puppies! Puppies! here
                break;
            case "Recovery":
                // Handle Recovery here
                break;
            case "Reverse DDT":
                // Handle Reverse DDT here
                break;
            case "Rock Bottom":
                // Handle Rock Bottom here
                break;
            case "Roll Out of the Ring":
                // Handle Roll Out of the Ring here
                break;
            case "Rolling Takedown":
                cardStrategy.Add("REVERSAL", new RollingTakedown(_view));
                break;
            case "Roundhouse Punch":
                // Handle Roundhouse Punch here
                break;
            case "Running Elbow Smash":
                // Handle Running Elbow Smash here
                break;
            case "Russian Leg Sweep":
                // Handle Russian Leg Sweep here
                break;
            case "Samoan Drop":
                // Handle Samoan Drop here
                break;
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
                // Handle Spinning Heel Kick here
                break;
            case "Spit At Opponent":
                // Handle Spit At Opponent here
                break;
            case "Stagger":
                // Handle Stagger here
                break;
            case "Standing Side Headlock":
                // Handle Standing Side Headlock here
                break;
            case "Step Aside":
                cardStrategy.Add("REVERSAL", new StepAside(_view));
                break;
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
                // Handle Torture Rack here
                break;
            case "Tree of Woe":
                // Handle Tree of Woe here
                break;
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
                return null;
                break;
        }
        return null;
    }
}