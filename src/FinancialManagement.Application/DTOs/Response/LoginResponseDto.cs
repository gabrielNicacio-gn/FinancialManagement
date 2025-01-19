using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FinancialManagement.Application.DTOs.Response;
public class LoginResponseDto
{
    public bool IsSucess { get; private set; } = true;
    public List<string> Errors { get; private set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Token { get; private set; } = string.Empty;
    public string RefreshToken { get; private set; } = string.Empty;

    public LoginResponseDto()
    => Errors = new List<string>();
    public LoginResponseDto(bool isSucess) : this()
    => IsSucess = isSucess;

    public LoginResponseDto(bool isSucess, string token, string refreshToken) : this(isSucess)
    {
        Token = token;
        RefreshToken = refreshToken;
    }

    public void AddError(string error) =>
    Errors.Add(error);

    public void AddErrors(IEnumerable<string> errors) =>
    Errors.AddRange(errors);

}
