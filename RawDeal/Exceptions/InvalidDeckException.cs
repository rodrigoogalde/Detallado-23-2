using RawDealView;
namespace RawDeal;

public class InvalidDeckException: Exception
{
    public void InvalidDeckMessage(View view)
    {   
        view.SayThatDeckIsInvalid();
    }
    
    public void NoCardsLeftToDraw(View view)
    {
        Console.WriteLine("No cards left to draw");
    }
}