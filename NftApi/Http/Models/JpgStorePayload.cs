using System.Text.Json;
using System.Text.Json.Serialization;

namespace NftApi.Http.Models;

public class JpgStorePayload
{
    public long Cursor { get; set; }

    public JsonDocument Filters { get; set; }

    [JsonPropertyName("sale_type")]
    public string SaleType { get; set; }

    [JsonPropertyName("search_text")]
    public string SearchText { get; set; }

    [JsonPropertyName("sort_by")]
    public string SortBy { get; set; }
}
