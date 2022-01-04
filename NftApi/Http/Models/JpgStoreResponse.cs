namespace NftApi.Http.Models;

public class JpgStoreResponse
{
    public long Cursor { get; set; }

    public List<JpgStoreListing> Nfts { get; set; }
}
