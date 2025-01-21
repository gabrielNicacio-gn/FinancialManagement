using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FinancialManagement.Application.DTOs.Response.Identity;
public class LoginResponseDto
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Token { get; private set; } = string.Empty;
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RefreshToken { get; private set; } = string.Empty;

    public LoginResponseDto(string token, string refreshToken)
    {
        Token = token;
        RefreshToken = refreshToken;
    }
}
