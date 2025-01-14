
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinancialManagement.Application.DTOs.Request.Identity;
using FinancialManagement.Application.DTOs.Response;
using FinancialManagement.Application.Interfaces.IdentityServices;
using FinancialManagement.Identity.Configurations;
using FinancialManagement.Identity.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Npgsql.Internal;

namespace FinancialManagement.Identity.Services;
public class IdentityServices : IIdentityServices
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly JwtOptions _jwtBearer;
    public IdentityServices(SignInManager<User> signInManager,
                           UserManager<User> userManager,
                           IOptions<JwtOptions> jwtBearer)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtBearer = jwtBearer.Value;
    }

    public async Task<RegisterUserResponseDto> RegisterUser(RegisterUserRequestDto userRequestDto)
    {
        var identityUser = new User()
        {
            Email = userRequestDto.Email,
            UserName = userRequestDto.UserName,
        };

        var passwordHasher = new PasswordHasher<User>();
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
        var user = await _userManager.FindByEmailAsync(userRequestDto.Email);
        var result = await _signInManager.CheckPasswordSignInAsync(user, userRequestDto.Password, false);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            var token = await GenerateJwtToken(result.Succeeded, userRequestDto.Email);
            return token;
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

            loginResponse.AddError("Email or Password incorrect");
        }
        return loginResponse;
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }

    private async Task<LoginResponseDto> GenerateJwtToken(bool isSucess, string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var claims = GetClaims(user);
        var expirations = DateTime.Now.AddSeconds(double.Parse(ConfigurationAppSettingsJson()["JWT:DateExpiration"]!));
        var secretKey = Encoding.ASCII.GetBytes(ConfigurationAppSettingsJson()["JWT:SecretKey"]!);
        var credentials = new SigningCredentials(new SymmetricSecurityKey(secretKey)
            , SecurityAlgorithms.HmacSha256Signature);

        var tokenDescription = new JwtSecurityToken(
            issuer: ConfigurationAppSettingsJson()["JWT:Issuer"],
            audience: ConfigurationAppSettingsJson()["JWT:Audience"],
            claims: claims,
            expires: expirations,
            signingCredentials: credentials
        );

        var jwtHandler = new JwtSecurityTokenHandler();

        var encodedToken = jwtHandler.WriteToken(tokenDescription);

        return new LoginResponseDto(isSucess, encodedToken);
    }

    private List<Claim> GetClaims(User user)
    {
        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, user.Id.ToString()));
        claims.Add(new Claim(ClaimTypes.Email, user.Email!.ToString()));
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