using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Identity.Configurations;
using FinancialManagement.Identity.Data;
using FinancialManagement.Identity.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FinancialManagement.Api.Extensions;

public static class UseAtentication
{
    public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!))
            };
        })
        .AddCookie(options =>
        {
            options.SlidingExpiration = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
            options.LoginPath = "/Account/Login";
            options.Cookie.SameSite = SameSiteMode.Strict;
        });
        services.AddAuthorization();
        services.AddIdentityCore<User>();

        services.AddIdentity<User, IdentityRole<Guid>>()
        .AddEntityFrameworkStores<FinancialManagementIdentityContext>()
        .AddDefaultTokenProviders();

    }
}
