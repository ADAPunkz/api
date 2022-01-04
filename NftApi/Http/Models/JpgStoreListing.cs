using System.Text.Json.Serialization;

namespace NftApi.Http.Models;

public class JpgStoreListing : ExternalListing
{
    public string Asset { get; set; }

    public string Name { get; set; }

    [JsonPropertyName("price_lovelace")]
    public string PriceLovelace { get; set; }

    public override NormalizedListing Normalize(string tokenPrefix)
    {
        var name = Name[tokenPrefix.Length..];
        var edition = int.Parse(name);
        var price = long.Parse(PriceLovelace);

        return new NormalizedListing
        {
            Edition = edition,
            SalePrice = (int)(price / 1000000),
            MarketUrl = $"https://www.jpg.store/asset/{Asset}"
        };
    }
}
