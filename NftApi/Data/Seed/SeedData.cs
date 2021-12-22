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
            if (await context.Traits.FindAsync(punkzNft.Accessories.Value) is not { } accessories)
            {
                context.Traits.Add(punkzNft.Accessories);
            }
            else
            {
                punkzNft.Accessories = accessories;
            }

            if (await context.Traits.FindAsync(punkzNft.Background.Value) is not { } background)
            {
                context.Traits.Add(punkzNft.Background);
            }
            else
            {
                punkzNft.Background = background;
            }

            if (await context.Traits.FindAsync(punkzNft.Eyes.Value) is not { } eyes)
            {
                context.Traits.Add(punkzNft.Eyes);
            }
            else
            {
                punkzNft.Eyes = eyes;
            }

            if (await context.Traits.FindAsync(punkzNft.Head.Value) is not { } head)
            {
                context.Traits.Add(punkzNft.Head);
            }
            else
            {
                punkzNft.Head = head;
            }

            if (await context.Traits.FindAsync(punkzNft.ImplantNodes.Value) is not { } implantNodes)
            {
                context.Traits.Add(punkzNft.ImplantNodes);
            }
            else
            {
                punkzNft.ImplantNodes = implantNodes;
            }

            if (await context.Traits.FindAsync(punkzNft.Mouth.Value) is not { } mouth)
            {
                context.Traits.Add(punkzNft.Mouth);
            }
            else
            {
                punkzNft.Mouth = mouth;
            }

            if (await context.Traits.FindAsync(punkzNft.Type.Value) is not { } type)
            {
                context.Traits.Add(punkzNft.Type);
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
}
