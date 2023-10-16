using RawDealView.Formatters;

namespace RawDeal.Cards;

public class FormatterPlayableCardInfo: IViewablePlayInfo
{
    public IViewableCardInfo CardInfo { get; }
    public String PlayedAs { get; }
    
    public FormatterPlayableCardInfo(Card card, string playedAs)
    {
        CardInfo = new FormaterCardInfo(card);
        PlayedAs = playedAs;
    }
}