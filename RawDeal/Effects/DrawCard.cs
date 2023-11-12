namespace RawDeal.Effects;

public class DrawCard: IEffect
{
    private readonly Player _player;
    
    public DrawCard(Player player)
    {
        _player = player;
    }
    public void Execute()
    {
        _player.DrawCard();
    }
}