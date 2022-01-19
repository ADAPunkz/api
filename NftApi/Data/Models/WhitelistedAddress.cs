using System.ComponentModel.DataAnnotations;

namespace NftApi.Data.Models;

public class WhitelistedAddress
{
    [Key]
    public string Value { get; set; }
}
