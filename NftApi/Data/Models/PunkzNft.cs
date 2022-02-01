using System.Text.Json.Serialization;

namespace NftApi.Data.Models;

public class PunkzNft : NftBase
{
    public PunkzTrait Background { get; set; }

    public PunkzTrait Type { get; set; }

    public PunkzTrait Mouth { get; set; }

    public PunkzTrait Eyes { get; set; }

    [JsonPropertyName("implant_nodes")]
    public PunkzTrait ImplantNodes { get; set; }

    public PunkzTrait Head { get; set; }

    public PunkzTrait Accessories { get; set; }
}
