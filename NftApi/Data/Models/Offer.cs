using System.ComponentModel.DataAnnotations;

namespace NftApi.Data.Models;

public class Offer
{
    [Key]
    public Guid Id { get; set; }

    public long Value { get; set; }

    public DateTime Expires { get; set; }
}
