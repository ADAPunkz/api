using NftApi.Data.Models;
using NftApi.Http.Models;

namespace NftApi.Data.Services;

public interface INftManager<T> where T : NftBase
{
    Task<T> FindById(int id);

    IQueryable<T> GetAll();

    Task UpdateMint(List<GetNftsResponse> nfts);

    Task UpdateSales(List<CnftIoListing> listings);
}
