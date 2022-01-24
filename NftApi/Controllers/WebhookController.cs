using Microsoft.AspNetCore.Mvc;
using NftApi.Data.Services;
using NftApi.Http.Services;

namespace NftApi.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class WebhookController : ApiControllerBase
{
    private readonly IConfiguration _configuration;

    private readonly PunkzManager _punkzManager;
    private readonly CollageManager _collageManager;
 
    private readonly CnftIoClient _cnftIoClient;
    private readonly JpgStoreClient _jpgStoreClient;
    private readonly NftMakerProClient _nftMakerProClient;

    public WebhookController(
        IConfiguration configuration,
        PunkzManager punkzManager,
        CollageManager collageManager,
        CnftIoClient cnftIoClient,
        JpgStoreClient jpgStoreClient,
        NftMakerProClient nftMakerProClient)
    {
        _configuration = configuration;
        _punkzManager = punkzManager;
        _collageManager = collageManager;
        _cnftIoClient = cnftIoClient;
        _jpgStoreClient = jpgStoreClient;
        _nftMakerProClient = nftMakerProClient;
    }

    [HttpGet("sales/punkz")]
    public async Task<StatusCodeResult> PunkzSales()
    {
        var cnftListings = await _cnftIoClient.FetchAllListings(_punkzManager.ProjectName, _punkzManager.TokenPrefix);
        var jpgStoreListings = await _jpgStoreClient.FetchAllListings(_punkzManager.ProjectName, _punkzManager.TokenPrefix);

        await _punkzManager.UpdateSales(cnftListings, _cnftIoClient.MarketName);
        await _punkzManager.UpdateSales(jpgStoreListings, _jpgStoreClient.MarketName, preferredMarket: true);

        return StatusCode(200);
    }

    [HttpGet("sales/collage")]
    public async Task<StatusCodeResult> CollageSales()
    {
        var cnftListings = await _cnftIoClient.FetchAllListings(_collageManager.ProjectName, _collageManager.TokenPrefix);
        var jpgStoreListings = await _jpgStoreClient.FetchAllListings(_collageManager.ProjectName, _collageManager.TokenPrefix);

        await _collageManager.UpdateSales(cnftListings, _cnftIoClient.MarketName);
        await _collageManager.UpdateSales(jpgStoreListings, _jpgStoreClient.MarketName, preferredMarket: true);

        return StatusCode(200);
    }

    [HttpGet("mint/collage")]
    public async Task<StatusCodeResult> CollageMint()
    {
        var apiKey = _configuration[$"NftMakerPro:ApiKey"];
        var projectId = _configuration[$"NftMakerPro:Collage:ProjectId"];

        var results = await _nftMakerProClient.FetchAllNfts(apiKey, projectId);

        await _collageManager.UpdateMint(results);

        return StatusCode(200);
    }
}
