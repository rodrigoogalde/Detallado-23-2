using System.Text.Json;
using RawDeal.DeserializeFormatter;

namespace RawDeal.Cards;

public class CardInfo
{
    private readonly List<CardModel>? _cardsJson;
    
    public CardInfo()
    {
        var infoCards = File.ReadAllText(Path.Combine("data", "cards.json"));
        _cardsJson = JsonSerializer.Deserialize<List<CardModel>>(infoCards, 
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
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