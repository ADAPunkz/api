using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NftApi.Data;
using NftApi.Extensions;
using NftApi.Http.Models;
using NftApi.Data.Models;
using System.ComponentModel;

namespace NftApi.Controllers;

public class PunkzController : ApiControllerBase
{
    private const string Edition = "edition";
    private const string Rank = "rank";
    private const string Price = "price";
    private const string ListedAt = "listedAt";
    private const string OfferCount = "offerCount";

    private const string Descending = "desc";

    public PunkzController(ApplicationDbContext dbContext) : base(dbContext)
    { }

    private IQueryable<PunkzNft> Punkz => DbContext.PunkzNfts
        .AsQueryable()
        .Include(punk => punk.Accessories)
        .Include(punk => punk.Background)
        .Include(punk => punk.Eyes)
        .Include(punk => punk.Head)
        .Include(punk => punk.ImplantNodes)
        .Include(punk => punk.Mouth)
        .Include(punk => punk.Type)
        .Include(punk => punk.Offers);

    [HttpGet("{id}")]
    public async Task<ActionResult<PunkzNft>> Get(int id)
    {
        var punk = await Punkz.FirstOrDefaultAsync(punk => punk.Edition == id);

        if (punk is null)
        {
            return NotFound();
        }

        return Ok(punk);
    }

    [HttpGet]
    public async Task<ActionResult<NftList<PunkzNft>>> Get(
        string background,
        string type,
        string mouth,
        string eyes,
        string implant_nodes,
        string head,
        string accessories,
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
        var nfts = Punkz
            .WhereIf(!string.IsNullOrEmpty(background), punk => punk.Background.Value.Trim().ToLower().Replace(" ", "_") == NormalizeTrait(background))
            .WhereIf(!string.IsNullOrEmpty(type), punk => punk.Type.Value.Trim().ToLower().Replace(" ", "_") == NormalizeTrait(type))
            .WhereIf(!string.IsNullOrEmpty(mouth), punk => punk.Mouth.Value.Trim().ToLower().Replace(" ", "_") == NormalizeTrait(mouth))
            .WhereIf(!string.IsNullOrEmpty(eyes), punk => punk.Eyes.Value.Trim().ToLower().Replace(" ", "_") == NormalizeTrait(eyes))
            .WhereIf(!string.IsNullOrEmpty(implant_nodes), punk => punk.ImplantNodes.Value.Trim().ToLower().Replace(" ", "_") == NormalizeTrait(implant_nodes))
            .WhereIf(!string.IsNullOrEmpty(head), punk => punk.Head.Value.Trim().ToLower().Replace(" ", "_") == NormalizeTrait(head))
            .WhereIf(!string.IsNullOrEmpty(accessories), punk => punk.Accessories.Value.Trim().ToLower().Replace(" ", "_") == NormalizeTrait(accessories))
            .WhereIf(minRank > 0, punk => punk.Rank >= minRank)
            .WhereIf(maxRank > 0, punk => punk.Rank <= maxRank)
            .WhereIf(onSale.HasValue, punk => punk.OnSale == onSale.Value)
            .WhereIf(isAuction.HasValue, punk => punk.IsAuction == isAuction.Value)
            .WhereIf(minPrice > 0, punk => punk.SalePrice >= minPrice)
            .WhereIf(maxPrice > 0, punk => punk.SalePrice <= maxPrice)
            .OrderByEditionIf(sort == Edition, sortDirection)
            .OrderByRankIf(sort == Rank, sortDirection)
            .OrderByPriceIf(sort == Price, sortDirection)
            .OrderByListedAtIf(sort == ListedAt, sortDirection)
            .OrderByOfferCountIf(sort == OfferCount, sortDirection);

        var count = await nfts.CountAsync();
        var items = await nfts
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(new NftList<PunkzNft>
        {
            Results = items,
            ResultsCount = count,
            PageSize = pageSize,
            NextPage = ++page
        });
    }

    private static string NormalizeTrait(string trait) => trait.Trim().ToLower().Replace(" ", "_");
}
