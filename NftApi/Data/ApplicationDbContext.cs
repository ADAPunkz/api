using Microsoft.EntityFrameworkCore;
using NftApi.Data.Models;

namespace NftApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public virtual DbSet<PunkzNft> PunkzNfts { get; set; }

    public virtual DbSet<Trait> Traits { get; set; }

    public virtual DbSet<Offer> Offers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
