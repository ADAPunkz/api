using Microsoft.EntityFrameworkCore;
using NftApi.Data.Models;
using NftApi.Http.Models;

namespace NftApi.Data.Services;

public class PunkzManager : NftManagerBase<PunkzNft>
{
    public PunkzManager(ApplicationDbContext dbContext)
        : base(dbContext, dbContext.PunkzNfts, "ADAPunkz", "ADAPunk")
    {
    }

    public new IQueryable<PunkzNft> Query => base.Query
        .Include(punk => punk.Accessories)
        .Include(punk => punk.Background)
        .Include(punk => punk.Eyes)
        .Include(punk => punk.Head)
        .Include(punk => punk.ImplantNodes)
        .Include(punk => punk.Mouth)
        .Include(punk => punk.Type);

    public new Task<PunkzNft> FindById(int id)
    {
        return Query.FirstOrDefaultAsync(nft => nft.Edition == id);
    }
}
