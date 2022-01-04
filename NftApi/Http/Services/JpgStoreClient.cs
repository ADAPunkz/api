using System.Net.Mime;
using System.Text;
using System.Text.Json;
using NftApi.Http.Models;

namespace NftApi.Http.Services;

public class JpgStoreClient : MarketplaceClient
{
    private const string BaseAddress = "https://www.jpg.store";

    public JpgStoreClient(HttpClient httpClient) : base(httpClient, BaseAddress)
    {
    }

    public override string MarketName => "jpg.store";

    public override async Task<List<NormalizedListing>> FetchAllListings(string projectName, string tokenPrefix)
    {
        var listings = new List<JpgStoreListing>();

        JpgStorePayload payload = new JpgStorePayload
        {
            SaleType = "buy-now"
        };

        projectName = projectName.ToLower();

        var response = await GetResponse(payload, projectName);

        while (true)
        {
            if (response?.Nfts is null)
            {
                break;
            }

            if (response.Nfts.Count == 0)
            {
                break;
            }

            listings.AddRange(response.Nfts);

            payload.Cursor = response.Cursor;

            response = await GetResponse(payload, projectName);
        }

        return listings.Select(listing => listing.Normalize(tokenPrefix)).ToList();
    }

    private async Task<JpgStoreResponse> GetResponse(JpgStorePayload payload, string projectName)
    {
        var responseMessage = await HttpClient.PostAsync($"/api/collection/{projectName}/assets?is_scraped=true",
            new StringContent(
                JsonSerializer.Serialize(payload, DefaultSerializerOptions),
                Encoding.UTF8,
                MediaTypeNames.Application.Json));

        var responseStream = await responseMessage.Content.ReadAsStreamAsync();

        return await JsonSerializer.DeserializeAsync<JpgStoreResponse>(responseStream, DefaultSerializerOptions);
    } 
}
