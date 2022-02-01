using System.ComponentModel.DataAnnotations;

namespace NftApi.Data.Models;

public abstract class Trait
{
    [Key]
    public string Value { get; set; }

    public decimal Percent { get; set; }
}

public class PunkzTrait : Trait
{
}

public class CollageTrait : Trait
{
}
