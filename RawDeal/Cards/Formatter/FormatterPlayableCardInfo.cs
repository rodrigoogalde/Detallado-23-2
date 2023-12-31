using RawDealView.Formatters;

namespace RawDeal.Cards.Formatter;

public class FormatterPlayableCardInfo: IViewablePlayInfo
{
    public IViewableCardInfo CardInfo { get; }
    public String PlayedAs { get; }
    
    public FormatterPlayableCardInfo(Card card, string playedAs)
    {
        CardInfo = new FormatterCardInfo(card);
        PlayedAs = playedAs;
    }
}