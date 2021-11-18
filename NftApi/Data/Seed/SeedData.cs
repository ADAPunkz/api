using Microsoft.EntityFrameworkCore;
using NftApi.Data.Models;
using System.Text.Json;

namespace NftApi.Data.Seed;

public static class SeedData
{
    public static void InitializePunkz(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

        context.Database.Migrate();

        if (context.PunkzNfts.Any())
        {
            return;
        }

        var path = Path.Combine(Environment.CurrentDirectory, "Data", "Seed", "punkz.json");
        var text = File.ReadAllText(path);
        var enumerable = JsonSerializer.Deserialize<IEnumerable<PunkzNft>>(text, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        foreach (var punkzNft in enumerable)
        {
            if (context.Traits.Find(punkzNft.Accessories.Value) is not Trait accessories)
            {
                context.Traits.Add(punkzNft.Accessories);
            }
            else
            {
                punkzNft.Accessories = accessories;
            }

            if (context.Traits.Find(punkzNft.Background.Value) is not Trait background)
            {
                context.Traits.Add(punkzNft.Background);
            }
            else
            {
                punkzNft.Background = background;
            }

            if (context.Traits.Find(punkzNft.Eyes.Value) is not Trait eyes)
            {
                context.Traits.Add(punkzNft.Eyes);
            }
            else
            {
                punkzNft.Eyes = eyes;
            }

            if (context.Traits.Find(punkzNft.Head.Value) is not Trait head)
            {
                context.Traits.Add(punkzNft.Head);
            }
            else
            {
                punkzNft.Head = head;
            }

            if (context.Traits.Find(punkzNft.ImplantNodes.Value) is not Trait implantNodes)
            {
                context.Traits.Add(punkzNft.ImplantNodes);
            }
            else
            {
                punkzNft.ImplantNodes = implantNodes;
            }

            if (context.Traits.Find(punkzNft.Mouth.Value) is not Trait mouth)
            {
                context.Traits.Add(punkzNft.Mouth);
            }
            else
            {
                punkzNft.Mouth = mouth;
            }

            if (context.Traits.Find(punkzNft.Type.Value) is not Trait type)
            {
                context.Traits.Add(punkzNft.Type);
            }
            else
            {
                punkzNft.Type = type;
            }

            punkzNft.Minted = true;

            context.PunkzNfts.Add(punkzNft);
        }

        context.SaveChanges();
    }
}
