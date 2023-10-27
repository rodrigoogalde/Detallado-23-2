using System.Text.Json;
using RawDeal.DeserializeFormatter;

namespace RawDeal.Cards;

public class LoaderSuperCardInfo
{
    public readonly List<SuperCardModel>? CardsJson;
    
    public LoaderSuperCardInfo()
    {
        var infoCards = File.ReadAllText(Path.Combine("data", "superstar.json"));
        CardsJson = JsonSerializer.Deserialize<List<SuperCardModel>>(infoCards, 
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }
    
    public void LoadCardData(SuperCardInfo superCard)
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