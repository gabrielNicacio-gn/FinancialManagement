using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using FinancialManagement.Api.Extensions;
using FinancialManagement.Application.DTOs.Request.Identity;
using FinancialManagement.Application.Interfaces.IdentityServices;

namespace FinancialManagement.Api.Routes;
public static class IdentityRoutes
{
    public static void MapIndentityRoutes(this WebApplication app)
    {
        var identityRoutes = app.MapGroup("/")
        .WithTags("Autentication")
        .WithSummary("Autentication Routes");

        identityRoutes.MapPost("/account", async (IIdentityServices identityServices, RegisterUserRequestDto registerUser) =>
        {
            var result = await identityServices.RegisterUser(registerUser);
            return result.IsSucess
            ? Results.Created("", result)
            : Results.BadRequest(result);
        })
        .Validate<RegisterUserRequestDto>()
        .AllowAnonymous();

        identityRoutes.MapPost("/sign-in", async (IIdentityServices identityServices, LoginRequestDto login) =>
       {
           var result = await identityServices.Login(login);
           return result.IsSucess
           ? Results.Ok(result)
           : Results.BadRequest(result);
       })
       .Validate<LoginRequestDto>()
       .AllowAnonymous();

        identityRoutes.MapPost("/refresh-token", async (IIdentityServices identityServices) =>
       {
           var httpContext = app.Services.GetRequiredService<IHttpContextAccessor>().HttpContext;
           var identity = httpContext.User.Identity as ClaimsIdentity;
           var userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
           var result = await identityServices.LoginWithoutPassword(userId);
           return result.IsSucess
           ? Results.Ok(result)
           : Results.BadRequest(result);
       })
       .RequireAuthorization();

    }
}
