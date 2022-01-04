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

    public async Task<BlockfrostAddressInfo> FetchAddressInfo(string address, string apiKey)
    {
        HttpClient.DefaultRequestHeaders.Add("project_id", apiKey);

        var responseMessage = await HttpClient.GetAsync($"/api/v0/addresses/{address}");
        var responseStream = await responseMessage.Content.ReadAsStreamAsync();

        return await JsonSerializer.DeserializeAsync<BlockfrostAddressInfo>(responseStream, DefaultSerializerOptions);
    }
}
