using Microsoft.AspNetCore.Mvc;
using NftApi.Data.Services;
using NftApi.Http.Services;

namespace NftApi.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class WebhookController : ApiControllerBase
{
    private readonly PunkzManager _punkzManager;
 
    private readonly CnftIoClient _cnftIoClient;
    private readonly JpgStoreClient _jpgStoreClient;

    public WebhookController(
        PunkzManager punkzManager,
        CnftIoClient cnftIoClient,
        JpgStoreClient jpgStoreClient)
    {
        _punkzManager = punkzManager;
        _cnftIoClient = cnftIoClient;
        _jpgStoreClient = jpgStoreClient;
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
}
