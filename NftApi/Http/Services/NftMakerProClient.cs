using System.Text.Json;
using NftApi.Http.Models;

namespace NftApi.Http.Services;

public class NftMakerProClient : HttpClientBase
{
    private const int PageSize = 50;

    private readonly HttpClient _httpClient;

    public NftMakerProClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://api.nft-maker.io");
    }

    public async Task<List<GetNftsResponse>> FetchAllNfts(string apiKey, string projectId)
    {
        var page = 1;
        var responseMessage = await _httpClient.GetAsync($"/getnfts/{apiKey}/{projectId}/all/{PageSize}/{page}");

        var response = await JsonSerializer.DeserializeAsync<List<GetNftsResponse>>(responseMessage.Content.ReadAsStream(), DefaultSerializerOptions);
        var results = new List<GetNftsResponse>();

        while (true)
        {
            if (response.Count == 0)
            {
                break;
            }

            results.AddRange(response);
            page++;

            responseMessage = await _httpClient.GetAsync($"/getnfts/{apiKey}/{projectId}/all/{PageSize}/{page}");
            response = await JsonSerializer.DeserializeAsync<List<GetNftsResponse>>(responseMessage.Content.ReadAsStream(), DefaultSerializerOptions);
        }

        return results;
    }
}
