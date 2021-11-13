using System.Text.Json.Serialization;

namespace NftApi.Data.Models;

public class PunkzNft : NftBase
{
    public virtual Trait Background { get; set; }

    public virtual Trait Type { get; set; }

    public virtual Trait Mouth { get; set; }

    public virtual Trait Eyes { get; set; }

    [JsonPropertyName("implant_nodes")]
    public virtual Trait ImplantNodes { get; set; }

    public virtual Trait Head { get; set; }

    public virtual Trait Accessories { get; set; }
}
