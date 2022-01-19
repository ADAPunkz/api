using Microsoft.AspNetCore.Mvc;
using NftApi.Http.Models;
using NftApi.Http.Services;

namespace NftApi.Controllers;

public class PoolController : ApiControllerBase
{
    private readonly BlockfrostClient _blockfrostClient;
    private readonly IConfiguration _configuration;

    public PoolController(
        BlockfrostClient blockfrostClient,
        IConfiguration configuration)
    {
        _configuration = configuration;
        _blockfrostClient = blockfrostClient;
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<PoolInfo>> Info()
    {
        var poolId = _configuration["PoolId"];
        var apiKey = _configuration["BlockfrostApiKey"];

        var result = await _blockfrostClient.FetchPoolInfo(poolId, apiKey);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result.ToPoolInfo());
    }
}
