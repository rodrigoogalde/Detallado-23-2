using RawDealView;

namespace RawDeal.Cards.Reversal;

public class StepAside: Reversal
{
    private readonly View _view;
    
    public StepAside(View view)
    {
        _view = view;
    }
    
    public bool IsEffectApplicable(Game game, Player player, Player opponent)
    {
        return IsReversalApplicable(game, player, opponent);
    }
    
    public bool IsReversalApplicable(Game game, Player player, Player opponent)
    {
        return opponent.GetLastCardPlayedAs() == "MANEUVER" &&
               opponent.GetLastCardPlayedSubtypes().Contains("Strike");
    }
}