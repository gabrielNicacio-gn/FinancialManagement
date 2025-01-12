using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FinancialManagement.Application.DTOs.Response;
public class LoginResponseDto
{
    public bool IsSucess { get; private set; }
    public List<string> Errors { get; private set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Token { get; private set; } = string.Empty;

    public LoginResponseDto()
    => Errors = new List<string>();

    public LoginResponseDto(bool isSucess = true) : this()
    => IsSucess = isSucess;

    public LoginResponseDto(string token) : this()
    => Token = token;

    public void AddError(string error) =>
    Errors.Add(error);

    public void AddErrors(IEnumerable<string> errors) =>
    Errors.AddRange(errors);

}
