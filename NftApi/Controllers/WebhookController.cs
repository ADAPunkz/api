using Microsoft.AspNetCore.Mvc;
using NftApi.Data.Services;
using NftApi.Http.Services;

namespace NftApi.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class WebhookController : ApiControllerBase
{
    private readonly CnftIoClient _cnftIoClient;
    private readonly PunkzManager _punkzManager;

    public WebhookController(
        PunkzManager punkzManager,
        CnftIoClient cnftIoClient)
    {
        _punkzManager = punkzManager;
        _cnftIoClient = cnftIoClient;
    }

    [HttpGet("sales/punkz")]
    public async Task<StatusCodeResult> PunkzSales()
    {
        var listings = await _cnftIoClient.FetchAllListings(_punkzManager.ProjectName);

        await _punkzManager.UpdateSales(listings);

        return StatusCode(200);
    }
}
