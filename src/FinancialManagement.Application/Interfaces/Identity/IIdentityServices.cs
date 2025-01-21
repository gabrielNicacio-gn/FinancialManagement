using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Application.DTOs.Request.Identity;
using FinancialManagement.Application.DTOs.Response.Identity;
using FinancialManagement.Application.DTOs.Shared;

namespace FinancialManagement.Application.Interfaces.IdentityServices
{
    public interface IIdentityServices
    {
        Task<BaseResponseDto<RegisterUserResponseDto>> RegisterUser(RegisterUserRequestDto userRequestDto);
        Task<BaseResponseDto<LoginResponseDto>> Login(LoginRequestDto userRequestDto);
        Task<BaseResponseDto<LoginResponseDto>> LoginWithoutPassword(string userId);
    }
}