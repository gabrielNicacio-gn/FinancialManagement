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
    Task<ExpenseResponseDto> CreateNewExpense(CreateExpenseDto newExpense);
    Task<ExpenseResponseDto> GetExpenseById(Guid idExpense);
    Task<IEnumerable<ExpenseResponseDto>> GetAllExpense();
    Task UpdateExpense(UpdateExpenseDto updateExpense, string nameProperty);
    Task RemoveExpense(Guid idExpense);
}
