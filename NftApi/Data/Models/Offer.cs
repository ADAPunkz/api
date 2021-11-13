using System.ComponentModel.DataAnnotations;

namespace NftApi.Data.Models;

public class Offer
{
    [Key]
    public virtual Guid Id { get; set; }

    public virtual long Value { get; set; }

    public virtual DateTime Expires { get; set; }
}
