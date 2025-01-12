using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Application.DTOs.Response;
public class RegisterUserResponseDto
{
    public bool IsSucess { get; private set; }
    public List<string> Errors { get; private set; }

    public RegisterUserResponseDto()
    => Errors = new List<string>();

    public RegisterUserResponseDto(bool isSucess = true) : this()
    => IsSucess = isSucess;

    public void AddErrors(IEnumerable<string> errors) =>
    Errors.AddRange(errors);
}
