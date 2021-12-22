using System.Text.Json.Serialization;

namespace NftApi.Data.Models;

public class PunkzNft : NftBase
{
    public Trait Background { get; set; }

    public Trait Type { get; set; }

    public Trait Mouth { get; set; }

    public Trait Eyes { get; set; }

    [JsonPropertyName("implant_nodes")]
    public Trait ImplantNodes { get; set; }

    public Trait Head { get; set; }

    public Trait Accessories { get; set; }
}
