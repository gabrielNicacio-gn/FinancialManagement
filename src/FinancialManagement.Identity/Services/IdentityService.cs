
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinancialManagement.Application.DTOs.Request.Identity;
using FinancialManagement.Application.DTOs.Response;
using FinancialManagement.Application.Interfaces.IdentityServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Npgsql.Internal;

namespace FinancialManagement.Identity.Services;
public class IdentityService : IIdentityServices
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtBearerOptions _jwtBearer;
    public IdentityService(SignInManager<IdentityUser> signInManager,
                           UserManager<IdentityUser> userManager,
                           JwtBearerOptions jwtBearer)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtBearer = jwtBearer;
    }

    public async Task<RegisterUserResponseDto> RegisterUser(RegisterUserRequestDto userRequestDto)
    {
        var identityUser = new IdentityUser()
        {
            Email = userRequestDto.Email,
            UserName = userRequestDto.UserName,
        };

        var passwordHasher = new PasswordHasher<IdentityUser>();
        var hash = passwordHasher.HashPassword(identityUser, userRequestDto.Password);
        identityUser.PasswordHash = hash;

        var created = await _userManager.CreateAsync(identityUser);

        if (created.Succeeded)
            await _userManager.SetLockoutEnabledAsync(identityUser, false);

        var registerResponse = new RegisterUserResponseDto(created.Succeeded);

        if (!created.Succeeded)
        {
            registerResponse.AddErrors(created.Errors.Select(er => er.Description));
        }

        return registerResponse;

    }
    public async Task<LoginResponseDto> Login(LoginRequestDto userRequestDto)
    {
        var result = await _signInManager.PasswordSignInAsync(userRequestDto.Email, userRequestDto.Password, true, false);
        if (result.Succeeded)
        {
            return await GenerateJwtToken(userRequestDto.Email);

        }

        var loginResponse = new LoginResponseDto(result.Succeeded);

        if (!result.Succeeded)
        {
            if (result.IsLockedOut)
                loginResponse.AddError("This account is blocked");
            else if (result.IsNotAllowed)
                loginResponse.AddError("This account is not allowed to log in");
            else if (result.RequiresTwoFactor)
                loginResponse.AddError("Required external authentication");
        }
        return loginResponse;
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }

    private async Task<LoginResponseDto> GenerateJwtToken(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var claims = GetClaims(user);

        var secretSigningKey = Encoding.ASCII.GetBytes
           (ConfigurationAppSettingsJson().GetValue<string>("JWT:SecretKey")!);

        var jwtHandler = new JwtSecurityTokenHandler();

        var credentials = new SigningCredentials(new SymmetricSecurityKey(secretSigningKey)
            , SecurityAlgorithms.HmacSha256Signature);
        var tokenDescription = new SecurityTokenDescriptor()
        {
            Subject = claims,
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(2),
            Issuer = ConfigurationAppSettingsJson().GetValue<string>("JWT:Issuer"),
            IssuedAt = DateTime.UtcNow
        };
        var newToken = jwtHandler.CreateToken(tokenDescription);

        var encodedToken = jwtHandler.WriteToken(newToken);

        return new LoginResponseDto(encodedToken);
    }

    private ClaimsIdentity GetClaims(IdentityUser user)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.Name, user.Id.ToString()));
        return claims;
    }

    private static IConfigurationRoot ConfigurationAppSettingsJson()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json")
            .Build();
        return configuration;
    }
}
