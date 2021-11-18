using System.Text.Json;

namespace NftApi.Http.Services;

public abstract class HttpClientBase
{
    protected static readonly JsonSerializerOptions DefaultSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
}
