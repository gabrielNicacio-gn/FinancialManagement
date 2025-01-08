
using FinancialManagement.Application.DTOs.Request.Expense;
using FinancialManagement.Application.DTOs.Response;
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

    public async Task<ExpenseResponseDto> CreateNewExpense(CreateExpenseDto newExpense)
    {
        var expense = new Expense
        {
            Value = newExpense.Value,
            Description = newExpense.Description,
            DateExpenses = newExpense.DateExpenses,
        };
        var created = await _expenseRepository.AddExpenses(expense);
        _logger.LogInformation($"Expense created with id: {created.IdExpense}");
        return new ExpenseResponseDto(expense.IdExpense, created.Value, created.DateExpenses,
        created.Description, created.CategoryeExpense.IdCategory, created.CategoryeExpense.Name);
    }

    public async Task<IEnumerable<ExpenseResponseDto>> GetAllExpense()
    {
        var expenses = await _expenseRepository.GetExpenses();
        _logger.LogInformation($"Found {expenses.Count()} expenses");
        return expenses
        .Select(expense => new ExpenseResponseDto(expense.IdExpense, expense.Value, expense.DateExpenses,
        expense.Description, expense.CategoryeExpense.IdCategory, expense.CategoryeExpense.Name));

    }

    public async Task<ExpenseResponseDto> GetExpenseById(Guid idExpense)
    {
        var expense = await _expenseRepository.GetExpensesById(idExpense)
        ?? throw new Exception("Expense not found");
        _logger.LogInformation($"Found expense with id: {expense.IdExpense}");
        return new ExpenseResponseDto(expense.IdExpense, expense.Value, expense.DateExpenses, expense.Description,
        expense.CategoryeExpense.IdCategory, expense.CategoryeExpense.Name);
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
