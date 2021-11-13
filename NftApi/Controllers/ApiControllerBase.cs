using Microsoft.AspNetCore.Mvc;
using NftApi.Data;

namespace NftApi.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    protected ApplicationDbContext DbContext { get; }

    public ApiControllerBase(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }
}
