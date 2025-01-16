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
        var identityRoutes = app.MapGroup("/")
        .WithTags("Autentication")
        .WithSummary("Autentication Routes");

        identityRoutes.MapPost("/register", async (IIdentityServices identityServices, RegisterUserRequestDto registerUser) =>
        {
            var result = await identityServices.RegisterUser(registerUser);
            return result.IsSucess
            ? Results.Created("", result)
            : Results.BadRequest(result.Errors);
        })
        .Validate<RegisterUserRequestDto>()
        .AllowAnonymous();

        identityRoutes.MapPost("/login", async (IIdentityServices identityServices, LoginRequestDto login) =>
       {
           var result = await identityServices.Login(login);
           var loginResult =
           result.IsSucess ? Results.Ok(result) : Results.Unauthorized();
           if (!result.IsSucess && result.Errors.Count > 0)
               loginResult = Results.BadRequest(result);

           return loginResult;
       })
       .Validate<LoginRequestDto>()
       .AllowAnonymous();

    }
}
