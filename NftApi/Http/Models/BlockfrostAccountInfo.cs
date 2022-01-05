using System.Text.Json.Serialization;

namespace NftApi.Http.Models;

public class BlockfrostAccountInfo
{
    [JsonPropertyName("stake_address")]
    public string StakeAddress { get; set; }

    [JsonPropertyName("controlled_amount")]
    public string ControlledAmount { get; set; }

    public AccountBalance ToAccountBalance()
    {
        var lovelace = long.Parse(ControlledAmount);

        return new AccountBalance
        {
            StakeAddress = StakeAddress,
            Balance = lovelace / 1000000
        };
    }
}
