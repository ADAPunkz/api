using Microsoft.AspNetCore.Mvc;
using NftApi.Data.Services;
using NftApi.Http.Services;

namespace NftApi.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class WebhookController : ApiControllerBase
{
    private readonly IConfiguration _configuration;

    private readonly PunkzManager _punkzManager;

    private readonly CnftIoClient _cnftIoClient;
    private readonly NftMakerProClient _nftMakerProClient;

    public WebhookController(
        IConfiguration configuration,
        PunkzManager punkzManager,
        CnftIoClient cnftIoClient,
        NftMakerProClient nftMakerProClient)
    {
        _configuration = configuration;
        _punkzManager = punkzManager;
        _cnftIoClient = cnftIoClient;
        _nftMakerProClient = nftMakerProClient;
    }

    [HttpGet("[action]/{id}")]
    public async Task<StatusCodeResult> Sales(string id)
    {
        switch (id)
        {
            case "punkz":
                var listings = await _cnftIoClient.FetchAllListings("ADAPunkz");
                await _punkzManager.UpdateSales(listings);
                break;
        }

        return StatusCode(200);
    }

    [HttpGet("[action]/{id}")]
    public async Task<StatusCodeResult> Mint(string id)
    {
        var apiKey = _configuration[$"NftMakerPro:{id}:ApiKey"];
        var projectId = _configuration[$"NftMakerPro:{id}:ProjectId"];

        var results = await _nftMakerProClient.FetchAllNfts(apiKey, projectId);

        switch (id)
        {
            case "punkz":
                await _punkzManager.UpdateMint(results);
                break;
        }

        return StatusCode(200);
    }
}
