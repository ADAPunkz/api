using NftApi.Data.Models;
using NftApi.Http.Models;
using NftApi.Http.Models.NftMakerPro;

namespace NftApi.Data.Services;

public interface INftManager<T> where T : NftBase
{
    string ProjectName { get; }

    string TokenPrefix { get; }

    IQueryable<T> Query { get; }

    Task<T> FindById(int id);

    Task UpdateMint(List<NftMakerProNft> nfts);

    Task UpdateSales(List<NormalizedListing> listings, string marketName, bool preferredMarket = false);

    Task UpdateRarity();

    Task<bool> CheckWhitelist(string address);
}
