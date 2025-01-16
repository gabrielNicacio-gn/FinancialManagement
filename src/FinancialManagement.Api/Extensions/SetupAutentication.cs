using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Identity.Configurations;
using FinancialManagement.Identity.Data;
using FinancialManagement.Identity.Models;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FinancialManagement.Api.Extensions;

public static class UseAtentication
{
    public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {

        var settingsJson = configuration.GetSection(nameof(JwtOptions));
        var secretKey = Encoding.ASCII.GetBytes(configuration.GetSection("JwtOptions:SecretKey").Value!);
        var credentials = new SigningCredentials(new SymmetricSecurityKey(secretKey)
            , SecurityAlgorithms.HmacSha256Signature);

        services.Configure<JwtOptions>(options =>
        {
            options.Audience = settingsJson[nameof(JwtOptions.Audience)]!;
            options.Issuer = settingsJson[nameof(JwtOptions.Issuer)]!;
            options.SigningCredentials = credentials;
            options.Expirations = int.Parse(settingsJson[nameof(JwtOptions.Expirations)]!);
        });

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = configuration.GetSection("JwtOptions:Issuer").Value,

                ValidateAudience = true,
                ValidAudience = configuration.GetSection("JwtOptions:Audience").Value,
                ValidateLifetime = true,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JwtOptions:SecretKey").Value!))
            };
        });

        services.AddAuthorization();
        services.AddIdentityCore<User>();

    }
}
