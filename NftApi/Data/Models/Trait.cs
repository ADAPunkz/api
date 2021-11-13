using System.ComponentModel.DataAnnotations;

namespace NftApi.Data.Models;

public class Trait
{
    [Key]
    public virtual string Value { get; set; }

    public virtual decimal Percent { get; set; }
}
