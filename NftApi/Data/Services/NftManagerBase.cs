using Microsoft.EntityFrameworkCore;
using NftApi.Data.Models;
using NftApi.Http.Models;

namespace NftApi.Data.Services;

public abstract class NftManagerBase<T> where T : NftBase
{
    protected ApplicationDbContext Context { get; }

    protected NftManagerBase(ApplicationDbContext context)
    {
        Context = context;
    }

    protected async Task UpdateMint(DbSet<T> set, List<GetNftsResponse> response)
    {
        foreach (var nft in response)
        {
            var edition = int.Parse(nft.Name);
            var match = await set.FindAsync(edition);

            match.Minted = nft.Minted;
            match.MintedAt = DateTime.UtcNow;
            match.Ipfs = nft.IpfsLink;
            match.Image = nft.GatewayLink;

            Context.Update(match);
        }

        await Context.SaveChangesAsync();
    }

    protected async Task UpdateSales(DbSet<T> set, List<CnftIoListing> listings, string tokenPrefix)
    {
        var processedIds = new List<int>();

        foreach (var listing in listings)
        {
            var name = listing.Asset.AssetId[tokenPrefix.Length..];
            var edition = int.Parse(name);

            var nft = await set.Include(nft => nft.Offers).FirstOrDefaultAsync(nft => nft.Edition == edition);

            if (nft.Offers is not null && nft.Offers.Count > 0)
            {
                Context.Offers.RemoveRange(nft.Offers);
            }

            nft.Minted = true;
            nft.OnSale = true;
            nft.SalePrice = (int)(listing.Price / 1000000);
            nft.MarketId = listing.Id;
            nft.ListedAt = listing.CreatedAt;
            nft.IsAuction = listing.IsAuction;
            nft.Offers = listing.Offers.Select(offer => offer.Normalize()).ToList();

            Context.Update(nft);
            processedIds.Add(edition);
        }

        var offMarketNfts = await set
            .AsQueryable()
            .Include(nft => nft.Offers)
            .Where(nft => !processedIds.Contains(nft.Edition))
            .ToListAsync();

        foreach (var nft in offMarketNfts)
        {
            if (nft.Offers is not null && nft.Offers.Count > 0)
            {
                Context.Offers.RemoveRange(nft.Offers);
            }

            nft.OnSale = false;
            nft.SalePrice = 0;
            nft.MarketId = null;
            nft.ListedAt = null;
            nft.IsAuction = false;
            nft.Offers = new List<Offer>();

            Context.Update(nft);
        }

        await Context.SaveChangesAsync();
    }
}
