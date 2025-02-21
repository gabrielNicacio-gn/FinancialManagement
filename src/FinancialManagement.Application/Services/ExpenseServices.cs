
using FinancialManagement.Application.DTOs.Request.Expense;
using FinancialManagement.Application.DTOs.Response;
using FinancialManagement.Application.DTOs.Shared;
using FinancialManagement.Application.Interfaces.Services;
using FinancialManagement.Domain.Interfaces.Repositories;
using FinancialManagement.Domain.Models;
using Microsoft.Extensions.Logging;

namespace FinancialManagement.Application.Services;
public class ExpenseServices : IExpenseServices
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly ILogger<ExpenseServices> _logger;
    public ExpenseServices(IExpenseRepository expenseRepository, ILogger<ExpenseServices> logger)
    {
        _expenseRepository = expenseRepository;
        _logger = logger;
    }

    public async Task<BaseResponseDto<ExpenseResponseDto>> CreateNewExpense(CreateExpenseDto newExpense, Guid UserId)
    {
        var expense = new Expense
        {
            Value = newExpense.Value,
            Description = newExpense.Description,
            DateExpenses = newExpense.DateExpenses,
            IdCategory = newExpense.CategoryExpense,
            UserId = UserId
        };
        var created = await _expenseRepository.AddExpenses(expense);
        _logger.LogInformation($"Expense created with id: {created.IdExpense}");
        var newExpenseResponse = new ExpenseResponseDto(created.IdExpense, created.Value, created.DateExpenses,
        created.Description, created.IdCategory);
        return new BaseResponseDto<ExpenseResponseDto>(newExpenseResponse);
    }

    public async Task<BaseResponseDto<IEnumerable<ExpenseResponseDto>>> GetAllExpense(Guid UserId)
    {
        var expenses = await _expenseRepository.GetExpenses(UserId);
        _logger.LogInformation($"Found {expenses.Count()} expenses");
        var listExpensesResponse = expenses
        .Select(expense => new ExpenseResponseDto(expense.IdExpense, expense.Value, expense.DateExpenses,
        expense.Description, expense.CategoryeExpense.IdCategory));
        return new BaseResponseDto<IEnumerable<ExpenseResponseDto>>(listExpensesResponse);
    }

    public async Task<BaseResponseDto<ExpenseResponseDto>> GetExpenseById(Guid idExpense)
    {
        var expense = await _expenseRepository.GetExpensesById(idExpense);
        if (expense is null)
        {
            _logger.LogInformation($"Expense not found");
            return new BaseResponseDto<ExpenseResponseDto>(false);
        }
        _logger.LogInformation($"Found expense with id: {expense.IdExpense}");
        var expenseRespose = new ExpenseResponseDto(expense.IdExpense, expense.Value, expense.DateExpenses, expense.Description,
        expense.CategoryeExpense.IdCategory);
        return new BaseResponseDto<ExpenseResponseDto>(expenseRespose);
    }

    public async Task RemoveExpense(Guid idExpense)
    {
        await _expenseRepository.DeleteExpenses(idExpense);
        _logger.LogInformation($"Expense with id: {idExpense} removed");
    }

    public async Task UpdateExpense(UpdateExpenseDto updateExpenseDto, string nameProperty)
    {
        var expense = new Expense
        {
            Value = updateExpenseDto.Value,
            Description = updateExpenseDto.Description,
            DateExpenses = updateExpenseDto.DateExpenses,
        };
        await _expenseRepository.UpdateExpenses(expense, nameProperty);
        _logger.LogInformation($"Expense with id: {expense.IdExpense} updated");
    }
}
