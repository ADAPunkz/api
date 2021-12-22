using System.Text.Json.Serialization;

namespace NftApi.Http.Models;

public class CnftIoListing
{
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    public string Name { get; set; }

    public long Price { get; set; }

    public string Type { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public CnftIoAsset Asset { get; set; }

    public List<CnftIoOffer> Offers { get; set; }

    public bool IsAuction => Type is "auction" or "offer";
}
