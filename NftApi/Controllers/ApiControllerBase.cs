using Microsoft.AspNetCore.Mvc;

namespace NftApi.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    protected const string Edition = "edition";
    protected const string Rank = "rank";
    protected const string Price = "price";
    protected const string ListedAt = "listedAt";
    protected const string MintedAt = "mintedAt";
    protected const string OfferCount = "offerCount";
    protected const string Descending = "desc";

    protected static string NormalizeTrait(string trait) => trait.Trim().ToLower().Replace(" ", "_");
}
