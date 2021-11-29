using Microsoft.EntityFrameworkCore;
using NftApi.Data.Models;

namespace NftApi.Data.Services;

public class PunkzManager : NftManagerBase<PunkzNft>, INftManager<PunkzNft>
{
    public const string PROJECT_NAME = "ADAPunkz";
    public const string TOKEN_PREFIX = "ADAPunk";

    public PunkzManager(ApplicationDbContext dbContext) : base(dbContext, dbContext.PunkzNfts, PROJECT_NAME, TOKEN_PREFIX)
    { }

    public new IQueryable<PunkzNft> Query => base.Query
        .Include(punk => punk.Accessories)
        .Include(punk => punk.Background)
        .Include(punk => punk.Eyes)
        .Include(punk => punk.Head)
        .Include(punk => punk.ImplantNodes)
        .Include(punk => punk.Mouth)
        .Include(punk => punk.Type)
        .Include(punk => punk.Offers);
}
