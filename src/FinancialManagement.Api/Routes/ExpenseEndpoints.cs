using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FinancialManagement.Application.DTOs.Request.Expense;
using FinancialManagement.Domain.Models;
using FinancialManagement.Domain.Interfaces.Repositories;
using FinancialManagement.Infrastructure.Repositories;
using Npgsql.Replication;
using FinancialManagement.Api.ExtensionsRoutes;

namespace FinancialManagement.Api.Routes;
public static class ExpenseEndpoints
{
    public static void MapExpenseRoutes(this WebApplication app)
    {
        var expensesRoutes = app.MapGroup("/")
        .WithTags("Expenses")
        .WithSummary("Expenses routes");

        expensesRoutes.MapPost("/expense", async (IExpenseRepository expenseRepository, CreateExpenseDto request) =>
        {
            var expense = new Expense
            {
                Value = request.Value,
                DateExpenses = request.DateExpenses,
                Description = request.Description
            };
            var result = await expenseRepository.AddExpenses(expense);
            return Results.Created($"/expense/{result.IdExpenses}", result);
        })
        .WithDescription("Create a new expense")
        .WithSummary("Create a new expense")
        .Validate<CreateExpenseDto>();

        expensesRoutes.MapGet("/expenses", async (IExpenseRepository expenseRepository) =>
        {
            var result = await expenseRepository.GetExpenses();
            return Results.Ok(result);
        })
        .WithDescription("Get all expenses")
        .WithSummary("Get all expenses");

        expensesRoutes.MapGet("/expense", async (IExpenseRepository expenseRepository, Guid id) =>
        {
            var result = await expenseRepository.GetExpensesById(id);
            return result is null ? Results.NotFound() : Results.Ok(result);
        })
        .WithDescription("Get all expenses")
        .WithSummary("Get all expenses");

        expensesRoutes.MapPut("/expense", async (IExpenseRepository expenseRepository, UpdateExpenseDto request) =>
        {
            var expense = new Expense
            {
                Value = request.Value,
                DateExpenses = request.DateExpenses,
                Description = request.Description
            };
            await expenseRepository.UpdateExpenses(expense, nameof(expense.Value));
            return Results.NoContent();
        })
        .WithDescription("Update an expense")
        .WithSummary("Update an expense")
        .Validate<UpdateExpenseDto>();

        expensesRoutes.MapDelete("/expense", async (IExpenseRepository expenseRepository, Guid id) =>
        {
            await expenseRepository.DeleteExpenses(id);
            return Results.NoContent();
        })
        .WithDescription("Delete an expense")
        .WithSummary("Delete an expense");
    }
}
