using System.Text.Json.Serialization;
using NftApi.Extensions;

namespace NftApi.Http.Models.Blockfrost;

public class BlockfrostPoolInfo
{
    [JsonPropertyName("blocks_minted")]
    public int BlocksMinted { get; set; }

    [JsonPropertyName("blocks_epoch")]
    public int BlocksEpoch { get; set; }

    [JsonPropertyName("live_stake")]
    public string LiveStake { get; set; }

    [JsonPropertyName("live_size")]
    public decimal LiveSize { get; set; }

    [JsonPropertyName("live_saturation")]
    public decimal LiveSaturation { get; set; }

    [JsonPropertyName("live_delegators")]
    public int LiveDelegators { get; set; }

    [JsonPropertyName("active_stake")]
    public string ActiveStake { get; set; }

    [JsonPropertyName("active_size")]
    public decimal ActiveSize { get; set; }

    [JsonPropertyName("declared_pledge")]
    public string DeclaredPledge { get; set; }

    [JsonPropertyName("live_pledge")]
    public string LivePledge { get; set; }

    [JsonPropertyName("margin_cost")]
    public decimal MarginCost { get; set; }

    [JsonPropertyName("fixed_cost")]
    public string FixedCost { get; set; }

    public PoolInfo ToPoolInfo()
    {
        return new PoolInfo
        {
            BlocksMinted = BlocksMinted,
            BlocksEpoch = BlocksEpoch,
            LiveStake = LiveStake.ToAda(),
            LiveSize = LiveSize,
            LiveSaturation = LiveSaturation,
            LiveDelegators = LiveDelegators,
            ActiveStake = ActiveStake.ToAda(),
            ActiveSize = ActiveSize,
            DeclaredPledge = DeclaredPledge.ToAda(),
            LivePledge = LivePledge.ToAda(),
            MarginCost = MarginCost,
            FixedCost = FixedCost.ToAda()
        };
    }
}
