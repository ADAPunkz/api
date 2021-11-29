using NftApi.Data.Models;
using NftApi.Http.Models;

namespace NftApi.Data.Services;

public interface INftManager<T> where T : NftBase
{
    string ProjectName { get; }

    string TokenPrefix { get; }

    IQueryable<T> Query { get; }

    Task<T> FindById(int id);

    Task UpdateMint(List<GetNftsResponse> nfts);

    Task UpdateSales(List<CnftIoListing> listings);

    Task UpdateRarity();
}
