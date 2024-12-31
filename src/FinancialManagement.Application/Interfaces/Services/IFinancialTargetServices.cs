using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Application.DTOs.Request.FinancialTarget;
using FinancialManagement.Application.DTOs.Response;
using FinancialManagement.Domain.Models;

namespace FinancialManagement.Application.Interfaces.Services;
public interface IFinancialTargetServices
{
    Task<FinancialTargetResponseDto> CreateNewFinancialTarget(CreateFinancialTargetDto newRevenue);
    Task<FinancialTargetResponseDto> GetFinancialTargetById(Guid idREvenue);
    Task<IEnumerable<FinancialTargetResponseDto>> GetAllFinancialTarget();
    Task UpdateFinancialTarget(UpdateFinancialTargetDto updateFinancialTarget, string nameProperty);
    Task RemoveFinancialTarget(Guid idRevenue);
}
