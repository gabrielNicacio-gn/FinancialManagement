
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

        if (user is null)
            return new LoginResponseDto(false);

        var result = await _signInManager.CheckPasswordSignInAsync(user!, userRequestDto.Password, false);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user!, false);
            var claims = GetClaims(user);
            var token = GenerateJwtToken(result.Succeeded, claims);
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

    private LoginResponseDto GenerateJwtToken(bool isSucess, IEnumerable<Claim> claims)
    {
        var expirations = DateTime.UtcNow.AddSeconds(_jwtBearer.Expirations);

        var tokenDescription = new JwtSecurityToken(
            issuer: _jwtBearer.Issuer,
            audience: _jwtBearer.Audience,
            claims: claims,
            expires: expirations,
            signingCredentials: _jwtBearer.SigningCredentials
        );

        var jwtHandler = new JwtSecurityTokenHandler();

        var encodedToken = jwtHandler.WriteToken(tokenDescription);

        return new LoginResponseDto(isSucess, encodedToken);
    }

    private List<Claim> GetClaims(User user)
    {
        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        claims.Add(new Claim(ClaimTypes.Name, user.UserName!.ToString()));
        return claims;
    }
}
