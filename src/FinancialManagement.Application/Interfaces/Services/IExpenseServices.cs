using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Application.DTOs.Request.Expense;
using FinancialManagement.Application.DTOs.Request.Revenue;
using FinancialManagement.Application.DTOs.Response;

namespace FinancialManagement.Application.Interfaces.Services;
public interface IExpenseServices
{
    Task<ExpenseResponseDto> CreateNewExpense(CreateExpenseDto newRevenue);
    Task<RevenueResponseDto> GetExpenseById(Guid idREvenue);
    Task<IEnumerable<RevenueResponseDto>> GetAllExpense();
    Task UpdateExpense(UpdateRevenueDto updateRevenue, string nameProperty);
    Task RemoveExpense(Guid idRevenue);
}
