using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Application.DTOs.Request.Identity;
using FinancialManagement.Application.DTOs.Response;

namespace FinancialManagement.Application.Interfaces.IdentityServices
{
    public interface IIdentityServices
    {
        Task<RegisterUserResponseDto> RegisterUser(RegisterUserRequestDto userRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto userRequestDto);
        Task<LoginResponseDto> LoginWithoutPassword(string userId);
    }
}