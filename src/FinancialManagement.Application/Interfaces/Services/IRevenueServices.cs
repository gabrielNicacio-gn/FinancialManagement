using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Application.DTOs.Request.Expense;
using FinancialManagement.Application.DTOs.Request.Revenue;
using FinancialManagement.Application.DTOs.Response;

namespace FinancialManagement.Domain.Interfaces.Services;

public interface IRevenueServices
{
    Task<RevenueResponseDto> CreateNewRevenue(CreateRevenueDto newRevenue);
    Task<RevenueResponseDto> GetRevenueById(Guid idREvenue);
    Task<IEnumerable<RevenueResponseDto>> GetAllRevenue();
    Task UpdateRevenue(UpdateRevenueDto updateRevenue, string nameProperty);
    Task RemoveRevenue(Guid idRevenue);
}
