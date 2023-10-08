using System.Text.Json;

namespace RawDeal.Cards;

public class CardInfo
{
    private readonly List<Root>? _cardsJson;
    
    public CardInfo()
    {
        var infoCards = File.ReadAllText(Path.Combine("data", "cards.json"));
        _cardsJson = JsonSerializer.Deserialize<List<Root>>(infoCards, 
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }
    
    private class Root
    {
        public string? Title { get; set; }
        public List<string>? Types { get; set; }
        public List<string>? Subtypes { get; set; }
        public string? Fortitude { get; set; }
        public string? Damage { get; set; }
        public string? StunValue { get; set; }
        public string? CardEffect { get; set; }
    }
    
    public void LoadCardData(Card card)
    {
        foreach (var cardJson in _cardsJson!.Where(cardJson => cardJson.Title == card.Title))
        {
            card.Types = cardJson.Types;
            card.Subtypes = cardJson.Subtypes;
            card.Fortitude = cardJson.Fortitude!;
            card.Damage = cardJson.Damage;
            card.StunValue = cardJson.StunValue!;
            card.CardEffect = cardJson.CardEffect ?? "";
        }
    }
}