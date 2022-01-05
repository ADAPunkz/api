using Microsoft.AspNetCore.Mvc;
using NftApi.Http.Models;
using NftApi.Http.Services;

namespace NftApi.Controllers;

public class ChestController : ApiControllerBase
{
    private readonly BlockfrostClient _blockfrostClient;
    private readonly IConfiguration _configuration;

    public ChestController(
        BlockfrostClient blockfrostClient,
        IConfiguration configuration)
    {
        _configuration = configuration;
        _blockfrostClient = blockfrostClient;
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<AccountBalance>> Balance()
    {
        var address = _configuration["ChestStakeAddress"];
        var apiKey = _configuration["BlockfrostApiKey"];

        var result = await _blockfrostClient.FetchAccountInfo(address, apiKey);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result.ToAccountBalance());
    }
}
