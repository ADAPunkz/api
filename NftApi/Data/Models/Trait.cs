using System.ComponentModel.DataAnnotations;

namespace NftApi.Data.Models;

public class Trait
{
    [Key]
    public string Value { get; set; }

    public decimal Percent { get; set; }
}
