using System.Text.Json;
using NftApi.Http.Models.NftMakerPro;

namespace NftApi.Http.Services;

public class NftMakerProClient : HttpClientBase
{
    private const string BaseAddress = "https://api.nft-maker.io";
    private const int PageSize = 50;

    public NftMakerProClient(HttpClient httpClient) : base(httpClient, BaseAddress)
    {
    }

    public async Task<List<NftMakerProNft>> FetchAllNfts(string apiKey, string projectId)
    {
        var page = 1;
        var results = new List<NftMakerProNft>();

        var response = await GetResponse(apiKey, projectId, page);

        while (true)
        {
            if (response is null || response.Count == 0)
            {
                break;
            }

            results.AddRange(response);
            page++;

            response = await GetResponse(apiKey, projectId, page);
        }

        return results;
    }

    private async Task<List<NftMakerProNft>> GetResponse(string apiKey, string projectId, int page)
    {
        var responseMessage = await HttpClient.GetAsync($"/getnfts/{apiKey}/{projectId}/all/{PageSize}/{page}");
        var responseStream = await responseMessage.Content.ReadAsStreamAsync();

        return await JsonSerializer.DeserializeAsync<List<NftMakerProNft>>(responseStream, DefaultSerializerOptions);
    }
}
