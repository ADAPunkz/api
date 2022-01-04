using System.Text.Json;
using NftApi.Data.Models;

namespace NftApi.Http.Models;

public class CnftIoOffer
{
    public long Offer { get; set; }

    // sometimes this is returned as a long UNIX time, sometimes a string ISO date
    public JsonElement Expires { get; set; }

    public Offer Normalize()
    {
        //TODO should probably keep value in lovelace and convert on client
        return new Offer
        {
            Value = Offer / 1000000,
            Expires = Expires.ValueKind == JsonValueKind.String
                ? DateTime.Parse(Expires.GetString() ?? string.Empty)
                : DateTimeOffset.FromUnixTimeMilliseconds(Expires.GetInt64()).UtcDateTime
        };
    }
}
