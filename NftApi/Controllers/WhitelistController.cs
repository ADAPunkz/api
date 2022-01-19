using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NftApi.Data;
using NftApi.Data.Models;
using NftApi.Http.Models;

namespace NftApi.Controllers;

public class WhitelistController : ApiControllerBase
{
    private readonly DbSet<WhitelistedAddress> _collageWhitelist;

    public WhitelistController(ApplicationDbContext dbContext)
    {
        _collageWhitelist = dbContext.CollageWhitelist;
    }

    [HttpGet("check/collage/{address}")]
    public async Task<ActionResult<WhitelistCheck>> CollageWhitelist(string address)
    {
        var count = await _collageWhitelist.CountAsync(row => row.Value == address);

        return Ok(new WhitelistCheck
        {
            Address = address,
            IsWhitelisted = count > 0
        });
    }
}
