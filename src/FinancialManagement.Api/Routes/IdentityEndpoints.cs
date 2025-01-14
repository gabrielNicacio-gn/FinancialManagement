using System;
using System.Collections.Generic;
using System.Linq;
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
        app.MapGroup("/")
        .WithTags("Autentication")
        .WithSummary("Autentication Routes");

        app.MapPost("/register", async (IIdentityServices identityServices, RegisterUserRequestDto registerUser) =>
        {
            var result = await identityServices.RegisterUser(registerUser);
            return result.IsSucess
            ? Results.Created("", result)
            : Results.BadRequest(result.Errors);
        })
        .Validate<RegisterUserRequestDto>()
        .AllowAnonymous();

        app.MapPost("/login", async (IIdentityServices identityServices, LoginRequestDto login) =>
       {
           var result = await identityServices.Login(login);
           return result.IsSucess
           ? Results.Ok(result)
           : Results.BadRequest(result.Errors);
       })
       .Validate<LoginRequestDto>()
       .AllowAnonymous();

        app.MapPost("/logout", async (IIdentityServices identityServices) =>
       {
           await identityServices.Logout();
           return Results.Ok();
       })
       .RequireAuthorization();

    }
}
