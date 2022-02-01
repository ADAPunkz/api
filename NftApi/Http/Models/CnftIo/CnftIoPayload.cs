namespace NftApi.Http.Models.CnftIo;

public class CnftIoPayload
{
    public int Page { get; set; }

    public string Project { get; set; }

    public bool Sold { get; set; }

    public bool Verified { get; set; }
}
