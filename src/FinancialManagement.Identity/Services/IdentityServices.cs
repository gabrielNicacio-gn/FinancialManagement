
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using FinancialManagement.Application.DTOs.Request.Identity;
using FinancialManagement.Application.DTOs.Response;
using FinancialManagement.Application.Interfaces.IdentityServices;
using FinancialManagement.Identity.Configurations;
using FinancialManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;
using FinancialManagement.Application.DTOs.Response.Identity;
using FinancialManagement.Application.DTOs.Shared;

namespace FinancialManagement.Identity.Services;
public class IdentityServices : IIdentityServices
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly JwtOptions _jwtBearer;
    private readonly IMemoryCache _cache;
    public IdentityServices(SignInManager<User> signInManager,
                           UserManager<User> userManager,
                           IOptions<JwtOptions> jwtBearer,
                           IMemoryCache cache)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtBearer = jwtBearer.Value;
        _cache = cache;
    }

    public async Task<BaseResponseDto<RegisterUserResponseDto>> RegisterUser(RegisterUserRequestDto userRequestDto)
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

        var registerResponse = new BaseResponseDto<RegisterUserResponseDto>(created.Succeeded);

        if (!created.Succeeded)
        {
            registerResponse.AddErrors(created.Errors.Select(er => er.Description));
        }

        return registerResponse;

    }
    public async Task<BaseResponseDto<LoginResponseDto>> Login(LoginRequestDto userRequestDto)
    {
        var user = await _userManager.FindByEmailAsync(userRequestDto.Email);
        if (user is null)
            return new BaseResponseDto<LoginResponseDto>(false);

        var result = await _signInManager.CheckPasswordSignInAsync(user, userRequestDto.Password, false);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return await GenerateCredentials(userRequestDto.Email, result.Succeeded, user);
        }

        var loginResponse = new BaseResponseDto<LoginResponseDto>(result.Succeeded);

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

    public async Task<BaseResponseDto<LoginResponseDto>> LoginWithoutPassword(string userId)
    {
        var loginResponse = new BaseResponseDto<LoginResponseDto>();
        var user = await _userManager.FindByIdAsync(userId);

        if (await _userManager.IsLockedOutAsync(user))
            loginResponse.AddError("This account is blocked");

        if (loginResponse.IsSucess)
            return await GenerateCredentials(user.Email, loginResponse.IsSucess, user);
        return loginResponse;
    }

    private async Task<BaseResponseDto<LoginResponseDto>> GenerateCredentials(string email, bool isSucess, User user)
    {
        if (user is null)
            return new BaseResponseDto<LoginResponseDto>(false);

        var acessTokenClaims = GetClaims(user, false);
        var refreshTokenClaims = GetClaims(user, true);

        var acessTokenExpiration = DateTime.UtcNow.AddMinutes(_jwtBearer.AccessTokenTimeExpiration);
        var refreshTokenExpiration = DateTime.UtcNow.AddMinutes(_jwtBearer.RefreshTokenTimeExpiration);

        var accessToken = GenerateJwtToken(acessTokenClaims, acessTokenExpiration);
        var refreshToken = GenerateJwtToken(refreshTokenClaims, refreshTokenExpiration);

        var response = new LoginResponseDto(accessToken, refreshToken);
        return new BaseResponseDto<LoginResponseDto>(response);
    }
    private string GenerateJwtToken(IEnumerable<Claim> claims, DateTime expiration)
    {

        var tokenDescription = new JwtSecurityToken(
            issuer: _jwtBearer.Issuer,
            audience: _jwtBearer.Audience,
            claims: claims,
            expires: expiration,
            signingCredentials: _jwtBearer.SigningCredentials
        );

        var jwtHandler = new JwtSecurityTokenHandler();

        var encodedToken = jwtHandler.WriteToken(tokenDescription);

        return encodedToken;
    }

    private List<Claim> GetClaims(User user, bool isRefreshToken)
    {
        var claims = new List<Claim>();
        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.UserName!.ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

        if (isRefreshToken)
            claims.Add(new Claim("rftoken", "isRefreshToken"));
        return claims;
    }
}
