using RawDeal.Cards;

namespace RawDeal.Utils;

public class FormatterCardRepresentation
{
    public Card? CardInObjectFormat { get; set; }
    public string? CardInStringFormat { get; set; }
    public string? Type { get; set; }
    public ICardTypeStrategy CardTypeStrategy { get; set; } = null!;
    // public string PlayedFromDeck { get; set; } = null!;
}