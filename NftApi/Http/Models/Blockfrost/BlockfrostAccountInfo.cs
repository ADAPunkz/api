using System.Text.Json.Serialization;
using NftApi.Extensions;

namespace NftApi.Http.Models.Blockfrost;

public class BlockfrostAccountInfo
{
    [JsonPropertyName("stake_address")]
    public string StakeAddress { get; set; }

    [JsonPropertyName("controlled_amount")]
    public string ControlledAmount { get; set; }

    public AccountBalance ToAccountBalance()
    {
        return new AccountBalance
        {
            StakeAddress = StakeAddress,
            Balance = ControlledAmount.ToAda()
        };
    }
}
