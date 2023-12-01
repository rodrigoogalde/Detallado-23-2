namespace RawDealView;

public class PlayerInfo
{
    private readonly string _superStarName;
    private readonly int _fortitudeRating;
    private readonly int _numberOfCardsInHand;
    private readonly int _numberOfCardsInArsenal;

    public PlayerInfo(string superstarName, int fortitudeRating, int numberOfCardsInHand, int numberOfCardsInArsenal)
    {
        _superStarName = superstarName;
        _fortitudeRating = fortitudeRating;
        _numberOfCardsInHand = numberOfCardsInHand;
        _numberOfCardsInArsenal = numberOfCardsInArsenal;
    }

    public override string ToString()
        => $"{_superStarName}: {_fortitudeRating}F, tiene {_numberOfCardsInHand} cartas en la mano y {_numberOfCardsInArsenal} en el arsenal.";
}