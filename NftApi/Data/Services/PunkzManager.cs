using Microsoft.EntityFrameworkCore;
using NftApi.Data.Models;
using NftApi.Http.Models;

namespace NftApi.Data.Services;

public class PunkzManager : NftManagerBase<PunkzNft>, INftManager<PunkzNft>
{
    public const string TokenPrefix = "ADAPunk";

    public PunkzManager(ApplicationDbContext dbContext) : base(dbContext)
    { }

    public IQueryable<PunkzNft> GetAll() => Context.PunkzNfts
        .AsQueryable()
        .Include(punk => punk.Accessories)
        .Include(punk => punk.Background)
        .Include(punk => punk.Eyes)
        .Include(punk => punk.Head)
        .Include(punk => punk.ImplantNodes)
        .Include(punk => punk.Mouth)
        .Include(punk => punk.Type)
        .Include(punk => punk.Offers);

    public Task<PunkzNft> FindById(int id) => GetAll().FirstOrDefaultAsync(nft => nft.Edition == id);

    public Task UpdateMint(List<GetNftsResponse> nfts) => UpdateMint(Context.PunkzNfts, nfts);

    public Task UpdateSales(List<CnftIoListing> listings) => UpdateSales(Context.PunkzNfts, listings, TokenPrefix);
}
