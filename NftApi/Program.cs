﻿using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NftApi.Data;
using NftApi.Data.Seed;
using NftApi.Data.Services;
using NftApi.Http.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContext");
    options.UseSqlite(connectionString);
});

builder.Services.AddHttpClient<NftMakerProClient>();
builder.Services.AddHttpClient<CnftIoClient>();
builder.Services.AddHttpClient<JpgStoreClient>();
builder.Services.AddHttpClient<BlockfrostClient>();

builder.Services.AddScoped<PunkzManager>();
builder.Services.AddScoped<CollageManager>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v2", new OpenApiInfo
    {
        Title = "ADAPunkz API", Version = "v2"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors(options => options.AllowAnyOrigin());
    app.UseExceptionHandler("/error-local-development");
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "ADAPunkz API v2");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    await SeedData.InitializePunkz(services);
    //await SeedData.InitializeCollage(services);
    await SeedData.InitializeCollageWhitelist(services);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred seeding the DB");
}

app.Run();
