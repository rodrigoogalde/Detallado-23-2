using RawDealView;

namespace RawDeal.Exceptions;

public class InvalidDeckException: Exception
{
    public void InvalidDeckMessage(View view)
    {   
        view.SayThatDeckIsInvalid();
    }
}