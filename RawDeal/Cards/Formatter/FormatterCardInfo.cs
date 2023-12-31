using RawDealView.Formatters;

namespace RawDeal.Cards.Formatter;

public class FormatterCardInfo: IViewableCardInfo
{
    public string Title { get; }
    public string Fortitude { get; }
    public string? Damage { get; }
    public string StunValue { get; }
    public List<string>? Types { get; }
    public List<string> Subtypes { get; }
    public string? CardEffect { get; }

    public FormatterCardInfo(Card card)
    {
        Title = card.Title;
        Fortitude = card.Fortitude;
        Damage = card.Damage;
        StunValue = card.StunValue;
        Types = card.Types;
        Subtypes = card.Subtypes!;
        CardEffect = card.CardEffect;
    }
}