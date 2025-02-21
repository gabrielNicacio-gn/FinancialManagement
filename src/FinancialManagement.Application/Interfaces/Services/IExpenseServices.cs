using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Application.DTOs.Request.Expense;
using FinancialManagement.Application.DTOs.Request.Revenue;
using FinancialManagement.Application.DTOs.Response;
using FinancialManagement.Application.DTOs.Shared;

namespace FinancialManagement.Application.Interfaces.Services;
public interface IExpenseServices
{
    Task<BaseResponseDto<ExpenseResponseDto>> CreateNewExpense(CreateExpenseDto newExpense, Guid UserId);
    Task<BaseResponseDto<ExpenseResponseDto>> GetExpenseById(Guid idExpense);
    Task<BaseResponseDto<IEnumerable<ExpenseResponseDto>>> GetAllExpense(Guid UserId);
    Task UpdateExpense(UpdateExpenseDto updateExpense, string nameProperty);
    Task RemoveExpense(Guid idExpense);
}
