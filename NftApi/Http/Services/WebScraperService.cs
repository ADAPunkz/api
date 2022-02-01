using NftApi.Data.Services;

namespace NftApi.Http.Services;

public sealed class WebScraperService : IHostedService, IDisposable
{
    private readonly IConfiguration _configuration;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<WebScraperService> _logger;

    private int executionCount = 0;
    private Timer _timer = null!;

    public WebScraperService(
        IConfiguration configuration,
        IServiceProvider serviceProvider,
        ILogger<WebScraperService> logger)
    {
        _configuration = configuration;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("WebScraperService started");
        _timer = new Timer(async (object state) => await DoWorkAsync(state), null, TimeSpan.Zero, TimeSpan.FromMinutes(15));
        return Task.CompletedTask;
    }

    private async Task DoWorkAsync(object state)
    {
        _logger.LogInformation("WebScraperService running");
        
        Interlocked.Increment(ref executionCount);

        using var scope = _serviceProvider.CreateScope();

        var punkzManager = scope.ServiceProvider.GetRequiredService<PunkzManager>();
        var collageManager = scope.ServiceProvider.GetRequiredService<CollageManager>();

        var cnftIoClient = scope.ServiceProvider.GetRequiredService<CnftIoClient>();
        var jpgStoreClient = scope.ServiceProvider.GetRequiredService<JpgStoreClient>();
        var nftMakerProClient = scope.ServiceProvider.GetRequiredService<NftMakerProClient>();

        /*try
        {
            var punkzCnftListings = await cnftIoClient.FetchAllListings(punkzManager.ProjectName, punkzManager.TokenPrefix);
            await punkzManager.UpdateSales(punkzCnftListings, cnftIoClient.MarketName);
        }
        catch (Exception ex)
        {
            _logger.LogError("Fetching Punkz sales from CNFT.IO failed", ex);
        }*/

        try
        {
            var punkzJpgListings = await jpgStoreClient.FetchAllListings(punkzManager.ProjectName, punkzManager.TokenPrefix);
            await punkzManager.UpdateSales(punkzJpgListings, jpgStoreClient.MarketName, preferredMarket: true);
        }
        catch (Exception ex)
        {
            _logger.LogError("Fetching Punkz sales from jpg.store failed", ex);
        }

        /*try
        {
            var collageCnftListings = await cnftIoClient.FetchAllListings(collageManager.ProjectName, collageManager.TokenPrefix);
            await collageManager.UpdateSales(collageCnftListings, cnftIoClient.MarketName);
        }
        catch (Exception ex)
        {
            _logger.LogError("Fetching Collage sales from CNFT.IO failed", ex);
        }*/

        try
        {
            var collageJpgListings = await jpgStoreClient.FetchAllListings(collageManager.ProjectName, collageManager.TokenPrefix);
            await collageManager.UpdateSales(collageJpgListings, jpgStoreClient.MarketName, preferredMarket: true);
        }
        catch (Exception ex)
        {
            _logger.LogError("Fetching Collage sales from jpg.store failed", ex);
        }

        try
        {
            var apiKey = _configuration[$"NftMakerPro:ApiKey"];
            var projectId = _configuration[$"NftMakerPro:Collage:ProjectId"];

            var results = await nftMakerProClient.FetchAllNfts(apiKey, projectId);

            await collageManager.UpdateMint(results);
        }
        catch (Exception ex)
        {
            _logger.LogError("Fetching Collage mint status from NFT Maker failed", ex);
        }
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("WebScraperService stopping");
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
