using System.Text.Json;

namespace NftApi.Http.Models.JpgStore;

public class JpgStoreResponse
{
    public JsonElement Cursor { get; set; }

    public bool HasMore { get; set; }

    public List<JpgStoreListing> Nfts { get; set; }
}
