using Microsoft.EntityFrameworkCore;
using NftApi.Data.Models;
using NftApi.Http.Models;

namespace NftApi.Data.Services;

public abstract class NftManagerBase<T> : INftManager<T> where T : NftBase
{
    protected NftManagerBase(ApplicationDbContext context, DbSet<T> nfts, string projectName, string tokenPrefix)
    {
        Context = context;
        Nfts = nfts;
        ProjectName = projectName;
        TokenPrefix = tokenPrefix;
    }

    protected ApplicationDbContext Context { get; }

    protected DbSet<T> Nfts { get; }

    public string TokenPrefix { get; }

    public string ProjectName { get; }

    public IQueryable<T> Query => Nfts.AsQueryable();

    public Task<T> FindById(int id)
    {
        return Query.FirstOrDefaultAsync(nft => nft.Edition == id);
    }

    public virtual async Task UpdateMint(List<NftMakerProNft> response)
    {
        foreach (var nft in response)
        {
            var edition = int.Parse(nft.Name);
            var match = await Nfts.FindAsync(edition);

            if (match is null)
            {
                continue;
            }

            match.Minted = nft.Minted;

            if (nft.Minted && !match.MintedAt.HasValue)
            {
                match.MintedAt = DateTime.UtcNow;
            }

            match.Ipfs = nft.IpfsLink;
            match.Image = nft.GatewayLink;

            Context.Update(match);
        }

        await Context.SaveChangesAsync();
    }

    public virtual async Task UpdateSales(List<CnftIoListing> listings)
    {
        var processedIds = new List<int>();

        foreach (var listing in listings)
        {
            var name = listing.Asset.AssetId[TokenPrefix.Length..];
            var edition = int.Parse(name);
            var match = await Nfts
                .Include(nft => nft.Offers)
                .FirstOrDefaultAsync(nft => nft.Edition == edition);

            if (match is null)
            {
                continue;
            }

            if (match.Offers is not null && match.Offers.Count > 0)
            {
                Context.Offers.RemoveRange(match.Offers);
            }

            match.Minted = true;
            match.OnSale = true;
            match.SalePrice = (int)(listing.Price / 1000000);
            match.MarketId = listing.Id;
            match.ListedAt = listing.CreatedAt ?? listing.UpdatedAt;
            match.IsAuction = listing.IsAuction;
            match.Offers = listing.Offers
                .Select(offer => offer.Normalize())
                .Where(offer => offer.Expires > DateTime.UtcNow)
                .ToList();

            Context.Update(match);
            processedIds.Add(edition);
        }

        var offMarketNfts = await Nfts
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

    public virtual Task UpdateRarity()
    {
        throw new NotImplementedException(
            $"A custom rarity update implementation has not been provided for the {ProjectName} collection");
    }

    protected static void CalculateAttributeScores(int nftsCount,
        params Dictionary<string, AttributeRarityData>[] attributes)
    {
        foreach (var attribute in attributes)
        {
            foreach (var (_, value) in attribute)
            {
                var occurrences = value.Occurrences;
                var probability = (decimal)occurrences / nftsCount;

                value.Score = Math.Round(1 / probability, 2);
                value.Percent = Math.Round(probability * 100, 2);
            }
        }
    }

    protected void SetRankFromScore(IEnumerable<T> nfts)
    {
        var ordered = nfts
            .OrderByDescending(nft => nft.Score)
            .ToList();

        for (var i = 0; i < ordered.Count; i++)
        {
            var nft = ordered.ElementAt(i);
            nft.Rank = i + 1;
        }

        Context.UpdateRange(ordered);
    }

    protected class AttributeRarityData
    {
        public int Occurrences { get; set; }

        public decimal Percent { get; set; }

        public decimal Score { get; set; }
    }
}
