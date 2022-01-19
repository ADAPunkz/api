using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using NftApi.Data.Models;

namespace NftApi.Data.Seed;

public static class SeedData
{
    public static async Task InitializePunkz(IServiceProvider serviceProvider)
    {
        await using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

        await context.Database.MigrateAsync();

        if (context.PunkzNfts.Any())
        {
            return;
        }

        var path = Path.Combine(Environment.CurrentDirectory, "Data", "Seed", "punkz.json");
        var text = await File.ReadAllTextAsync(path);
        var enumerable = JsonSerializer.Deserialize<IEnumerable<PunkzNft>>(text, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (enumerable is null)
        {
            return;
        }

        foreach (var punkzNft in enumerable)
        {
            if (await context.PunkzTraits.FindAsync(punkzNft.Accessories.Value) is not { } accessories)
            {
                context.PunkzTraits.Add(punkzNft.Accessories);
            }
            else
            {
                punkzNft.Accessories = accessories;
            }

            if (await context.PunkzTraits.FindAsync(punkzNft.Background.Value) is not { } background)
            {
                context.PunkzTraits.Add(punkzNft.Background);
            }
            else
            {
                punkzNft.Background = background;
            }

            if (await context.PunkzTraits.FindAsync(punkzNft.Eyes.Value) is not { } eyes)
            {
                context.PunkzTraits.Add(punkzNft.Eyes);
            }
            else
            {
                punkzNft.Eyes = eyes;
            }

            if (await context.PunkzTraits.FindAsync(punkzNft.Head.Value) is not { } head)
            {
                context.PunkzTraits.Add(punkzNft.Head);
            }
            else
            {
                punkzNft.Head = head;
            }

            if (await context.PunkzTraits.FindAsync(punkzNft.ImplantNodes.Value) is not { } implantNodes)
            {
                context.PunkzTraits.Add(punkzNft.ImplantNodes);
            }
            else
            {
                punkzNft.ImplantNodes = implantNodes;
            }

            if (await context.PunkzTraits.FindAsync(punkzNft.Mouth.Value) is not { } mouth)
            {
                context.PunkzTraits.Add(punkzNft.Mouth);
            }
            else
            {
                punkzNft.Mouth = mouth;
            }

            if (await context.PunkzTraits.FindAsync(punkzNft.Type.Value) is not { } type)
            {
                context.PunkzTraits.Add(punkzNft.Type);
            }
            else
            {
                punkzNft.Type = type;
            }

            // the whole collection is already minted
            punkzNft.Minted = true;

            context.PunkzNfts.Add(punkzNft);
        }

        await context.SaveChangesAsync();
    }

    public static async Task InitializeCollage(IServiceProvider serviceProvider)
    {
        await using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

        await context.Database.MigrateAsync();

        if (context.CollageNfts.Any())
        {
            return;
        }

        var path = Path.Combine(Environment.CurrentDirectory, "Data", "Seed", "collages.json");
        var text = await File.ReadAllTextAsync(path);
        var enumerable = JsonSerializer.Deserialize<IEnumerable<CollageNft>>(text, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (enumerable is null)
        {
            return;
        }

        foreach (var collageNft in enumerable)
        {
            if (await context.CollageTraits.FindAsync(collageNft.Frame.Value) is not { } frame)
            {
                context.CollageTraits.Add(collageNft.Frame);
            }
            else
            {
                collageNft.Frame = frame;
            }

            if (await context.CollageTraits.FindAsync(collageNft.Type.Value) is not { } type)
            {
                context.CollageTraits.Add(collageNft.Type);
            }
            else
            {
                collageNft.Type = type;
            }

            context.CollageNfts.Add(collageNft);
        }

        await context.SaveChangesAsync();
    }

    public static async Task InitializeCollageWhitelist(IServiceProvider serviceProvider)
    {
        await using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

        await context.Database.MigrateAsync();

        var path = Path.Combine(Environment.CurrentDirectory, "Data", "Seed", "collage.whitelist.json");
        var text = await File.ReadAllTextAsync(path);
        var enumerable = JsonSerializer.Deserialize<IEnumerable<WhitelistedAddress>>(text, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (enumerable is null)
        {
            return;
        }

        foreach (var whiteListedAddress in enumerable)
        {
            if (await context.CollageWhitelist.FindAsync(whiteListedAddress.Value) is not { })
            {
                context.CollageWhitelist.Add(whiteListedAddress);
            }
        }

        await context.SaveChangesAsync();
    }
}
