using NftApi.Data.Models;

namespace NftApi.Http.Models;

public class NormalizedListing
{
    public int Edition { get; set; }

    public int SalePrice { get; set; }

    public string MarketUrl { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsAuction { get; set; }

    public List<Offer> Offers { get; set; } = new();
}
