using Microsoft.AspNetCore.Mvc;
using NftApi.Http.Models;
using NftApi.Http.Models.Blockfrost;
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
        var addresses = _configuration["ChestStakeAddress"].Split(',');
        var apiKey = _configuration["BlockfrostApiKey"];

        var requests = new List<Task<BlockfrostAccountInfo>>();

        foreach (var address in addresses)
        {
            requests.Add(_blockfrostClient.FetchAccountInfo(address, apiKey));
        }

        var results = await Task.WhenAll(requests);
        var balance = new AccountBalance();

        foreach (var result in results)
        {
            balance.Balance += result.ToAccountBalance().Balance;
        }

        return Ok(balance);
    }
}
