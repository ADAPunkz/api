using System.Net.Mime;
using System.Text;
using System.Text.Json;
using NftApi.Http.Models;
using NftApi.Http.Models.CnftIo;

namespace NftApi.Http.Services;

public class CnftIoClient : MarketplaceClient
{
    private const string BaseAddress = "https://api.cnft.io";

    public CnftIoClient(HttpClient httpClient) : base(httpClient, BaseAddress)
    {
    }

    public override string MarketName => "CNFT.IO";

    public override async Task<List<NormalizedListing>> FetchAllListings(string projectName, string tokenPrefix)
    {
        var listings = new List<CnftIoListing>();
        var payload = new CnftIoPayload
        {
            Page = 1, Sold = false, Verified = true, Project = projectName
        };

        var response = await GetResponse(payload);

        while (true)
        {
            if (response is null)
            {
                break;
            }

            if (response.Results.Count == 0)
            {
                break;
            }

            listings.AddRange(response.Results);
            payload.Page++;

            response = await GetResponse(payload);
        }

        return listings.Select(listing => listing.Normalize(tokenPrefix)).ToList();
    }

    private async Task<CnftIoResponse> GetResponse(CnftIoPayload payload)
    {
        var responseMessage = await HttpClient.PostAsync("/market/listings",
            new StringContent(
                JsonSerializer.Serialize(payload, DefaultSerializerOptions),
                Encoding.UTF8,
                MediaTypeNames.Application.Json));

        var responseStream = await responseMessage.Content.ReadAsStreamAsync();

        return await JsonSerializer.DeserializeAsync<CnftIoResponse>(responseStream, DefaultSerializerOptions);
    }
}
