using System.Text.Json;

namespace NftApi.Http.Services;

public abstract class HttpClientBase
{
    protected static readonly JsonSerializerOptions DefaultSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    protected HttpClientBase(HttpClient httpClient, string baseAddress)
    {
        HttpClient = httpClient;
        HttpClient.BaseAddress = new Uri(baseAddress);
    }

    protected HttpClient HttpClient { get; }
}
