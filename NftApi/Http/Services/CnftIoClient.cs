using System.Net.Mime;
using System.Text;
using System.Text.Json;
using NftApi.Http.Models;

namespace NftApi.Http.Services;

public class CnftIoClient : HttpClientBase
{
    private const string BaseAddress = "https://api.cnft.io";

    public CnftIoClient(HttpClient httpClient) : base(httpClient, BaseAddress)
    {
    }

    public async Task<List<CnftIoListing>> FetchAllListings(string projectName)
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

        return listings;
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
