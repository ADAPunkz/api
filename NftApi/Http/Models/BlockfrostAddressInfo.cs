using System.Text.Json.Serialization;

namespace NftApi.Http.Models;

public class BlockfrostAddressInfo
{
    public string Address { get; set; }

    public List<BlockfrostAddressAmount> Amount { get; set; }

    [JsonPropertyName("stake_address")]
    public string StakeAddress { get; set; }

    public string Type { get; set; }

    public bool Script { get; set; }
}
