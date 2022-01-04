using NftApi.Http.Models;

namespace NftApi.Http.Services;

public abstract class MarketplaceClient : HttpClientBase
{
    protected MarketplaceClient(HttpClient httpClient, string baseAddress) : base(httpClient, baseAddress)
    {
    }

    public abstract string MarketName { get; }

    public abstract Task<List<NormalizedListing>> FetchAllListings(string projectId, string tokenPrefix);
}
