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
        .Include(collage => collage.Frame)
        .Include(collage => collage.Type);

    public new Task<CollageNft> FindById(int id)
    {
        return Query.FirstOrDefaultAsync(nft => nft.Edition == id);
    }

    public override async Task UpdateMint(List<NftMakerProNft> response)
    {
        await base.UpdateMint(response);

        // rarity is dynamic as more are minted
        await UpdateRarity();
    }

    public override async Task UpdateRarity()
    {
        var minted = await Query.Where(nft => nft.Minted).ToListAsync();

        var frames = new Dictionary<string, AttributeRarityData>();
        var types = new Dictionary<string, AttributeRarityData>();

        foreach (var nft in minted)
        {
            if (!frames.ContainsKey(nft.Frame.Value))
            {
                frames.Add(nft.Frame.Value, new AttributeRarityData { Occurrences = 1 });
            }
            else
            {
                frames[nft.Frame.Value].Occurrences++;
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
            var frame = frames[nft.Frame.Value];
            var type = types[nft.Type.Value];

            var score = frame.Score + type.Score;

            nft.Score = score;
            nft.Frame.Percent = frame.Percent;
            nft.Type.Percent = type.Percent;
        }

        SetRankFromScore(minted);

        await Context.SaveChangesAsync();
    }
}
