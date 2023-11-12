using RawDeal.Effects;

namespace RawDeal.Cards.Reversal;

public abstract class Reversal
{
    private List<IEffect> _effects;
    
    public void AddEffect(IEffect effect)
    {
        _effects.Add(effect);
    }
    
    public void Execute()
    {
        foreach (var effect in _effects)
        {
            effect.Execute();
        }
    }
    
    
}