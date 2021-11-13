namespace NftApi.Http.Models;

public class CnftIoPayload
{
    public int Page { get; set; }

    public string Project { get; set; }

    public bool Sold { get; set; }

    public bool Verified { get; set; }
}
