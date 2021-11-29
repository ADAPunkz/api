using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace NftApi.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : ApiControllerBase
{
    [Route("/error-local-development")]
    public IActionResult ErrorLocalDevelopment([FromServices]IWebHostEnvironment webHostEnvironment)
    {
        if (webHostEnvironment.EnvironmentName != "Development")
        {
            throw new InvalidOperationException("This shouldn't be invoked in non-development environments.");
        }

        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

        return Problem(
            detail: context.Error.StackTrace,
            title: context.Error.Message);
    }

    [Route("/error")]
    public IActionResult Error() => Problem();
}
