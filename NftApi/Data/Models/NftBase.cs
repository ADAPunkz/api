using System.ComponentModel.DataAnnotations;

namespace NftApi.Data.Models;

public abstract class NftBase
{
    [Key]
    public int Edition { get; set; }

    public string Name { get; set; }

    public decimal Score { get; set; }

    public int Rank { get; set; }

    public string Image { get; set; }

    public string Ipfs { get; set; }

    public bool Minted { get; set; }

    public DateTime? MintedAt { get; set; }

    public bool OnSale { get; set; }

    public bool IsAuction { get; set; }

    public int SalePrice { get; set; }

    public string MarketUrl { get; set; }

    public DateTime? ListedAt { get; set; }

    public string MarketName { get; set; }

    public List<Offer> Offers { get; set; } = new();
}
