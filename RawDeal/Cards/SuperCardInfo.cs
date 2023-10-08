using System.Text.Json;

namespace RawDeal.Cards;

public class SuperCardInfo
{
    public readonly List<Root>? CardsJson;
    
    public SuperCardInfo()
    {
        var infoCards = File.ReadAllText(Path.Combine("data", "superstar.json"));
        CardsJson = JsonSerializer.Deserialize<List<Root>>(infoCards, 
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }

    public class Root
    {
        public string? Name { get; set; }
        public string? Logo { get; set; }
        public int HandSize { get; set; }
        public int SuperstarValue { get; set; }
        public string? SuperstarAbility { get; set;}
    }
    public void LoadCardData(SuperCard superCard)
    {
        foreach (var cardJson in CardsJson!.Where(cardJson => cardJson.Name == superCard.Name))
        {
            superCard.Logo = cardJson.Logo;
            superCard.HandSize = cardJson.HandSize;
            superCard.SuperstarValue = cardJson.SuperstarValue;
            superCard.SuperstarAbility = cardJson.SuperstarAbility!;
        }
    }
}