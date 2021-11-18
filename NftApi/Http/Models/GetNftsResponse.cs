namespace NftApi.Http.Models;

public class GetNftsResponse
{
    public string Name { get; set; }

    public string IpfsLink { get; set; }

    public string GatewayLink { get; set; }

    public bool Minted { get; set; }
}
