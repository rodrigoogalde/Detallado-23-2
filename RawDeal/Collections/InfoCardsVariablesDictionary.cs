namespace RawDeal.Collections;

public class InfoCardsVariablesDictionary
{
    private static readonly Dictionary<string, string?> CardTypes = new Dictionary<string, string?>
    {
        { "ReversalCardType", "Reversal" },
        { "CardPlayAsAction", "Action" },
        { "CardPlayAsManeuver", "Maneuver" }
    };

    public static string? GetCardType(string key)
    {
        CardTypes.TryGetValue(key, out string? value);
        return value;
    }

    public static Dictionary<string, string?> GetAllCardTypes()
    {
        return new Dictionary<string, string?>(CardTypes);
    }
}
