using Microsoft.EntityFrameworkCore;
using NftApi.Data.Models;
using NftApi.Http.Models;

namespace NftApi.Data.Services;

public class CollageManager : NftManagerBase<CollageNft>
{
    public CollageManager(ApplicationDbContext dbContext)
        : base(dbContext, dbContext.CollageNfts, "ADAPunkzMVP", "ADAPunkzMVP")
    {
    }

    public new IQueryable<CollageNft> Query => base.Query
        .Where(collage => collage.Minted)
        .Include(collage => collage.Tier)
        .Include(collage => collage.Type);

    public new Task<CollageNft> FindById(int id)
    {
        return Query.FirstOrDefaultAsync(nft => nft.Edition == id);
    }

    public override async Task UpdateMint(List<NftMakerProNft> response)
    {
        await base.UpdateMint(response);
        await UpdateRarity();
    }

    public override async Task UpdateRarity()
    {
        var minted = await Query.ToListAsync();

        var frames = new Dictionary<string, AttributeRarityData>();
        var types = new Dictionary<string, AttributeRarityData>();

        foreach (var nft in minted)
        {
            if (!frames.ContainsKey(nft.Tier.Value))
            {
                frames.Add(nft.Tier.Value, new AttributeRarityData { Occurrences = 1 });
            }
            else
            {
                frames[nft.Tier.Value].Occurrences++;
            }

            if (!types.ContainsKey(nft.Type.Value))
            {
                types.Add(nft.Type.Value, new AttributeRarityData { Occurrences = 1 });
            }
            else
            {
                types[nft.Type.Value].Occurrences++;
            }
        }

        CalculateAttributeScores(minted.Count, frames, types);

        foreach (var nft in minted)
        {
            var frame = frames[nft.Tier.Value];
            var type = types[nft.Type.Value];

            var score = frame.Score + type.Score;

            nft.Score = score;
            nft.Tier.Percent = frame.Percent;
            nft.Type.Percent = type.Percent;
        }

        SetRankFromScore(minted);

        await Context.SaveChangesAsync();
    }

    public override Task<bool> CheckWhitelist(string address)
    {
        return Context.CollageWhitelist.AnyAsync(row => row.Value.Trim().ToLower() == address.Trim().ToLower());
    }

    protected override void SetRankFromScore(IEnumerable<CollageNft> nfts)
    {
        base.SetRankFromScore(nfts);

        // because there are groups of NFTs with equal rarity, those need to be re-ranked to equal rank
        var nftsList = nfts.OrderByDescending(nft => nft.Score).ToList();

        for (var i = 1; i < nftsList.Count; i++)
        {
            var prev = nftsList[i - 1];
            var curr = nftsList[i];

            if (curr.Score == prev.Score)
            {
                curr.Rank = prev.Rank;
                continue;
            }

            curr.Rank = prev.Rank + 1;
        }

        Context.UpdateRange(nftsList);
    }
}
