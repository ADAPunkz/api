using NftApi.Data.Models;
using NftApi.Http.Models;

namespace NftApi.Data.Services;

public interface INftManager<T> where T : NftBase
{
    string ProjectName { get; }

    string TokenPrefix { get; }

    Task<T> FindById(int id);

    IQueryable<T> GetAll();

    Task UpdateMint(List<GetNftsResponse> nfts);

    Task UpdateRarity();

    Task UpdateSales(List<CnftIoListing> listings);
}
