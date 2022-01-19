namespace NftApi.Http.Models;

public class PoolInfo
{
    public int BlocksMinted { get; set; }

    public int BlocksEpoch { get; set; }

    public int LiveStake { get; set; }

    public decimal LiveSize { get; set; }

    public decimal LiveSaturation { get; set; }

    public int LiveDelegators { get; set; }

    public int ActiveStake { get; set; }

    public decimal ActiveSize { get; set; }

    public int DeclaredPledge { get; set; }

    public int LivePledge { get; set; }

    public decimal MarginCost { get; set; }

    public int FixedCost { get; set; }

}
