using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Application.DTOs.Request.Expense;
using FinancialManagement.Application.DTOs.Request.Revenue;
using FinancialManagement.Application.DTOs.Response;
using FinancialManagement.Application.Interfaces.Services;
using FinancialManagement.Domain.Interfaces.Repositories;

namespace FinancialManagement.Application.Services;
public class ExpensesServices : IExpenseServices
{
    private readonly IExpenseRepository _expenseRepository;
    public ExpensesServices(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public Task<ExpenseResponseDto> CreateNewExpense(CreateExpenseDto newRevenue)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<RevenueResponseDto>> GetAllExpense()
    {
        throw new NotImplementedException();
    }

    public Task<RevenueResponseDto> GetExpenseById(Guid idREvenue)
    {
        throw new NotImplementedException();
    }

    public Task RemoveExpense(Guid idRevenue)
    {
        throw new NotImplementedException();
    }

    public Task UpdateExpense(UpdateRevenueDto updateRevenue, string nameProperty)
    {
        throw new NotImplementedException();
    }
}
