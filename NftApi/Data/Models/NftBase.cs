using System.ComponentModel.DataAnnotations;

namespace NftApi.Data.Models;

public abstract class NftBase
{
    [Key]
    public virtual int Edition { get; set; }

    public virtual string Name { get; set; }

    public virtual decimal Score { get; set; }

    public virtual int Rank { get; set; }

    public virtual string Image { get; set; }

    public virtual string Ipfs { get; set; }

    public virtual bool Minted { get; set; }

    public virtual DateTime MintedAt { get; set; }

    public virtual bool OnSale { get; set; }

    public virtual bool IsAuction { get; set; }

    public virtual int SalePrice { get; set; }

    public virtual string MarketId { get; set; }

    public virtual DateTime? ListedAt { get; set; }

    public virtual List<Offer> Offers { get; set; } = new();
}
