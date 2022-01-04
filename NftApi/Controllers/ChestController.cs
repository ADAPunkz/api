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
    public async Task<ActionResult<AddressBalance>> Balance()
    {
        var address = _configuration["CommunityChestAddress"];
        var apiKey = _configuration["BlockfrostApiKey"];

        var result = await _blockfrostClient.FetchAddressInfo(address, apiKey);

        var lovelaceAmount = result.Amount.FirstOrDefault(amount => amount.Unit == BlockfrostClient.LovelaceAmountId);

        if (lovelaceAmount is null)
        {
            return NotFound();
        }

        return Ok(new AddressBalance
        {
            Address = address, Balance = long.Parse(lovelaceAmount.Quantity) / 1000000
        });
    }
}
