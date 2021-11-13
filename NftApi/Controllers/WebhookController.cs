using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NftApi.Data;
using NftApi.Data.Models;
using NftApi.Http.Models;
using NftApi.Http.Services;

namespace NftApi.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class WebhookController : ApiControllerBase
{
    private readonly ILogger<WebhookController> _logger;
    private readonly CnftIoClient _cnftIoClient;

    public WebhookController(ApplicationDbContext dbContext, CnftIoClient cnftIoClient, ILogger<WebhookController> logger) : base(dbContext)
    {
        _cnftIoClient = cnftIoClient;
        _logger = logger;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Sales()
    {
        var results = await _cnftIoClient.FetchAllListings(new CnftIoPayload
        {
            Page = 1,
            Project = "ADAPunkz",
            Sold = false,
            Verified = true
        });

        var processedIds = new List<int>();

        foreach (var result in results)
        {
            var name = result.Asset.AssetId["ADAPunk".Length..];
            var edition = int.Parse(name);

            var punk = await DbContext.PunkzNfts.Include(punk => punk.Offers).FirstOrDefaultAsync(punk => punk.Edition == edition);

            if (punk.Offers is not null && punk.Offers.Count > 0)
            {
                DbContext.Offers.RemoveRange(punk.Offers);
            }

            punk.Minted = true;
            punk.OnSale = true;
            punk.SalePrice = (int)(result.Price / 1000000);
            punk.MarketId = result.Id;
            punk.ListedAt = result.CreatedAt;
            punk.IsAuction = result.IsAuction;
            punk.Offers = result.Offers.Select(offer => offer.Normalize()).ToList();

            DbContext.Update(punk);
            processedIds.Add(edition);
        }

        var offMarketNfts = await DbContext.PunkzNfts
            .Include(punk => punk.Offers)
            .Where(punk => !processedIds.Contains(punk.Edition))
            .ToListAsync();

        foreach (var punk in offMarketNfts)
        {
            if (punk.Offers is not null && punk.Offers.Count > 0)
            {
                DbContext.Offers.RemoveRange(punk.Offers);
            }

            punk.Minted = true;
            punk.OnSale = false;
            punk.SalePrice = 0;
            punk.MarketId = null;
            punk.ListedAt = null;
            punk.IsAuction = false;
            punk.Offers = new List<Offer>();

            DbContext.Update(punk);
        }

        await DbContext.SaveChangesAsync();

        return StatusCode(200);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Mint()
    {
        throw new NotImplementedException();
    }
}
