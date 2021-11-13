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
        var offer = new Offer
        {
            Value = Offer / 1000000
        };

        // normalize the inconsistent value to a DateTime for the DB
        if (Expires.ValueKind == JsonValueKind.String)
        {
            offer.Expires = DateTime.Parse(Expires.GetString());
        }
        else
        {
            offer.Expires = DateTimeOffset.FromUnixTimeMilliseconds(Expires.GetInt64()).UtcDateTime;
        }

        return offer;
    }
}
