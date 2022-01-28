using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NftApi.Data.Models;
using NftApi.Data.Services;
using NftApi.Extensions;
using NftApi.Http.Models;

namespace NftApi.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class CollageController : ApiControllerBase
{
    private readonly IConfiguration _configuration;

    private readonly CollageManager _collageManager;

    public CollageController(
        IConfiguration configuration,
        CollageManager collageManager)
    {
        _configuration = configuration;
        _collageManager = collageManager;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CollageNft>> Get(int id)
    {
        var collage = await _collageManager.FindById(id);

        if (collage is null)
        {
            return NotFound();
        }

        return Ok(collage);
    }

    [HttpGet]
    public async Task<ActionResult<NftList<CollageNft>>> Get(
        string frame,
        string type,
        string sort,
        string direction,
        int minRank,
        int maxRank,
        bool? onSale,
        bool? isAuction,
        int minPrice,
        int maxPrice,
        int page = 1,
        int pageSize = 20)
    {
        var sortDirection = direction == Descending ? ListSortDirection.Descending : ListSortDirection.Ascending;
        var nfts = _collageManager.Query
            .WhereIf(!string.IsNullOrEmpty(frame), collage => collage.Tier.Value.Trim().ToLower().Replace(" ", "_") == NormalizeTrait(frame))
            .WhereIf(!string.IsNullOrEmpty(type), collage => collage.Type.Value.Trim().ToLower().Replace(" ", "_") == NormalizeTrait(type))
            .WhereIf(minRank > 0, collage => collage.Rank >= minRank)
            .WhereIf(maxRank > 0, collage => collage.Rank <= maxRank)
            .WhereIf(onSale.HasValue, collage => collage.OnSale == onSale.Value)
            .WhereIf(isAuction.HasValue, collage => collage.IsAuction == isAuction.Value)
            .WhereIf(minPrice > 0, collage => collage.SalePrice >= minPrice)
            .WhereIf(maxPrice > 0, collage => collage.SalePrice <= maxPrice);

        var count = await nfts.CountAsync();
        var items = await nfts
            .OrderByEditionIf(sort == Edition, sortDirection)
            .OrderByRankIf(sort == Rank, sortDirection)
            .OrderByPriceIf(sort == Price, sortDirection)
            .OrderByListedAtIf(sort == ListedAt, sortDirection)
            .OrderByOfferCountIf(sort == OfferCount, sortDirection)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Cast<CollageNft>()
            .ToListAsync();

        return Ok(new NftList<CollageNft>
        {
            Results = items,
            ResultsCount = count,
            PageSize = pageSize,
            NextPage = ++page
        });
    }

    [HttpGet("mint/whitelist/{address}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<ActionResult<WhitelistCheck>> MintWhitelist(string address)
    {
        var isWhitelisted = await _collageManager.CheckWhitelist(address);

        return Ok(new WhitelistCheck
        {
            Address = address,
            IsWhitelisted = isWhitelisted
        });
    }

    [HttpGet("mint/address")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public ActionResult<MintingAddress> MintAddress(string r)
    {
        var mintLaunch = new DateTime(2022, 1, 28, 19, 00, 00, DateTimeKind.Utc);

        if (DateTime.UtcNow < mintLaunch && string.IsNullOrEmpty(r))
        {
            return Ok(new MintingAddress
            {
                IsActive = false
            });
        }

        var address = _configuration["CollageMintAddress"];

        return Ok(new MintingAddress
        {
            Address = address,
            IsActive = true
        });
    }
}
