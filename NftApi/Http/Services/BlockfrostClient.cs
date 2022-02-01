using System.Text.Json;
using NftApi.Http.Models;

namespace NftApi.Http.Services;

public class BlockfrostClient : HttpClientBase
{
    public const string LovelaceAmountId = "lovelace";

    private const string BaseAddress = "https://cardano-mainnet.blockfrost.io";

    public BlockfrostClient(HttpClient httpClient) : base(httpClient, BaseAddress)
    {
    }

    public async Task<BlockfrostAccountInfo> FetchAccountInfo(string stakeAddress, string apiKey)
    {
        var responseMessage = await GetResponse($"/api/v0/accounts/{stakeAddress}", apiKey);
        var responseStream = await responseMessage.Content.ReadAsStreamAsync();

        return await JsonSerializer.DeserializeAsync<BlockfrostAccountInfo>(responseStream, DefaultSerializerOptions);
    }

    public async Task<BlockfrostPoolInfo> FetchPoolInfo(string poolId, string apiKey)
    {
        var responseMessage = await GetResponse($"/api/v0/pools/{poolId}", apiKey);
        var responseStream = await responseMessage.Content.ReadAsStreamAsync();

        return await JsonSerializer.DeserializeAsync<BlockfrostPoolInfo>(responseStream, DefaultSerializerOptions);
    }

    public Task<HttpResponseMessage> GetResponse(string endpoint, string apiKey)
    {
        var requestMessage = new HttpRequestMessage
        {
            RequestUri = new Uri($"{HttpClient.BaseAddress}{endpoint}"),
            Method = HttpMethod.Get
        };

        requestMessage.Headers.Add("project_id", apiKey);

        return HttpClient.SendAsync(requestMessage);
    }
}
