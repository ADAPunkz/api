using Microsoft.EntityFrameworkCore;
using NftApi.Data.Models;

namespace NftApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public virtual DbSet<PunkzNft> PunkzNfts { get; set; }

    public virtual DbSet<CollageNft> CollageNfts { get; set; }

    public virtual DbSet<PunkzTrait> PunkzTraits { get; set; }

    public virtual DbSet<CollageTrait> CollageTraits { get; set; }

    public virtual DbSet<Offer> Offers { get; set; }

    public virtual DbSet<WhitelistedAddress> CollageWhitelist { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
