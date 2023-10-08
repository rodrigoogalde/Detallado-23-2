using RawDealView.Formatters;

namespace RawDeal.Cards;

public class FormaterPlayableCardInfo: IViewablePlayInfo
{
    public IViewableCardInfo CardInfo { get; }
    public String PlayedAs { get; }
    
    public FormaterPlayableCardInfo(Card card, string playedAs)
    {
        CardInfo = new FormaterCardInfo(card);
        PlayedAs = playedAs;
    }
}