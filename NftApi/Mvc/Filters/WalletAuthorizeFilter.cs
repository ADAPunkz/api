using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace NftApi.Mvc.Filters;

public class WalletAuthorizeFilter : IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var header = context.HttpContext.Request.Headers.Authorization;

        if (string.IsNullOrWhiteSpace(header))
        {
            context.Result = new StatusCodeResult(401);
            return;
        }

        var token = header.ToString()["Bearer ".Length..];
        var tokenHandler = new JwtSecurityTokenHandler();
        var hmac = Encoding.UTF8.GetBytes("secretsecretsecretsecret");
        
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(hmac),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            var identity = new ClaimsIdentity(jwtToken.Claims, "Bearer");
            var principal = new ClaimsPrincipal(identity);
            
            context.HttpContext.User = principal;
        }
        catch (Exception e)
        {
            context.Result = new StatusCodeResult(403);
        }
    }
}
