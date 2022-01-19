using System.Text.Json.Serialization;
using NftApi.Extensions;

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
        var price = PriceLovelace.ToAda();

        return new NormalizedListing
        {
            Edition = edition,
            SalePrice = price,
            MarketUrl = $"https://www.jpg.store/asset/{Asset}"
        };
    }
}
