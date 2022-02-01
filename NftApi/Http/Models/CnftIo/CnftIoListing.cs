using System.Text.Json.Serialization;

namespace NftApi.Http.Models.CnftIo;

public class CnftIoListing : ExternalListing
{
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    public string Name { get; set; }

    public long Price { get; set; }

    public string Type { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public CnftIoAsset Asset { get; set; }

    public List<CnftIoOffer> Offers { get; set; } = new();

    public bool IsAuction => Type is "auction" or "offer";

    public override NormalizedListing Normalize(string tokenPrefix)
    {
        var name = Asset.AssetId[tokenPrefix.Length..];
        var edition = int.Parse(name);
        var offers = Offers
            .Select(offer => offer.Normalize())
            .Where(offer => offer.Expires > DateTime.UtcNow)
            .ToList();

        return new NormalizedListing
        {
            Edition = edition,
            SalePrice = (int)(Price / 1000000),
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt,
            MarketUrl = $"https://cnft.io/token/{Id}",
            IsAuction = IsAuction,
            Offers = offers
        };
    }
}
