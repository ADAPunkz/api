namespace NftApi.Http.Models.NftMakerPro;

public class NftMakerProNft
{
    public string Name { get; set; }

    public string IpfsLink { get; set; }

    public string GatewayLink { get; set; }

    public bool Minted { get; set; }

    public DateTime? Selldate { get; set; }
}
